using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	public Dictionary<FoodType, int> inventory;

	private void Start() {
		InitInventory();
	}

	public void AddItem(FoodType type, int count) {
		inventory[type] += count;
	}

	public void AddItems(Tuple<FoodType, int>[] tuples) {
		foreach (Tuple<FoodType, int> tuple in tuples) {
			AddItem(tuple.Item1, tuple.Item2);
		}
	}

	public void RemoveItem(FoodType type, int count) {
		inventory[type] -= count;
	}

	public void RemoveItems(Tuple<FoodType, int>[] tuples) {
		foreach (Tuple<FoodType, int> tuple in tuples) {
			RemoveItem(tuple.Item1, tuple.Item2);
		}
	}

	public void ClearInventory() {
		foreach (FoodType key in inventory.Keys) {
			inventory[key] = 0;
		}
	}

	private void InitInventory() {
		inventory = new Dictionary<FoodType, int>();
		foreach (FoodType type in Enum.GetValues(typeof(FoodType))) {
			inventory.Add(type, 0);
		}
	}

}
