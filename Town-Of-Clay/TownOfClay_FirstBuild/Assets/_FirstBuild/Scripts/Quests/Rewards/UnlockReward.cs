// UnlockReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Rewards/Unlock")]
    public class UnlockReward : QuestReward
    {
        public string unlockId;
        [Tooltip("Shown in the offer popup, e.g. 'Parcel sending'.")]
        public string displayName;

        public override void Grant(IQuestServices s) => s.Unlock(unlockId);
        public override string Describe() => string.IsNullOrEmpty(displayName) ? unlockId : displayName;
    }
}
