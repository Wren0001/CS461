// TalkToStep.cs (ClayFirstBuild)
// Talk to an NPC or read an object (the notice board, the kiln).
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Talk To")]
    public class TalkToStep : QuestStep
    {
        public string targetId;

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type == QuestEventType.TalkedTo && Match(targetId, e.id))
                AddCount(p, 1);
        }
    }
}
