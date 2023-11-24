using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;
    public const string KitchenNodeTag = "KitchenNode";
    private const string KitchenStaffTag = "KitchenStaff";
    private const string WaitStaffTag = "WaitStaff";

    [HideInInspector]
	public GameObject[] kitchenStaffButtons;
    [HideInInspector]
    public Button waitStaffButton;
    public Button buyButton;
    public Button sellButton;
    public GameObject upgradePanel;

    private ShopButton selectedButton;

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

	public void SelectPizzaTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.PizzaTowerString);
        SetSelectedButton(button);
	}

	public void SelectBurgerTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.BurgerTowerString);
		SetSelectedButton(button);
	}

	public void SelectSushiTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.SushiTowerString);
		SetSelectedButton(button);
	}

	public void SelectNoodlesTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.NoodlesTowerString);
		SetSelectedButton(button);
	}

	public void SelectWaiterTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.WaiterTowerString);
		SetSelectedButton(button);
	}

	public void SelectFridgeTower(ShopButton button) {
		BuildManager.Instance.SetTowerToBuild(BuildManager.FridgeTowerString);
		SetSelectedButton(button);
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

        BuildManager.Instance.BuildTower(costs);
	}

	public void SellTower() {
        Player.Instance.currentNode.SellTower();
	}

	/// <summary>
    /// Check to see which shop object should be enabled depending on player location
    /// </summary>
	public void UpdateButtons(Node node, bool isKitchenNode) {
        // Set buttons to be enabled depending on tower type and where player is
        waitStaffButton.interactable = !isKitchenNode;
        foreach (GameObject staffButton in kitchenStaffButtons) {
            staffButton.GetComponent<Button>().interactable = isKitchenNode;
        }

        bool hasStaff = node.tower != null;
		buyButton.interactable = !hasStaff;
        sellButton.interactable = hasStaff;

        if (node.upgradeButton) {
            node.upgradeButton.GetComponent<Button>().interactable = node.isUpgraded;
        }
	}

	public void ClearSelectedTower() {
		if (selectedButton != null) {
			selectedButton.SetSelected(false);
			selectedButton = null;
		}

		BuildManager.Instance.SetTowerToBuild(BuildManager.NoTowerString);
	}

	private void SetSelectedButton(ShopButton button) {
        button.SetSelected(true);
        selectedButton = button;
    }

}
