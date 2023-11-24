using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton instance
	public static BuildManager Instance;

	public const string PizzaTowerString = "pizza";
	public const string BurgerTowerString = "burger";
	public const string SushiTowerString = "sushi";
	public const string NoodlesTowerString = "noodles";
	public const string WaiterTowerString = "waiter";
	public const string FridgeTowerString = "fridge";
	public const string NoTowerString = "none";

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

	public void SetTowerToBuild(string type) {
		switch (type) {
			case PizzaTowerString:
				towerToBuild = sushiTowerPrefab;
				break;
			case BurgerTowerString:
				towerToBuild = burgerTowerPrefab;
				break;
			case SushiTowerString:
				towerToBuild = sushiTowerPrefab;
				break;
			case NoodlesTowerString:
				towerToBuild = noodlesTowerPrefab;
				break;
			case WaiterTowerString:
				towerToBuild = waiterTowerPrefab;
				break;
			case FridgeTowerString:
				towerToBuild = fridgeTowerPrefab;
				break;
			case NoTowerString:
			default:
				towerToBuild = null;
				break;
		}
	}

	public void BuildTower(StaffCosts costs) {
		if (CheckCanBuild(costs.hirePrice)) {
			Player.Instance.currentNode.BuildTower(towerToBuild, costs);
		}
	}

	public void Rotate() {
		if (Player.Instance.GetCurrentTowerTransform() != null) {
			Staff staff = Player.Instance.GetCurrentTowerTransform().GetComponent<Staff>();
			if (staff == null) {
				return;
			}
			staff.GetActiveGFX().transform.Rotate(0, 90, 0);
		}
	}

	public void UpgradeTower() {
		Player.Instance.currentNode.UpgradeTower();
	}

	public bool CheckCanBuild(int hirePrice) {
		// Check if there is already a tower on the tile
		if (Player.Instance.GetCurrentTowerTransform() != null) {
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
		if (Player.Instance.GetCurrentTowerTransform() == null) {
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
