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
	private bool isCompleted = false;

	public int CurrentAmount {
		get => currentAmount;
		set => currentAmount = value;
	}

	public bool IsCompleted {
		get => isCompleted;
		set => isCompleted = value;
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

}
