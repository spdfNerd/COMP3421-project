using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public Quest firstQuest; // index 0
    public Quest secondQuest; // index 1
    public Quest thirdQuest; // index 2
    public Quest[] questPool;
    [HideInInspector]
    public int[] usedIndices = new int[] { -1, -1, -1 };

    // Counters for quest
    public int firstQuestCounter = 0;
    public int secondQuestCounter = 0;
    public int thirdQuestCounter = 0;

    public Quest UpdateQuest(int i) {
        int index = Random.Range(0, questPool.Length);
        while (usedIndices.Contains(index)) {
            index = Random.Range(0, questPool.Length);
        }

        usedIndices[i] = index;
        return questPool[index];
    }

    public void Update() {
        if (firstQuest.isCompleted) {
			firstQuest = UpdateQuest(0);
		}
        if (secondQuest.isCompleted) {
			secondQuest = UpdateQuest(1);
		}
        if (thirdQuest.isCompleted) {
			thirdQuest = UpdateQuest(2);
		}
    }

}
