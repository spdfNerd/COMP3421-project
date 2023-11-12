using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string quest_name;
    public int point_reward;
    public QuestType type;
    public bool is_completed;
    
}

public enum QuestType { 
    Spend, // Spend some amount of cash
    Serve, // Serve a type of food or a type of customer
}
