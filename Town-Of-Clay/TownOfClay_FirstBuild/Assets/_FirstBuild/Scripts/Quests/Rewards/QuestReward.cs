// QuestReward.cs (ClayFirstBuild)
using UnityEngine;

namespace ClayFirstBuild.Quests.Rewards
{
    public abstract class QuestReward : ScriptableObject
    {
        public abstract void Grant(IQuestServices s);

        /// <summary>Short text for the offer popup, e.g. "150g".</summary>
        public abstract string Describe();
    }
}
