// VisitLocationStep.cs (ClayFirstBuild)
// Enter a location (raised by LocationTrigger or on scene load), e.g. "quarry".
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Visit Location")]
    public class VisitLocationStep : QuestStep
    {
        public string locationId;

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type == QuestEventType.LocationEntered && Match(locationId, e.id))
                AddCount(p, 1);
        }
    }
}
