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
	/// <param name="type">The type of food</param>
	/// <param name="count">The amount of food to add</param>
	public void AddItem(FoodType type, int count = 1) {
		inventory[type] += count;
	}

	public int GetItemCount(FoodType foodType) {
		return inventory[foodType];
	}

	/// <summary>
	/// Set the amount of the specified food in the inventory
	/// </summary>
	/// <param name="foodType">The type of food to be set</param>
	/// <param name="foodCount">The amount of food to be set</param>
	public void SetItemCount(FoodType foodType, int foodCount) {
		inventory[foodType] = foodCount;
	}

	private void Init() {
		inventory = new Dictionary<FoodType, int>();
		foreach (FoodType type in Enum.GetValues(typeof(FoodType))) {
			inventory.Add(type, 0);
		}
	}

}
