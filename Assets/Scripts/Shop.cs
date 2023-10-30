using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop Instance;

    public GameObject[] kitchenStaff;
    public Button waitStaff;
    public Button buyButton;
    public Button sellButton;
    
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
		if (towerToBuild == null) {
			return;
		}

		Chef chefComponent = towerToBuild.GetComponent<Chef>();
		Waiter waiterComponent = towerToBuild.GetComponent<Waiter>();
		Fridge fridgeComponent = towerToBuild.GetComponent<Fridge>();

		int hirePrice, sellPrice, runningCost;
		if (chefComponent != null) {
			hirePrice = chefComponent.hirePrice;
			sellPrice = chefComponent.sellPrice;
			runningCost = chefComponent.runningCost;
		} else if (waiterComponent != null) {
			hirePrice = waiterComponent.hirePrice;
			sellPrice = waiterComponent.sellPrice;
			runningCost = waiterComponent.runningCost;
		} else if (fridgeComponent != null) {
			hirePrice = fridgeComponent.hirePrice;
			sellPrice = fridgeComponent.sellPrice;
			runningCost = fridgeComponent.runningCost;
		} else {
			hirePrice = 0;
			sellPrice = 0;
			runningCost = 0;
		}

		if (BuildManager.Instance.CheckCanBuild(hirePrice)) {
			Player.Instance.currentNode.BuildTower(towerToBuild, hirePrice, sellPrice, runningCost);
		}
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
    }

}
