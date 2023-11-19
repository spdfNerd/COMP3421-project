using UnityEngine;

public class CollectionNode : MonoBehaviour {

	// Singleton instance
	public static CollectionNode Instance;

	private void Awake() {
		// Ensure unique singleton instance in scene
		if (Instance != null) {
			Debug.LogError("More than one CollectionNode in scene!");
			return;
		}
		Instance = this;
	}

	/// <summary>
	/// Transfer all food items from the chefs and fridges in the kitchen area
	/// to the specified inventory
	/// </summary>
	/// <param name="other">Inventory to transfer items into</param>
	public void TransferKitchenInventory(Inventory other) {
		Node[] nodes = transform.parent.GetComponentsInChildren<Node>();
		foreach (Node node in nodes) {
			Chef chef = null;
			Fridge fridge = null;
			
			// Grab component of tower if the tower exists
			if (node.tower != null) {
				chef = node.tower.GetComponent<Chef>();
				fridge = node.tower.GetComponent<Fridge>();
			}

			// Transfer all items from chef/fridge to player
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
