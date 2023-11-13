using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

	public static QuestManager Instance;

    public Quest firstQuest;
    public Quest secondQuest;
    public Quest thirdQuest;
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
		usedIndices = new int[]  { -1, -1, -1 };
		questTypes = new QuestType[] { firstQuest.questType, secondQuest.questType, thirdQuest.questType };

		DisplayQuest(firstQuest, 0);
		DisplayQuest(secondQuest, 1);
		DisplayQuest(thirdQuest, 2);
	}

	public void Update() {
        if (firstQuest.IsCompleted) {
			firstQuest = UpdateQuest(0);
			questTypes[0] = firstQuest.questType;
		}
        if (secondQuest.IsCompleted) {
			secondQuest = UpdateQuest(1);
			questTypes[1] = secondQuest.questType;
		}
        if (thirdQuest.IsCompleted) {
			thirdQuest = UpdateQuest(2);
			questTypes[2] = thirdQuest.questType;
		}
	}

	public Quest UpdateQuest(int index) {
		int newQuestIndex = Random.Range(0, questPool.Length);
		while (usedIndices.Contains(newQuestIndex)) {
			newQuestIndex = Random.Range(0, questPool.Length);
		}
		usedIndices[index] = newQuestIndex;

		Quest newQuest = questPool[newQuestIndex];
		DisplayQuest(newQuest, index);
		return newQuest;
	}

	private void DisplayQuest(Quest quest, int index) {
		Transform newQusetTransform = Instantiate(quest.transform, questsPanel);
		newQusetTransform.SetSiblingIndex(index);
	}

}
