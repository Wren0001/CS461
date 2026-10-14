// QuestEventTester.cs (ClayFirstBuild)
// Fire quest events from the Inspector (right-click the component header) to play through
// quests before the real systems exist. Collected items are also added to DebugQuestServices,
// and delivered ones taken from it, so Collect/Deliver steps behave like the real thing.
using UnityEngine;

namespace ClayFirstBuild.Quests
{
    public class QuestEventTester : MonoBehaviour
    {
        public QuestEventType type = QuestEventType.ItemCollected;
        public string id = "letter";
        public string targetId;
        [Min(1)] public int amount = 1;
        [Space]
        public string questId = "lost_letters";

        [ContextMenu("Raise event")]
        public void RaiseEvent()
        {
            var debug = FindAnyObjectByType<DebugQuestServices>();
            if (debug != null && type == QuestEventType.ItemCollected) debug.GiveItem(id, amount);
            if (debug != null && type == QuestEventType.ItemDelivered && !debug.TakeItem(id, amount))
                Debug.LogWarning($"[QuestEventTester] Not enough {id} to deliver; raising the event anyway.");
            QuestEvents.Raise(new QuestEvent(type, id, targetId, amount));
        }

        [ContextMenu("Start quest")]
        public void StartQuest() => QuestManager.Instance?.StartQuest(questId);

        [ContextMenu("Turn in quest")]
        public void TurnIn() => QuestManager.Instance?.TurnIn(questId);

        [ContextMenu("Print quest state")]
        public void Print()
        {
            var m = QuestManager.Instance;
            if (m == null) return;
            foreach (var q in m.AllQuests)
                if (q != null) Debug.Log($"[Quests] {q.id}: {m.GetStatus(q.id)} — {m.TrackerText(q.id)}");
        }
    }
}
