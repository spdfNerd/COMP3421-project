using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string quest_name;
    public int point_reward;
    public QuestType type;
    public bool is_completed = false;
    public int required_amount;
    public int current_amount;

    public void Update() { 
        if (current_amount >= required_amount)
        {
            is_completed = true;
        }
    }
    
}
