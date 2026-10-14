// CounterStep.cs (ClayFirstBuild)
// Count something that isn't an item, raised with QuestEvents.CounterRaised(counterId).
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Counter")]
    public class CounterStep : QuestStep
    {
        public string counterId;
        [Min(1)] public int required = 1;

        public override int Required => required;

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type == QuestEventType.CounterRaised && Match(counterId, e.id))
                AddCount(p, e.amount);
        }
    }
}
