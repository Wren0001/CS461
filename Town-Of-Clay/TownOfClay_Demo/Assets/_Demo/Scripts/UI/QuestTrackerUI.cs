// QuestTrackerUI.cs (ClayDemo)
// HUD box showing one active quest: its title and the current step with a count.
// Uses TextMeshPro. Shows the pinned quest if it's active, otherwise the first active one.
using UnityEngine;
using TMPro;
using ClayDemo.Quests;

namespace ClayDemo.UI
{
    public class QuestTrackerUI : MonoBehaviour
    {
        [SerializeField] GameObject root;          // hidden when nothing is active
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text stepText;
        [SerializeField] string pinnedQuestId;

        QuestManager manager;

        void Start()
        {
            manager = QuestManager.Instance;
            if (manager != null) manager.Changed += Refresh;
            Refresh();
        }

        void OnDestroy()
        {
            if (manager != null) manager.Changed -= Refresh;
        }

        public void Pin(string questId) { pinnedQuestId = questId; Refresh(); }

        public void Refresh()
        {
            QuestDefinition q = null;
            if (manager != null)
            {
                foreach (var a in manager.ActiveQuests)
                {
                    if (q == null) q = a;
                    if (a.id == pinnedQuestId) { q = a; break; }
                }
            }
            if (root != null) root.SetActive(q != null);
            if (q == null) return;
            if (titleText != null) titleText.text = q.title;
            if (stepText != null) stepText.text = manager.TrackerText(q.id);
        }
    }
}
