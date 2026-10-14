// CraftItemStep.cs (ClayFirstBuild)
// Make N of an item: fired pottery (cup_celadon), a packed parcel (package_small), a repair (kiln_repaired).
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Craft Item")]
    public class CraftItemStep : QuestStep
    {
        public string itemId;
        [Min(1)] public int required = 1;

        public override int Required => required;

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type == QuestEventType.ItemCrafted && Match(itemId, e.id))
                AddCount(p, e.amount);
        }
    }
}
