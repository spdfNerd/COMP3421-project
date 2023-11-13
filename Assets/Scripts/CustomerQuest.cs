using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQuest : Quest
{
    public CustomerType cust_type;

    CustomerQuest(int amount, CustomerType c_type)
    {
        this.type = QuestType.SERVE;
        this.required_amount = amount;
        this.cust_type = c_type;
    }
}
