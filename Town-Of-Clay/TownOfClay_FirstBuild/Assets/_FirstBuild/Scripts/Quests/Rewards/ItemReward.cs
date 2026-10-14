// ItemReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Rewards/Item")]
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
