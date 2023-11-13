using UnityEngine;

public class CollectionNode : MonoBehaviour {

	public static CollectionNode Instance;

	private void Awake() {
		if (Instance != null) {
			Debug.LogError("More than one CollectionNode in scene!");
			return;
		}
		Instance = this;
	}

	public void TransferInventory(Inventory other) {
		Node[] nodes = transform.parent.GetComponentsInChildren<Node>();
		foreach (Node node in nodes) {
			Chef chef = null;
			Fridge fridge = null;
			if (node.tower != null) {
				chef = node.tower.GetComponent<Chef>();
				fridge = node.tower.GetComponent<Fridge>();
			}

			if (chef != null) {
				other.AddItem(chef.foodType, chef.FoodCount);
				chef.ResetFoodCount();
			} else if (fridge != null) {
				other.AddItem(FoodType.COKE, fridge.CokeCount);
				other.AddItem(FoodType.WATER, fridge.WaterCount);
				fridge.ResetDrinksCount();
			}
		}
	}

}
