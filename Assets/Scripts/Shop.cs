using System;
using UnityEngine;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    LevelManager levelManager;
    Player player;

    public int sushiTowerHirePrice;
    public int sushiTowerSellPrice;
    public int sushiTowerRunningCost;
    public int burgerTowerHirePrice;
    public int burgerTowerSellPrice;
    public int burgerTowerRunningCost;
    public int pizzaTowerHirePrice;
    public int pizzaTowerSellPrice;
    public int pizzaTowerRunningCost;
    public int noodlesTowerHirePrice;
    public int noodlesTowerSellPrice;
    public int noodlesTowerRunningCost;
    public int waiterTowerHirePrice;
    public int waiterTowerSellPrice;
    public int waiterTowerRunningCost;
    public int fridgeTowerHirePrice;
    public int fridgeTowerSellPrice;
    public int fridgeTowerRunningCost;

    public GameObject[] kitchenStaff;
    public Button waitStaff;
    public Button buyButton;
    public Button sellButton;
    
    private GameObject selectedTower;
    private int selectedTowerHirePrice;
    private int selectedTowerSellPrice;
    private int selectedTowerRunningCost;


    void Start()
    {
<<<<<<< HEAD
        levelManager = LevelManager.Instance;
=======
        levelManager = LevelManager.instance;
        player = Player.instance;
        kitchenStaff = GameObject.FindGameObjectsWithTag("KitchenStaff");
        waitStaff = GameObject.FindWithTag("WaitStaff").GetComponent<Button>();
>>>>>>> master
    }

    public void SelectSushiTower () {
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
        selectedTower = GameObject.Find("SushiChef");
        selectedTowerHirePrice = sushiTowerHirePrice;
        selectedTowerSellPrice = sushiTowerSellPrice;
        selectedTowerRunningCost = sushiTowerRunningCost;
    }

    public void SelectBurgerTower () {
        levelManager.SetTowerToBuild(levelManager.burgerTowerPrefab);
        selectedTower = GameObject.Find("BurgerChef");
        selectedTowerHirePrice = burgerTowerHirePrice;
        selectedTowerSellPrice = burgerTowerSellPrice;
        selectedTowerRunningCost = burgerTowerRunningCost;
    }

    public void SelectPizzaTower () {
        levelManager.SetTowerToBuild(levelManager.pizzaTowerPrefab);
        selectedTower = GameObject.Find("PizzaChef");
        selectedTowerHirePrice = pizzaTowerHirePrice;
        selectedTowerSellPrice = pizzaTowerSellPrice;
        selectedTowerRunningCost = pizzaTowerRunningCost;
    }

    public void SelectNoodlesTower () {
        levelManager.SetTowerToBuild(levelManager.noodlesTowerPrefab);
        selectedTower = GameObject.Find("NoodlesChef");
        selectedTowerHirePrice = noodlesTowerHirePrice;
        selectedTowerSellPrice = noodlesTowerSellPrice;
        selectedTowerRunningCost = noodlesTowerRunningCost;
    }

    public void SelectWaiterTower () {
        levelManager.SetTowerToBuild(levelManager.waiterTowerPrefab);
        selectedTower = GameObject.Find("Waiter");
        selectedTowerHirePrice = waiterTowerHirePrice;
        selectedTowerSellPrice = waiterTowerSellPrice;
        selectedTowerRunningCost = waiterTowerRunningCost;
    }

    public void SelectFridgeTower () {
        levelManager.SetTowerToBuild(levelManager.fridgeTowerPrefab);
        selectedTower = GameObject.Find("Fridge");
        selectedTowerHirePrice = fridgeTowerHirePrice;
        selectedTowerSellPrice = fridgeTowerSellPrice;
        selectedTowerRunningCost = fridgeTowerRunningCost;
    }

    public void BuyTower () {
        // no tower selected to build
        if (levelManager.GetTowerToBuild() == null)
            return;

        player.currentNode.BuildTower(levelManager.GetTowerToBuild(), selectedTowerHirePrice, selectedTowerSellPrice, selectedTowerRunningCost);
    }

    public void SellTower () {
		player.currentNode.DestroyTower();
    }

    public Vector3 GetBuildPosition ()
	{
		return player.GetPosition();
	}

    // Check to see which shop object should be displayed depending on player location
    void CheckNode () {
        if (player.currentNode != null) {
            if (player.currentNode.tag == "KitchenNode") {
                waitStaff.interactable = false;
                foreach (GameObject staff in kitchenStaff)
                {
                    staff.GetComponent<Button>().interactable = true;
                }
                if (selectedTower!= null && selectedTower.tag != "KitchenStaff") {
                    selectedTower = null;
                    levelManager.SetTowerToBuild(null);
                }
            } else {
                waitStaff.interactable = true;
                foreach (GameObject staff in kitchenStaff)
                {
                    staff.GetComponent<Button>().interactable = false;
                }
                if (selectedTower!= null && selectedTower.tag != "WaitStaff") {
                    selectedTower = null;
                    levelManager.SetTowerToBuild(null);
                }
            }

            if (player.currentNode.tower == null) {
                buyButton.interactable = true;
                sellButton.interactable = false;
            } else {
                buyButton.interactable = false;
                sellButton.interactable = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckNode();
    }
}
