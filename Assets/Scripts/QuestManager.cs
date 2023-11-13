using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public Quest firstQuest; // index 0
    public Quest secondQuest; // index 1
    public Quest thirdQuest; // index 2
    public Quest[] questPool;

    [HideInInspector]
    public int[] usedIndices = new int[] { -1, -1, -1 };

    public Quest UpdateQuest(int i) {
        int index = Random.Range(0, questPool.Length);
        while (usedIndices.Contains(index)) {
            index = Random.Range(0, questPool.Length);
        }

        usedIndices[i] = index;
        return questPool[index];
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

}
