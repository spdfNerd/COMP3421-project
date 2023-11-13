using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest first_quest; // index 0
    public Quest second_quest; // index 1
    public Quest third_quest; // index 2
    public Quest[] quests;
    public int[] used_indices = new int[] {-1,-1,-1};

    // Counters for quest
    public int first_quest_counter = 0;
    public int second_quest_counter = 0;
    public int third_quest_counter = 0;


    public Quest update_quest(int i) {
        int index = Random.Range(0, quests.Length);
        while (used_indices.Contains(index)) {
            index = Random.Range(0, quests.Length);
        }

        used_indices[i] = index;
        return quests[index];
    }

    public void Update()
    {
        if (first_quest.is_completed)
            first_quest = update_quest(0);
        if (second_quest.is_completed)
            second_quest = update_quest(1);
        if (third_quest.is_completed)
            third_quest = update_quest(2);
    }
}
