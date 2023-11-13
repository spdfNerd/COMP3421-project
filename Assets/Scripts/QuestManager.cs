using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance;

	public Quest[] currentQuests;
    public Transform questsPanel;

    public Quest[] questPool;

    [HideInInspector]
    public int[] usedIndices;
	[HideInInspector]
	public QuestType[] questTypes;

	private void Awake() {
		if (Instance != null) {
			Debug.Log("More than one QuestManager in scene!");
			return;
		}
		Instance = this;
	}

	private void Start() {
		usedIndices = new int[] { -1, -1, -1 };
		questTypes = new QuestType[currentQuests.Length];

		for (int i = 0; i < currentQuests.Length; i++) {
			questTypes[i] = currentQuests[i].questType;
			DisplayQuest(currentQuests[i], i, false);
		}
	}

	public void UpdateQuest(int index) {
		int newQuestIndex = Random.Range(0, questPool.Length);
		while (usedIndices.Contains(newQuestIndex)) {
			newQuestIndex = Random.Range(0, questPool.Length);
		}
		usedIndices[index] = newQuestIndex;

		Quest newQuest = questPool[newQuestIndex];
		questTypes[index] = newQuest.questType;
		currentQuests[index] = newQuest;
		DisplayQuest(currentQuests[index], index);
	}

	public void NotifyQuestCompleted(int index) {
		UpdateQuest(index);
	}

	public void TryUpdateSpendQuestProgress(int amount) {
		TryUpdateQuestProgress(QuestType.SPEND, amount);
	}

	public void TryUpdateServeFoodQuestProgress(FoodType foodType, int amount = 1) {
		TryUpdateQuestProgress(QuestType.SERVE_FOOD, amount, foodType);
	}

	public void TryUpdateServeCustomerQuestProgress(CustomerType customerType, int amount = 1) {
		TryUpdateQuestProgress(QuestType.SERVE_CUSTOMER, amount, FoodType.PIZZA, customerType);
	}

	private void DisplayQuest(Quest quest, int index, bool isReplacing = true) {
		if (isReplacing) {
			GameObject oldQuest = questsPanel.GetChild(index).gameObject;
			if (oldQuest != null) {
				Destroy(oldQuest);
			}
		}

		Transform newQusetTransform = Instantiate(quest.transform, questsPanel);
		newQusetTransform.SetSiblingIndex(index);
	}

	private void TryUpdateQuestProgress(QuestType questType, int amount,
		FoodType foodType = FoodType.PIZZA, CustomerType customerType = CustomerType.ADULT) {
		for (int i = 0; i < questTypes.Length; i++) {
			if (questTypes[i] != questType) {
				continue;
			}

			Quest quest = currentQuests[i];
			if (questType == QuestType.SERVE_FOOD && quest.foodType != foodType) {
				continue;
			} else if (questType == QuestType.SERVE_CUSTOMER && quest.customerType != customerType) {
				continue;
			} else {
				quest.AddToProgress(amount);
			}
		}
	}

}
