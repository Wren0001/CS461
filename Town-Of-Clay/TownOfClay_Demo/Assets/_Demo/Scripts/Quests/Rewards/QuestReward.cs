// QuestReward.cs (ClayDemo)
using UnityEngine;

namespace ClayDemo.Quests.Rewards
{
    public abstract class QuestReward : ScriptableObject
    {
        public abstract void Grant(IQuestServices s);

        /// <summary>Short text for the offer popup, e.g. "150g".</summary>
        public abstract string Describe();
    }
}
