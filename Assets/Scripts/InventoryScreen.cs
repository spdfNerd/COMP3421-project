using System;
using TMPro;
using UnityEngine;

public class InventoryScreen : MonoBehaviour {

	public Transform selectionIndicator;
	public Transform[] foodIcons;

	private int selectedItem;
	private TextMeshProUGUI[] foodCountTexts;

	public int SelectedItem {
		get => selectedItem;
		set {
			selectedItem = value;
			UpdateSelectedFood(value);
		}
	}

	private void Start() {
		foodCountTexts = new TextMeshProUGUI[foodIcons.Length];
		for (int i = 0; i < foodIcons.Length; i++) {
			foodCountTexts[i] = foodIcons[i].GetChild(0).GetComponent<TextMeshProUGUI>();
		}
	}

	private void Update() {
		if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1)) {
			SelectedItem = 0;
		} else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2)) {
			SelectedItem = 1;
		} else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3)) {
			SelectedItem = 2;
		} else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4)) {
			SelectedItem = 3;
		} else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5)) {
			SelectedItem = 4;
		} else if (Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Keypad6)) {
			SelectedItem = 5;
		}
	}

	public void UpdateSelectedFood(int slot) {
		selectedItem = slot;
		selectionIndicator.position = foodIcons[slot].position;
	}

	public int GetCurrentItemCount() {
		return int.Parse(foodCountTexts[selectedItem].text);
	}

	public void UpdateTexts(Inventory inventory) {
		for (int i = 0; i < Enum.GetValues(typeof(FoodType)).Length; i++) {
			foodCountTexts[i].text = inventory.GetItemCount((FoodType) i).ToString();
		}
	}

	public void SetTextToZero(FoodType type) {
		foodCountTexts[(int) type].text = "0";
	}

	public void SetAllToZero() {
		foreach (TextMeshProUGUI text in foodCountTexts) {
			text.text = "0";
		}
	}

}
