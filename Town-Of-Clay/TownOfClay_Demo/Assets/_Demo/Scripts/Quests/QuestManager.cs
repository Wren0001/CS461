// QuestManager.cs (ClayDemo)
// Owns quest state: which quests are available, active, ready to turn in or complete,
// listens to QuestEvents, advances steps and grants rewards.
// Put it in Boot.unity with an IQuestServices component (DebugQuestServices for now).
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ClayDemo.Quests.Rewards;

namespace ClayDemo.Quests
{
    public enum QuestStatus { Locked, Available, Active, ReadyToTurnIn, Complete }
    public enum QuestMarker { None, New, TurnIn }

    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        [Tooltip("Every quest in this build.")]
        [SerializeField] List<QuestDefinition> quests = new List<QuestDefinition>();
        [Tooltip("Any component that implements IQuestServices. Empty = looks on this GameObject.")]
        [SerializeField] MonoBehaviour servicesProvider;
        [SerializeField] bool logEvents = true;

        /// <summary>Any quest changed status or progress. UI listens to this.</summary>
        public event Action Changed;
        public event Action<QuestDefinition> QuestAvailable;
        public event Action<QuestDefinition> QuestStarted;
        public event Action<QuestDefinition> QuestUpdated;
        public event Action<QuestDefinition> QuestReadyToTurnIn;
        public event Action<QuestDefinition> QuestCompleted;

        [Serializable]
        class QuestRuntime
        {
            public string id;
            public QuestStatus status;
            public int stepIndex;
            public StepProgress progress = new StepProgress();
            [NonSerialized] public QuestDefinition def;
        }

        readonly Dictionary<string, QuestRuntime> runtimes = new Dictionary<string, QuestRuntime>();
        IQuestServices services;

        public IQuestServices Services => services;
        public IReadOnlyList<QuestDefinition> AllQuests => quests;

        // ---------------------------------------------------------------- setup
        void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            services = servicesProvider as IQuestServices;
            if (services == null) services = GetComponent<IQuestServices>();
            if (services == null)
                Debug.LogWarning("[Quests] No IQuestServices found. Add DebugQuestServices to this GameObject.", this);

            foreach (var def in quests)
            {
                if (def == null) continue;
                if (string.IsNullOrEmpty(def.id)) { Debug.LogError($"[Quests] {def.name} has no id.", def); continue; }
                if (runtimes.ContainsKey(def.id)) { Debug.LogError($"[Quests] Duplicate quest id '{def.id}'.", def); continue; }
                runtimes[def.id] = new QuestRuntime { id = def.id, def = def, status = QuestStatus.Locked };
            }
        }

        void Start() => RefreshAvailability();
        void OnEnable() => QuestEvents.Raised += OnQuestEvent;
        void OnDisable() => QuestEvents.Raised -= OnQuestEvent;
        void OnDestroy() { if (Instance == this) Instance = null; }

        public void SetServices(IQuestServices s) => services = s;

        // ---------------------------------------------------------------- queries
        public QuestDefinition Get(string id) => runtimes.TryGetValue(id, out var rt) ? rt.def : null;
        public QuestStatus GetStatus(string id) => runtimes.TryGetValue(id, out var rt) ? rt.status : QuestStatus.Locked;
        public bool IsComplete(string id) => GetStatus(id) == QuestStatus.Complete;

        public IEnumerable<QuestDefinition> ActiveQuests =>
            runtimes.Values.Where(r => r.status == QuestStatus.Active || r.status == QuestStatus.ReadyToTurnIn).Select(r => r.def);

        public IEnumerable<QuestDefinition> CompletedQuests =>
            runtimes.Values.Where(r => r.status == QuestStatus.Complete).Select(r => r.def);

        public QuestStep CurrentStep(string id)
        {
            if (!runtimes.TryGetValue(id, out var rt) || rt.status != QuestStatus.Active) return null;
            return rt.stepIndex < rt.def.steps.Count ? rt.def.steps[rt.stepIndex] : null;
        }

        /// <summary>Tracker text: the current step with its count, or "Talk to X" when ready to turn in.</summary>
        public string TrackerText(string id)
        {
            if (!runtimes.TryGetValue(id, out var rt)) return "";
            switch (rt.status)
            {
                case QuestStatus.ReadyToTurnIn: return $"Report back to {Pretty(rt.def.giverId)}";
                case QuestStatus.Complete: return "Complete";
                case QuestStatus.Active:
                    var step = CurrentStep(id);
                    return step != null ? step.ProgressText(rt.progress) : "";
                default: return "";
            }
        }

        /// <summary>Marker to show over an NPC or object: '?' beats '!'.</summary>
        public QuestMarker GetMarker(string giverOrObjectId)
        {
            if (string.IsNullOrEmpty(giverOrObjectId)) return QuestMarker.None;
            bool offer = false;
            foreach (var rt in runtimes.Values)
            {
                if (rt.status == QuestStatus.ReadyToTurnIn && rt.def.requiresTurnIn && rt.def.giverId == giverOrObjectId)
                    return QuestMarker.TurnIn;
                if (rt.status == QuestStatus.Available && rt.def.OfferLocation == giverOrObjectId)
                    offer = true;
            }
            return offer ? QuestMarker.New : QuestMarker.None;
        }

        public QuestDefinition FirstAvailableAt(string giverOrObjectId) =>
            runtimes.Values.Where(r => r.status == QuestStatus.Available && r.def.OfferLocation == giverOrObjectId)
                .Select(r => r.def).FirstOrDefault();

        public QuestDefinition FirstReadyToTurnIn(string giverId) =>
            runtimes.Values.Where(r => r.status == QuestStatus.ReadyToTurnIn && r.def.giverId == giverId)
                .Select(r => r.def).FirstOrDefault();

        // ---------------------------------------------------------------- actions
        /// <summary>Starts a quest. Works from Available, and from Locked for letter/object quests.</summary>
        public bool StartQuest(string id)
        {
            if (!runtimes.TryGetValue(id, out var rt)) { Debug.LogWarning($"[Quests] Unknown quest '{id}'."); return false; }
            if (rt.status != QuestStatus.Available && rt.status != QuestStatus.Locked) return false;

            rt.status = QuestStatus.Active;
            rt.stepIndex = 0;
            rt.progress = new StepProgress();
            Log($"Started '{rt.def.title}'");
            QuestStarted?.Invoke(rt.def);
            BeginStep(rt);
            Changed?.Invoke();
            return true;
        }

        /// <summary>Called by QuestGiver when the player talks to the giver of a quest that's ready.</summary>
        public bool TurnIn(string id)
        {
            if (!runtimes.TryGetValue(id, out var rt) || rt.status != QuestStatus.ReadyToTurnIn) return false;
            Complete(rt);
            return true;
        }

        /// <summary>Re-checks which quests can be offered. Call from GameClock.DayStarted later.</summary>
        public void RefreshAvailability()
        {
            bool any = false;
            foreach (var rt in runtimes.Values)
            {
                if (rt.status != QuestStatus.Locked || !rt.def.offerOnStart) continue;
                if (!ConditionsMet(rt.def)) continue;
                rt.status = QuestStatus.Available;
                any = true;
                Log($"Available: '{rt.def.title}'");
                QuestAvailable?.Invoke(rt.def);
            }
            if (any) Changed?.Invoke();
        }

        bool ConditionsMet(QuestDefinition def)
        {
            return true;
        }

        // ---------------------------------------------------------------- progress
        void OnQuestEvent(QuestEvent e)
        {
            if (logEvents) Log($"Event {e}");
            // copy: completing a quest can change the collection
            foreach (var rt in runtimes.Values.Where(r => r.status == QuestStatus.Active).ToList())
            {
                var step = rt.stepIndex < rt.def.steps.Count ? rt.def.steps[rt.stepIndex] : null;
                if (step == null) continue;
                int before = rt.progress.count;
                step.OnEvent(e, rt.progress, services);
                if (rt.progress.done) AdvanceStep(rt);
                else if (rt.progress.count != before) { QuestUpdated?.Invoke(rt.def); Changed?.Invoke(); }
            }
        }

        void BeginStep(QuestRuntime rt)
        {
            if (rt.stepIndex >= rt.def.steps.Count) { FinishSteps(rt); return; }
            var step = rt.def.steps[rt.stepIndex];
            if (step == null) { Debug.LogError($"[Quests] '{rt.def.id}' has an empty step slot {rt.stepIndex}."); return; }
            step.OnStart(rt.progress, services);
            if (rt.progress.done) AdvanceStep(rt);   // e.g. already holding the items
            else QuestUpdated?.Invoke(rt.def);
        }

        void AdvanceStep(QuestRuntime rt)
        {
            Log($"'{rt.def.title}' step {rt.stepIndex + 1}/{rt.def.steps.Count} done");
            rt.stepIndex++;
            rt.progress = new StepProgress();
            BeginStep(rt);
            Changed?.Invoke();
        }

        void FinishSteps(QuestRuntime rt)
        {
            if (rt.def.requiresTurnIn)
            {
                rt.status = QuestStatus.ReadyToTurnIn;
                Log($"Ready to turn in: '{rt.def.title}'");
                QuestReadyToTurnIn?.Invoke(rt.def);
                Changed?.Invoke();
            }
            else Complete(rt);
        }

        void Complete(QuestRuntime rt)
        {
            rt.status = QuestStatus.Complete;
            if (services != null)
                foreach (var r in rt.def.rewards)
                    if (r != null) r.Grant(services);
            Log($"Complete: '{rt.def.title}' -> {rt.def.RewardSummary()}");
            QuestCompleted?.Invoke(rt.def);
            Changed?.Invoke();
            RefreshAvailability();
        }

        // ---------------------------------------------------------------- helpers
        void Log(string msg) { if (logEvents) Debug.Log($"[Quests] {msg}", this); }

        static string Pretty(string id) =>
            string.IsNullOrEmpty(id) ? "the giver" :
            System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(id.Replace('_', ' '));

#if UNITY_EDITOR
        [ContextMenu("Fill list with every quest in the project")]
        void FindAllQuests()
        {
            quests = UnityEditor.AssetDatabase.FindAssets("t:QuestDefinition")
                .Select(g => UnityEditor.AssetDatabase.LoadAssetAtPath<QuestDefinition>(UnityEditor.AssetDatabase.GUIDToAssetPath(g)))
                .Where(q => q != null).OrderBy(q => q.name).ToList();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
