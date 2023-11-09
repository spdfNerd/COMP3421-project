using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;

    public GameObject[] kitchenStaff;
    public Button waitStaff;
    public Button buyButton;
    public Button sellButton;
    public Button upgradeButton;
    
    private GameObject selectedTower;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("More than one Shop in scene!");
            return;
        }
        Instance = this;
    }

    private void Start() {
        kitchenStaff = GameObject.FindGameObjectsWithTag("KitchenStaff");
        waitStaff = GameObject.FindWithTag("WaitStaff").GetComponent<Button>();
    }

    private void Update() {
        CheckNode();
    }

	public void BuyTower() {
        Transform towerToBuild = BuildManager.Instance.towerToBuild;
        Transform upgradedTowerToBuild = BuildManager.Instance.upgradedTowerToBuild;
		if (towerToBuild == null) {
			return;
		}

		Chef chefComponent = towerToBuild.GetComponent<Chef>();
		Waiter waiterComponent = towerToBuild.GetComponent<Waiter>();
		Fridge fridgeComponent = towerToBuild.GetComponent<Fridge>();

		int hirePrice, sellPrice, runningCost, upgradePrice;
		if (chefComponent != null) {
			hirePrice = chefComponent.hirePrice;
			sellPrice = chefComponent.sellPrice;
			runningCost = chefComponent.runningCost;
            upgradePrice = chefComponent.upgradePrice;
		} else if (waiterComponent != null) {
			hirePrice = waiterComponent.hirePrice;
			sellPrice = waiterComponent.sellPrice;
			runningCost = waiterComponent.runningCost;
            upgradePrice = waiterComponent.upgradePrice;
		} else if (fridgeComponent != null) {
			hirePrice = fridgeComponent.hirePrice;
			sellPrice = fridgeComponent.sellPrice;
			runningCost = fridgeComponent.runningCost;
            upgradePrice = fridgeComponent.upgradePrice;
		} else {
			hirePrice = 0;
			sellPrice = 0;
			runningCost = 0;
            upgradePrice = 0;
		}

		if (BuildManager.Instance.CheckCanBuild(hirePrice)) {
			Player.Instance.currentNode.BuildTower(towerToBuild, upgradedTowerToBuild, hirePrice, sellPrice, runningCost, upgradePrice);
		}
	}

    public void UpgradeTower() {
        Player.Instance.currentNode.UpgradeTower();
    }

	public void SellTower() {
        Player.Instance.currentNode.SellTower();
	}

	// Check to see which shop object should be displayed depending on player location
	private void CheckNode() {
        if (Player.Instance.currentNode == null) {
            return;
        }

        if (Player.Instance.currentNode.tag == "KitchenNode") {
            waitStaff.interactable = false;
            foreach (GameObject staff in kitchenStaff) {
                staff.GetComponent<Button>().interactable = true;
            }
            if (selectedTower != null && selectedTower.tag != "KitchenStaff") {
                selectedTower = null;
                LevelManager.Instance.SetTowerToBuild(null);
            }
        } else {
            waitStaff.interactable = true;
            foreach (GameObject staff in kitchenStaff) {
                staff.GetComponent<Button>().interactable = false;
            }
            if (selectedTower != null && selectedTower.tag != "WaitStaff") {
                selectedTower = null;
				LevelManager.Instance.SetTowerToBuild(null);
            }
        }

        if (Player.Instance.currentNode.tower == null) {
            buyButton.interactable = true;
            sellButton.interactable = false;
            upgradeButton.interactable = false;
        } else {
            buyButton.interactable = false;
            sellButton.interactable = true;
            if (Player.Instance.currentNode.isUpgraded) {
                upgradeButton.interactable = false;
            } else {
                upgradeButton.interactable = true;
            }
        }
    }

}
