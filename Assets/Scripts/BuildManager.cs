using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public static BuildManager Instance;

	public Transform sushiTowerPrefab;
	public Transform upgradedSushiTowerPrefab;
	public Transform burgerTowerPrefab;
	public Transform upgradedBurgerTowerPrefab;
	public Transform pizzaTowerPrefab;
	public Transform upgradedPizzaTowerPrefab;
	public Transform noodlesTowerPrefab;
	public Transform upgradedNoodlesTowerPrefab;
	public Transform waiterTowerPrefab;
	public Transform upgradedWaiterTowerPrefab;
	public Transform fridgeTowerPrefab;

	[HideInInspector]
	public Transform towerToBuild;
	[HideInInspector]
	public Transform upgradedTowerToBuild;

	private void Awake() {
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
		// if there is already a tower on the tile
		if (Player.Instance.currentNode.tower != null) {
			Debug.Log("Can't build there");
			return false;
		}

		if (LevelManager.Instance.Money < hirePrice) {
			Debug.Log("Not enough money to build that!");
			return false;
		}

		return true;
	}

	public bool CheckCanUpgrade(int upgradePrice) {
		// if there is no tower on the tile
		if (Player.Instance.currentNode.tower == null) {
			Debug.Log("Can't upgrade a nonexistant tower");
			return false;
		}

		if (LevelManager.Instance.Money < upgradePrice) {
			Debug.Log("Not enough money to upgrade that!");
			return false;
		}

		return true;
	}

}
