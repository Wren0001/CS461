// GoldReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Rewards/Gold")]
    public class GoldReward : QuestReward
    {
        [Min(0)] public int amount;

        public override void Grant(IQuestServices s) => s.AddGold(amount);
        public override string Describe() => $"{amount}g";
    }
}
