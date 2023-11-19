using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour {

	[Header("Quest Details")]
	public string questName;
	public QuestType questType;

	[Header("Completion Conditions")]
	public int requiredAmount;
	public FoodType foodType;
	public CustomerType customerType;

	[Header("Rewards")]
	public int cashReward;
	[Header("Rewards")]
	public int reputationReward;

	[Header("Graphics")]
	public TextMeshProUGUI questNameText;
	public TextMeshProUGUI rewardsText;
	public TextMeshProUGUI progressText;

	public Image progressBarBackground;
	public Image progressBar;

	private int currentAmount;

	public bool IsCompleted {
		get => currentAmount >= requiredAmount;
	}

	private void Start() {
		currentAmount = 0;
		questNameText.text = questName;
		rewardsText.text = GetRewardsText();
		UpdateProgress();
	}

	public string GetRewardsText() {
		string rewardsText = "Rewards: ";

		switch (questType) {
			case QuestType.SERVE_FOOD:
			case QuestType.SERVE_CUSTOMER:
				rewardsText += "$" + cashReward;
				break;
			case QuestType.SPEND:
			default:
				rewardsText += reputationReward + " rep.";
				break;
		}

		return rewardsText;
	}

	public void AddToProgress(int amount) {
		currentAmount += amount;
		UpdateProgress();
		if (IsCompleted) {
			AddRewards();
			QuestManager.Instance.NotifyQuestCompleted(transform.GetSiblingIndex());
		}
	}

	private void UpdateProgress() {
		progressText.text = string.Format("{0}/{1}", currentAmount, requiredAmount);

		float width = progressBarBackground.rectTransform.rect.width * currentAmount / requiredAmount;
		float height = progressBarBackground.rectTransform.rect.height;
		progressBar.rectTransform.sizeDelta = new Vector2(width, height);
	}

	private void AddRewards() {
		switch (questType) {
			case QuestType.SERVE_FOOD:
			case QuestType.SERVE_CUSTOMER:
				LevelManager.Instance.Money += cashReward;
				break;
			case QuestType.SPEND:
				LevelManager.Instance.Reputation += reputationReward;
				break;
		}
	}

}
