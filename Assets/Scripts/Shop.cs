using TMPro;
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

    public Transform upgradePanel;

	private Button rotateButton;
	private Button upgradeButton;
	private TextMeshProUGUI upgradeButtonText;

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

		rotateButton = upgradePanel.GetChild(0).GetComponent<Button>();
		upgradeButton = upgradePanel.GetChild(1).GetComponent<Button>();
		upgradeButtonText = upgradeButton.GetComponentInChildren<TextMeshProUGUI>(true);
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
	}

	public void SetSelectedButton(ShopButton button) {
		selectedButton = button;
	}

	public void ClearSelectedTower() {
		if (selectedButton != null) {
			selectedButton.SetSelected(false);
		}
	}

	public void EnableUpgradePanel(bool canUpgrade, int upgradePrice) {
		Transform towerTransform = Player.Instance.GetCurrentTowerTransform();

		// Set buttons position
		Vector3 position = towerTransform.position;
		position.y = upgradePanel.position.y;
		upgradePanel.position = position;

		// Link button functions so they upgrade and rotate tower when clicked
		rotateButton.onClick.AddListener(() => BuildManager.Instance.Rotate());
		upgradeButton.onClick.AddListener(() => BuildManager.Instance.UpgradeTower());

		upgradeButton.interactable = canUpgrade;
		upgradeButtonText.text = "$" + upgradePrice;

		upgradePanel.gameObject.SetActive(true);
	}

	public void DisableUpgradeButton() {
		upgradeButton.interactable = false;
	}

	public void DisableUpgradePanel() {
		// Make sure the buttons will not do anything relating to this node when clicked
		rotateButton.onClick.RemoveAllListeners();
		upgradeButton.onClick.RemoveAllListeners();

		upgradePanel.gameObject.SetActive(false);
	}

	private void SetKitchenStaffButtons() {
		GameObject[] kitchenStaffButtonGOs = GameObject.FindGameObjectsWithTag(KitchenStaffTag);
		kitchenStaffButtons = new ShopButton[kitchenStaffButtonGOs.Length];
		for (int i = 0; i < kitchenStaffButtonGOs.Length; i++) {
			kitchenStaffButtons[i] = kitchenStaffButtonGOs[i].GetComponent<ShopButton>();
		}
	}

}
