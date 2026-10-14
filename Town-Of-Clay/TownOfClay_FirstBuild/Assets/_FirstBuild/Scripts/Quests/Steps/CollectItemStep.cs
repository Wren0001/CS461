// CollectItemStep.cs (ClayFirstBuild)
// Have N of an item. Counts what's already in the inventory when the step starts.
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Collect Item")]
    public class CollectItemStep : QuestStep
    {
        public string itemId;
        [Min(1)] public int required = 1;

        public override int Required => required;

        public override void OnStart(StepProgress p, IQuestServices s)
        {
            if (s == null) return;
            p.count = s.CountItem(itemId);
            p.done = p.count >= required;
        }

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type != QuestEventType.ItemCollected || !Match(itemId, e.id)) return;
            if (s != null) { p.count = s.CountItem(itemId); p.done = p.count >= required; }
            else AddCount(p, e.amount);
        }
    }
}
