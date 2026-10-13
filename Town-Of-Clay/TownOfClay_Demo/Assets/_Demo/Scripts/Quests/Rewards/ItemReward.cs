// ItemReward.cs (ClayDemo)
using UnityEngine;

namespace ClayDemo.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay Demo/Quests/Rewards/Item")]
    public class ItemReward : QuestReward
    {
        public string itemId;
        [Min(1)] public int count = 1;
        [Tooltip("Shown in the offer popup; falls back to the id.")]
        public string displayName;

        public override void Grant(IQuestServices s) => s.GiveItem(itemId, count);

        public override string Describe()
        {
            string n = string.IsNullOrEmpty(displayName) ? itemId : displayName;
            return count > 1 ? $"{n} x{count}" : n;
        }
    }
}
