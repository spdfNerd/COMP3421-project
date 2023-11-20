using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;
    private static readonly string KitchenStaffTag = "KitchenStaff";
    private static readonly string WaitStaffTag = "WaitStaff";
    private static readonly string KitchenNodeTag = "KitchenNode";

    [HideInInspector]
	public GameObject[] kitchenStaffButtons;
    [HideInInspector]
    public Button waitStaffButton;
    public Button buyButton;
    public Button sellButton;
    public GameObject upgradePanel;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Shop in scene!");
            return;
        }
        Instance = this;
    }

    private void Start() {
        kitchenStaffButtons = GameObject.FindGameObjectsWithTag(KitchenStaffTag);
        waitStaffButton = GameObject.FindWithTag(WaitStaffTag).GetComponent<Button>();
    }

    private void Update() {
        UpdateShopButtons();
    }

	public void BuyTower() {
        Transform towerToBuild = BuildManager.Instance.towerToBuild;
		if (towerToBuild == null) {
			return;
		}

        Staff staffComponent = towerToBuild.GetComponent<Staff>();
		Fridge fridgeComponent = towerToBuild.GetComponent<Fridge>();

        StaffCosts costs;
		if (fridgeComponent != null) {
			costs = fridgeComponent.costs;
		} else {
            costs = staffComponent.costs;
        }

		if (BuildManager.Instance.CheckCanBuild(costs.hirePrice)) {
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

	public void SellTower() {
        Player.Instance.currentNode.SellTower();
	}

	/// <summary>
    /// Check to see which shop object should be displayed depending on player location
    /// </summary>
	private void UpdateShopButtons() {
        Node node = Player.Instance.currentNode;
        if (node == null) {
            return;
        }
        
        bool isKitchenNode = node.CompareTag(KitchenNodeTag);
        // Set buttons to be active depending on tower type and where player is
        waitStaffButton.interactable = !isKitchenNode;
        foreach (GameObject staffButton in kitchenStaffButtons) {
            staffButton.GetComponent<Button>().interactable = isKitchenNode;
        }

        // Clear selected tower if player moves between kitchen and normal nodes
        Transform selectedTower = BuildManager.Instance.towerToBuild;
        if (selectedTower != null) {
            bool waiterInKitchen = isKitchenNode && selectedTower.CompareTag(WaitStaffTag);
            bool chefNotInKitchen = !isKitchenNode && selectedTower.CompareTag(KitchenStaffTag);
            if (waiterInKitchen || chefNotInKitchen) {
                LevelManager.Instance.SetTowerToBuild(null);
            }
        }

        bool hasStaff = node.tower != null;
		buyButton.interactable = !hasStaff;
        sellButton.interactable = hasStaff;

        if (node.upgradeButton) {
            node.upgradeButton.GetComponent<Button>().interactable = node.isUpgraded;
        }
    }

}
