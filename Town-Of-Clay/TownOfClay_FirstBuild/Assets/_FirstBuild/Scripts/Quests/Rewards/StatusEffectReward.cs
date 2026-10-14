// StatusEffectReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Rewards/Status Effect")]
    public class StatusEffectReward : QuestReward
    {
        public string effectId;
        [Min(0)] public float hours = 2f;

        public override void Grant(IQuestServices s) => s.ApplyStatus(effectId, hours);
        public override string Describe() => $"{effectId} ({hours:0.#}h)";
    }
}
