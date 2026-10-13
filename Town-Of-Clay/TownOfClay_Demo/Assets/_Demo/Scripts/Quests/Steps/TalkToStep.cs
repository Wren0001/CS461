// TalkToStep.cs (ClayDemo)
// Talk to an NPC or read an object (the notice board, the kiln).
using UnityEngine;

namespace ClayDemo.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay Demo/Quests/Steps/Talk To")]
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
