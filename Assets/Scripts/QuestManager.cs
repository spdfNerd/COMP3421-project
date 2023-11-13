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
		usedIndices = new int[] { 0, 1, 2 };
		questTypes = new QuestType[currentQuests.Length];

		InitCurrentQuests();
	}

	public void UpdateQuest(int index) {
		int newQuestIndex = Random.Range(0, questPool.Length);
		while (usedIndices.Contains(newQuestIndex)) {
			newQuestIndex = Random.Range(0, questPool.Length);
		}
		usedIndices[index] = newQuestIndex;

		Destroy(currentQuests[index].gameObject);
		Transform newQuest = Instantiate(questPool[newQuestIndex], questsPanel);
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
