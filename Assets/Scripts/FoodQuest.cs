using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodQuest : Quest
{
    public FoodType food_type;

    FoodQuest(int amount, FoodType f_type) {
        this.type = QuestType.SERVE;
        this.required_amount = amount;
        food_type = f_type;
    }
}
