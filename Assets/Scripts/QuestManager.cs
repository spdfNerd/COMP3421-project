using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance;

	public Transform questsPanel;
    public Transform[] questPool;

    [HideInInspector]
    public int[] usedIndices;
	[HideInInspector]
	public QuestType[] questTypes;
	private Transform[] currentQuests;

	private void Awake() {
		if (Instance != null) {
			Debug.Log("More than one QuestManager in scene!");
			return;
		}
		Instance = this;
	}

	private void Start() {
		if (!LevelManager.Instance.isEndlessMode) {
			enabled = false;
			return;
		}

		currentQuests = new Transform[3];
		// Set the initial quests to be the first three in the quest pool
		usedIndices = new int[] { 0, 1, 2 };
		questTypes = new QuestType[currentQuests.Length];

		InitCurrentQuests();
	}

	public void UpdateQuest(int index) {
		int newQuestIndex = Random.Range(0, questPool.Length);
		// Reroll quest index until a not-in-use index is obtained
		while (usedIndices.Contains(newQuestIndex)) {
			newQuestIndex = Random.Range(0, questPool.Length);
		}
		usedIndices[index] = newQuestIndex;

		Destroy(currentQuests[index].gameObject);
		Transform newQuest = Instantiate(questPool[newQuestIndex], questsPanel);
		// Place quest in the same place as the just-completed quest
		newQuest.SetSiblingIndex(index);
		questTypes[index] = newQuest.GetComponent<Quest>().questType;
		currentQuests[index] = newQuest;
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

	private void InitCurrentQuests() {
		for (int i = 0; i < usedIndices.Length; i++) {
			currentQuests[i] = Instantiate(questPool[i], questsPanel);
			questTypes[i] = currentQuests[i].GetComponent<Quest>().questType;
		}
	}

	private void TryUpdateQuestProgress(QuestType questType, int amount,
		FoodType foodType = FoodType.PIZZA, CustomerType customerType = CustomerType.ADULT) {
		for (int i = 0; i < questTypes.Length; i++) {
			if (questTypes[i] != questType) {
				continue;
			}

			Quest quest = currentQuests[i].GetComponent<Quest>();
			// Only progress quest if required conditions match
			if (questType == QuestType.SERVE_FOOD && quest.foodType != foodType) {
				continue;
			} else if (questType == QuestType.SERVE_CUSTOMER && quest.customerType != customerType) {
				continue;
			} else {
				quest.AddToQuestProgress(amount);
			}
		}
	}

}
