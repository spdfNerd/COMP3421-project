using System;
using System.Collections.Generic;

public class Inventory {

	private Dictionary<FoodType, int> inventory;

	public Inventory() {
		Init();
	}

	public void AddItem(FoodType type, int count = 1) {
		inventory[type] += count;
	}

	public void AddItems(Dictionary<FoodType, int> items) {
		foreach (FoodType type in items.Keys) {
			AddItem(type, items[type]);
		}
	}

	public void RemoveItem(FoodType type, int count) {
		inventory[type] -= count;
	}

	public void RemoveItems(Dictionary<FoodType, int> items) {
		foreach (FoodType type in items.Keys) {
			RemoveItem(type, items[type]);
		}
	}

	public void ClearItem(FoodType type) {
		inventory[type] = 0;
	}

	public int GetItemCount(FoodType foodType) {
		return inventory[foodType];
	}

	public void CopyTo(Inventory other, bool clearOtherFirst = false) {
		if (clearOtherFirst) {
			other.Clear();
		}

		foreach (FoodType type in inventory.Keys) {
			other.AddItem(type, inventory[type]);
		}
	}

	public void Clear() {
		foreach (FoodType key in inventory.Keys) {
			inventory[key] = 0;
		}
	}

	private void Init() {
		inventory = new Dictionary<FoodType, int>();
		foreach (FoodType type in Enum.GetValues(typeof(FoodType))) {
			inventory.Add(type, 0);
		}
	}

}
