using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton instance
	public static BuildManager Instance;

	[Header("Base Tower Models")]
	public Transform sushiTowerPrefab;
	public Transform burgerTowerPrefab;
	public Transform pizzaTowerPrefab;
	public Transform noodlesTowerPrefab;
	public Transform waiterTowerPrefab;
	public Transform fridgeTowerPrefab;

	[HideInInspector]
	public Transform towerToBuild;

	private void Awake() {
		// Ensure unique singleton instance in scene
		if (Instance != null) {
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		Instance = this;
	}

	public void SelectSushiTower(ShopButton button) {
		towerToBuild = sushiTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectBurgerTower(ShopButton button) {
		towerToBuild = burgerTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectPizzaTower(ShopButton button) {
		towerToBuild = pizzaTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectNoodlesTower(ShopButton button) {
		towerToBuild = noodlesTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectWaiterTower(ShopButton button) {
		towerToBuild = waiterTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectFridgeTower(ShopButton button) {
		towerToBuild = fridgeTowerPrefab;
		button.ToggleSelect();
	}

	public bool CheckCanBuild(int hirePrice) {
		// Check if there is already a tower on the tile
		if (Player.Instance.currentNode.tower != null) {
			Debug.Log("Can't build there");
			return false;
		}

		// Check that there is enough money to build
		if (LevelManager.Instance.Money < hirePrice) {
			Debug.Log("Not enough money to build that!");
			return false;
		}

		return true;
	}

	public bool CanUpgrade(int upgradePrice) {
		// Check that there is a tower on the tile
		if (Player.Instance.currentNode.tower == null) {
			Debug.Log("Can't upgrade a nonexistant tower");
			return false;
		}

		// Check that the tower is not already upgraded
		if (Player.Instance.currentNode.isUpgraded) {
			Debug.Log("Already upgraded");
			return false;
		}

		// Check that there is enough money to upgrade
		if (LevelManager.Instance.Money < upgradePrice) {
			Debug.Log("Not enough money to upgrade that!");
			return false;
		}

		return true;
	}

}
