using TMPro;
using UnityEngine;

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

	private int currentAmount;

	public bool IsCompleted {
		get => currentAmount >= requiredAmount;
	}

	private void Start() {
		questNameText.text = questName;
		rewardsText.text = GetRewardsText();
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
		Debug.Log(currentAmount);
		if (IsCompleted) {
			QuestManager.Instance.NotifyQuestCompleted(transform.GetSiblingIndex());
		}
	}

}
