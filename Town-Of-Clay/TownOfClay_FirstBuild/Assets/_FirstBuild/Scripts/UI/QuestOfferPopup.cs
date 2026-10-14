// QuestOfferPopup.cs (ClayFirstBuild)
// The ui_quest_offer panel: title, description, rewards, Accept / Decline.
// Put this component on an object that stays active and point 'panel' at the child that shows/hides.
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ClayFirstBuild.Quests;

namespace ClayFirstBuild.UI
{
    public class QuestOfferPopup : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TMP_Text titleText;
        [SerializeField] TMP_Text descriptionText;
        [SerializeField] TMP_Text rewardsText;
        [SerializeField] Button acceptButton;
        [SerializeField] Button declineButton;

        Action onAccept;

        public bool IsOpen => panel != null && panel.activeSelf;

        void Awake()
        {
            if (acceptButton != null) acceptButton.onClick.AddListener(Accept);
            if (declineButton != null) declineButton.onClick.AddListener(Hide);
            if (panel != null) panel.SetActive(false);
        }

        void OnEnable() => QuestGiver.OfferRequested += Show;
        void OnDisable() => QuestGiver.OfferRequested -= Show;

        public void Show(QuestDefinition quest, Action accept)
        {
            onAccept = accept;
            if (titleText != null) titleText.text = quest.title;
            if (descriptionText != null) descriptionText.text = quest.description;
            if (rewardsText != null)
            {
                string r = quest.RewardSummary();
                rewardsText.text = string.IsNullOrEmpty(r) ? "" : "Reward: " + r;
            }
            if (panel != null) panel.SetActive(true);
            if (acceptButton != null) acceptButton.Select();
        }

        public void Accept()
        {
            var a = onAccept;
            Hide();
            a?.Invoke();
        }

        public void Hide()
        {
            onAccept = null;
            if (panel != null) panel.SetActive(false);
        }
    }
}
