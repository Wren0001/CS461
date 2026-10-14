// QuestGiver.cs (ClayFirstBuild)
// Put on an NPC or object that offers or takes back quests (the Postmaster, the notice board).
// Shows the '!' / '?' marker, and Interact() handles offer, turn-in and the TalkedTo event.
// Call Interact() from the NPC's or object's own interaction code (NpcInteractable, notice board).
using System;
using UnityEngine;

namespace ClayFirstBuild.Quests
{
    public class QuestGiver : MonoBehaviour
    {
        /// <summary>Raised to show an offer. QuestOfferPopup listens; the Action accepts the quest.</summary>
        public static event Action<QuestDefinition, Action> OfferRequested;

        [Tooltip("Matches QuestDefinition.giverId / offeredAt, e.g. postmaster, notice_board, marigold.")]
        public string giverId;
        [SerializeField] SpriteRenderer marker;
        [SerializeField] Sprite newQuestSprite;   // quest_marker_new
        [SerializeField] Sprite turnInSprite;     // quest_marker_turn_in

        QuestManager manager;

        void Start()
        {
            manager = QuestManager.Instance;
            if (manager != null) manager.Changed += RefreshMarker;
            RefreshMarker();
        }

        void OnDestroy()
        {
            if (manager != null) manager.Changed -= RefreshMarker;
        }

        public void RefreshMarker()
        {
            if (marker == null) return;
            var m = manager != null ? manager.GetMarker(giverId) : QuestMarker.None;
            marker.enabled = m != QuestMarker.None;
            marker.sprite = m == QuestMarker.TurnIn ? turnInSprite : newQuestSprite;
        }

        /// <summary>The player talked to this giver.</summary>
        public void Interact()
        {
            if (manager == null) return;

            // Talking counts first: it may finish a "talk to" step and make a quest ready.
            QuestEvents.TalkedTo(giverId);

            var ready = manager.FirstReadyToTurnIn(giverId);
            if (ready != null) { manager.TurnIn(ready.id); return; }

            var offer = manager.FirstAvailableAt(giverId);
            if (offer == null) return;

            if (OfferRequested != null) OfferRequested(offer, () => Accept(offer));
            else Accept(offer);   // no popup in the scene yet: accept straight away
        }

        void Accept(QuestDefinition q)
        {
            if (!manager.StartQuest(q.id)) return;
            // Accepting at this giver counts as talking to it, so "Read the notice board" ticks off.
            QuestEvents.TalkedTo(giverId);
        }
    }
}
