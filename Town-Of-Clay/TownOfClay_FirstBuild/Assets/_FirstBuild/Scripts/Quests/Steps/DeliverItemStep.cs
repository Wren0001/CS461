// DeliverItemStep.cs (ClayFirstBuild)
// Hand N of an item to a target (an NPC id or a drop-off id such as "post_office").
// The system doing the hand-over removes the items, then raises QuestEvents.ItemDelivered.
using UnityEngine;

namespace ClayFirstBuild.Quests.Steps
{
    [CreateAssetMenu(menuName = "Clay First Build/Quests/Steps/Deliver Item")]
    public class DeliverItemStep : QuestStep
    {
        public string itemId;
        [Tooltip("NPC or drop-off id. End with * for a prefix, e.g. mailbox_*")]
        public string targetId;
        [Min(1)] public int required = 1;

        public override int Required => required;

        public override void OnEvent(QuestEvent e, StepProgress p, IQuestServices s)
        {
            if (e.type == QuestEventType.ItemDelivered && Match(itemId, e.id) && Match(targetId, e.targetId))
                AddCount(p, e.amount);
        }
    }
}
