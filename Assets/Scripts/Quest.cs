[System.Serializable]
public class Quest {

    public string questName;
    public int pointReward;
    public QuestType type;
    public bool isCompleted = false;
    public int requiredAmount;
    public int currentAmount;

    public void UpdateCurrentAmount(int newAmount) {
		currentAmount = newAmount;
	}
    
}

public class CashQuest : Quest {

	public CashQuest(int amount) {
		type = QuestType.SPEND;
		requiredAmount = amount;
	}

}

public class CustomerQuest : Quest {

	public CustomerType customerType;

	public CustomerQuest(int amount, CustomerType customerType) {
		type = QuestType.SERVE;
		requiredAmount = amount;
		this.customerType = customerType;
	}

}

public class FoodQuest : Quest {
	
	public FoodType foodType;

	public FoodQuest(int amount, FoodType foodType) {
		type = QuestType.SERVE;
		requiredAmount = amount;
		this.foodType = foodType;
	}

}
