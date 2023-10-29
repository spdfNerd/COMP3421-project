using System;
using UnityEngine;

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
    public GameObject waiterStaff;
    
    private GameObject selectedTower;
    private int selectedTowerHirePrice;


    void Start()
    {
        levelManager = LevelManager.instance;
        player = Player.instance;
        kitchenStaff = GameObject.FindGameObjectsWithTag("KitchenStaff");
        waiterStaff = GameObject.FindWithTag("WaiterStaff");
    }

    public void SelectSushiTower () {
        Debug.Log("Sushi Tower Selected");
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
        selectedTower = GameObject.Find("SushiChef");
        selectedTowerHirePrice = sushiTowerHirePrice;
    }

    public void SelectBurgerTower () {
        Debug.Log("Burger Tower Selected");
        levelManager.SetTowerToBuild(levelManager.burgerTowerPrefab);
        selectedTower = GameObject.Find("BurgerChef");
        selectedTowerHirePrice = burgerTowerHirePrice;
    }

    public void SelectPizzaTower () {
        Debug.Log("Sushi Tower Selected");
        levelManager.SetTowerToBuild(levelManager.pizzaTowerPrefab);
        selectedTower = GameObject.Find("PizzaChef");
        selectedTowerHirePrice = pizzaTowerHirePrice;
    }

    public void SelectNoodlesTower () {
        Debug.Log("Noodles Tower Selected");
        levelManager.SetTowerToBuild(levelManager.noodlesTowerPrefab);
        selectedTower = GameObject.Find("NoodlesChef");
        selectedTowerHirePrice = noodlesTowerHirePrice;
    }

    public void SelectWaiterTower () {
        Debug.Log("Waiter Tower Selected");
        levelManager.SetTowerToBuild(levelManager.waiterTowerPrefab);
        selectedTower = GameObject.Find("Waiter");
        selectedTowerHirePrice = waiterTowerHirePrice;
    }

    public void SelectFridgeTower () {
        Debug.Log("Fridge Tower Selected");
        levelManager.SetTowerToBuild(levelManager.fridgeTowerPrefab);
        selectedTower = GameObject.Find("Fridge");
        selectedTowerHirePrice = fridgeTowerHirePrice;
    }

    public void BuyTower () {
        Debug.Log("Pressed");

        // no tower selected to build
        if (levelManager.GetTowerToBuild() == null)
            return;

        player.currentNode.BuildTower(levelManager.GetTowerToBuild(), selectedTowerHirePrice);
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
                waiterStaff.SetActive(false);
                foreach (GameObject staff in kitchenStaff)
                {
                    staff.SetActive(true);
                }
                if (selectedTower!= null && selectedTower.tag != "KitchenStaff") {
                    selectedTower = null;
                    levelManager.SetTowerToBuild(null);
                }
            } else {
                waiterStaff.SetActive(true);
                foreach (GameObject staff in kitchenStaff)
                {
                    staff.SetActive(false);
                }
                if (selectedTower!= null && selectedTower.tag != "WaiterStaff") {
                    selectedTower = null;
                    levelManager.SetTowerToBuild(null);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckNode();
    }
}
