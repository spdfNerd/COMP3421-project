using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashQuest : Quest
{
    CashQuest(int amount) {
        this.type = QuestType.SPEND;
        this.required_amount = amount;
    }
}
