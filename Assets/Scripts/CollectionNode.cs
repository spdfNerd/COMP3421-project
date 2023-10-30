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
			if (node.tower != null) {
				chef = node.tower.GetComponent<Chef>();
			}
			if (chef != null) {
				other.AddItem(chef.foodType, chef.FoodCount);
				chef.ResetFoodCount();
			}
		}
	}

}
