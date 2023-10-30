using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

	public static BuildManager Instance;

	public Transform sushiTowerPrefab;
	public Transform burgerTowerPrefab;
	public Transform pizzaTowerPrefab;
	public Transform noodlesTowerPrefab;
	public Transform waiterTowerPrefab;
	public Transform fridgeTowerPrefab;

	[HideInInspector]
	public Transform towerToBuild;

	private void Awake() {
		if (Instance != null) {
			Debug.LogError("More than one BuildManagers in scene!");
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

}
