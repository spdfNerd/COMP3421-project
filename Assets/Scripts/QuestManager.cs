using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public Quest firstQuest; // index 0
    public Quest secondQuest; // index 1
    public Quest thirdQuest; // index 2
    public Transform questsPanel;

    public Quest[] questPool;

    [HideInInspector]
    public int[] usedIndices = new int[] { -1, -1, -1 };

	private void Start() {
		DisplayQuest(firstQuest, 0);
		DisplayQuest(secondQuest, 1);
		DisplayQuest(thirdQuest, 2);
	}

	public void Update() {
        if (firstQuest.IsCompleted) {
			firstQuest = UpdateQuest(0);
		}
        if (secondQuest.IsCompleted) {
			secondQuest = UpdateQuest(1);
		}
        if (thirdQuest.IsCompleted) {
			thirdQuest = UpdateQuest(2);
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
