using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;
    public const string KitchenNodeTag = "KitchenNode";
    private const string KitchenStaffTag = "KitchenStaff";
    private const string WaitStaffTag = "WaitStaff";

    [HideInInspector]
	public ShopButton[] kitchenStaffButtons;
    [HideInInspector]
    public ShopButton waitStaffButton;
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
		SetKitchenStaffButtons();
        waitStaffButton = GameObject.FindWithTag(WaitStaffTag).GetComponent<ShopButton>();
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
		waitStaffButton.interactable = !isKitchenNode && waitStaffButton.IsAffordable;
        foreach (ShopButton staffButton in kitchenStaffButtons) {
            staffButton.interactable = isKitchenNode && staffButton.IsAffordable;
        }

        bool hasStaff = node.tower != null;
		buyButton.interactable = !hasStaff;
        sellButton.interactable = hasStaff;

        if (node.upgradeButton) {
            node.upgradeButton.GetComponent<Button>().interactable = node.isUpgraded;
        }
	}

	public void SetSelectedButton(ShopButton button) {
		selectedButton = button;
	}

	public void ClearSelectedTower() {
		if (selectedButton != null) {
			selectedButton.SetSelected(false);
		}
	}

	private void SetKitchenStaffButtons() {
		GameObject[] kitchenStaffButtonGOs = GameObject.FindGameObjectsWithTag(KitchenStaffTag);
		kitchenStaffButtons = new ShopButton[kitchenStaffButtonGOs.Length];
		for (int i = 0; i < kitchenStaffButtonGOs.Length; i++) {
			kitchenStaffButtons[i] = kitchenStaffButtonGOs[i].GetComponent<ShopButton>();
		}
	}

}
