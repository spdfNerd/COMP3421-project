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
	public int reputationReward;

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
    
}
