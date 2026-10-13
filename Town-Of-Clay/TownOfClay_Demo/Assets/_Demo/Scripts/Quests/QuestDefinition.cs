// QuestDefinition.cs (ClayDemo)
// One quest as an asset: steps run in order, rewards are granted on completion.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ClayDemo.Quests.Rewards;

namespace ClayDemo.Quests
{
    public enum QuestSource { NPC, NoticeBoard, Letter, Object }

    [CreateAssetMenu(menuName = "Clay Demo/Quests/Quest")]
    public class QuestDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string id;
        public string title;
        [TextArea(2, 5)] public string description;

        [Header("Who and where")]
        public QuestSource source = QuestSource.NPC;
        [Tooltip("NPC or object the quest is turned in to.")]
        public string giverId;
        [Tooltip("Where the '!' offer shows. Empty = the giver. The notice board uses 'notice_board'.")]
        public string offeredAt;

        [Header("Flow")]
        [Tooltip("Can be offered as soon as its conditions are met. Turn off for quests started by a letter or an object (MailManager / ScriptedBreakdown call StartQuest).")]
        public bool offerOnStart = true;
        [Tooltip("After the last step: on = talk to the giver to finish ('?' marker); off = finishes by itself.")]
        public bool requiresTurnIn = true;

        public List<QuestStep> steps = new List<QuestStep>();
        public List<QuestReward> rewards = new List<QuestReward>();

        public string OfferLocation => string.IsNullOrEmpty(offeredAt) ? giverId : offeredAt;

        public string RewardSummary() =>
            string.Join(" · ", rewards.Where(r => r != null).Select(r => r.Describe()));

        void OnValidate()
        {
            if (string.IsNullOrEmpty(id)) id = name;
        }
    }
}
