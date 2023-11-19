using System;
using System.Collections.Generic;

public class Inventory {

	private Dictionary<FoodType, int> inventory;

	public Inventory() {
		Init();
	}

	/// <summary>
	/// Add specified amount of food to inventory
	/// </summary>
	/// <param name="type">Type of food</param>
	/// <param name="count">Amount of food to add</param>
	public void AddItem(FoodType type, int count = 1) {
		inventory[type] += count;
	}

	/// <summary>
	/// Set the amount of the type of food to 0
	/// </summary>
	/// <param name="type">The type of food to clear</param>
	public void ClearItem(FoodType type) {
		inventory[type] = 0;
	}

	public int GetItemCount(FoodType foodType) {
		return inventory[foodType];
	}

	private void Init() {
		inventory = new Dictionary<FoodType, int>();
		foreach (FoodType type in Enum.GetValues(typeof(FoodType))) {
			inventory.Add(type, 0);
		}
	}

}
