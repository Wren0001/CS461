// FriendshipReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Rewards/Friendship")]
    public class FriendshipReward : QuestReward
    {
        public string npcId;
        public int hearts = 1;

        public override void Grant(IQuestServices s) => s.AddFriendship(npcId, hearts);
        public override string Describe() => $"{npcId} +{hearts} heart{(hearts == 1 ? "" : "s")}";
    }
}
