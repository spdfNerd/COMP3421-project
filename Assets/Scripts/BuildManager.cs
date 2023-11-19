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

	[Header("Upgraded Tower Models")]
	public Transform upgradedSushiTowerPrefab;
	public Transform upgradedBurgerTowerPrefab;
	public Transform upgradedPizzaTowerPrefab;
	public Transform upgradedNoodlesTowerPrefab;
	public Transform upgradedWaiterTowerPrefab;

	[HideInInspector]
	public Transform towerToBuild;
	[HideInInspector]
	public Transform upgradedTowerToBuild;

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
		upgradedTowerToBuild = upgradedSushiTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectBurgerTower(ShopButton button) {
		towerToBuild = burgerTowerPrefab;
		upgradedTowerToBuild = upgradedBurgerTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectPizzaTower(ShopButton button) {
		towerToBuild = pizzaTowerPrefab;
		upgradedTowerToBuild = upgradedPizzaTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectNoodlesTower(ShopButton button) {
		towerToBuild = noodlesTowerPrefab;
		upgradedTowerToBuild = upgradedNoodlesTowerPrefab;
		button.ToggleSelect();
	}

	public void SelectWaiterTower(ShopButton button) {
		towerToBuild = waiterTowerPrefab;
		upgradedTowerToBuild = upgradedWaiterTowerPrefab;
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
