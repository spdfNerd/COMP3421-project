using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	public Transform baseTowerPrefab;

	private int hirePrice;
	private Image image;
	private Button button;

	public bool interactable {
		get => button.interactable;
		set => button.interactable = value;
	}

	public bool IsAffordable {
		get => hirePrice <= LevelManager.Instance.Money;
	}

	private void Awake() {
		button = GetComponent<Button>();
	}

	private void Start() {
		if (baseTowerPrefab == null) {
			Debug.LogError("Tower prefab not set!");
			return;
		}

		Staff staffComponent = baseTowerPrefab.GetComponent<Staff>();
		Fridge fridgeComponent = baseTowerPrefab.GetComponent<Fridge>();
		if (staffComponent != null) {
			hirePrice = staffComponent.costs.hirePrice;
		} else if (fridgeComponent != null) {
			hirePrice = fridgeComponent.costs.hirePrice;
		} else {
			hirePrice = int.MaxValue;
		}

		image = GetComponent<Image>();
	}

	public void SetSelected() {
		SetSelected(true);
	}

	public void SetSelected(bool selected) {
		BuildManager.Instance.SetTowerToBuild(selected ? baseTowerPrefab : null);
		image.color = selected ? button.colors.selectedColor : button.colors.normalColor;
		Shop.Instance.SetSelectedButton(selected ? this : null);
	}

}
