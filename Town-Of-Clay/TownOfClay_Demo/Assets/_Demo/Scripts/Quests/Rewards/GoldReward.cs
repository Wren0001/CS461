// GoldReward.cs (ClayDemo)
using UnityEngine;

namespace ClayDemo.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay Demo/Quests/Rewards/Gold")]
    public class GoldReward : QuestReward
    {
        [Min(0)] public int amount;

        public override void Grant(IQuestServices s) => s.AddGold(amount);
        public override string Describe() => $"{amount}g";
    }
}
