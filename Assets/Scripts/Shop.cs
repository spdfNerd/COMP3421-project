using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;

    [HideInInspector]
	public GameObject[] kitchenStaff;
    [HideInInspector]
    public Button waitStaff;
    public Button buyButton;
    public Button sellButton;
    public GameObject rotateButtonPrefab;
    public GameObject upgradeButtonPrefab;
    
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

        StaffCosts costs = new StaffCosts();
		if (chefComponent != null) {
            costs = chefComponent.costs;
		} else if (waiterComponent != null) {
			costs = waiterComponent.costs;
		} else if (fridgeComponent != null) {
			costs = fridgeComponent.costs;
		}

		if (BuildManager.Instance.CheckCanBuild(costs.hirePrice)) {
			Player.Instance.currentNode.BuildTower(towerToBuild, upgradedTowerToBuild, costs);
		}
	}

    public void Rotate () {
        if (Player.Instance.currentNode.tower != null) {
            GameObject towerGFX = Player.Instance.currentNode.tower.transform.Find("GFX").gameObject;
            towerGFX.transform.Rotate(0, 90, 0); 
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
        } else {
            buyButton.interactable = false;
            sellButton.interactable = true;
        }

        if (Player.Instance.currentNode.upgradeButton) {
            if (Player.Instance.currentNode.isUpgraded) {
                Player.Instance.currentNode.upgradeButton.GetComponent<Button>().interactable = false;
            } else {
                Player.Instance.currentNode.upgradeButton.GetComponent<Button>().interactable = true;
            }
        }
    }

}

[System.Serializable]
public class StaffCosts {

    public int hirePrice;
    public int sellPrice;
    public int runningCost;
    public int upgradePrice;

    public StaffCosts() {
        hirePrice = 0;
        sellPrice = 0;
        runningCost = 0;
        upgradePrice = 0;
    }

}
