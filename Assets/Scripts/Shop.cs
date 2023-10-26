using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    LevelManager levelManager;
    Player player;

    public int sushiTowerPrice;
    public int burgerTowerPrice;
    public int pizzaTowerPrice;
    public int noodlesTowerPrice;
    public int waiterTowerPrice;
    public int fridgeTowerPrice;

    private int selectedTowerPrice;


    void Start()
    {
        levelManager = LevelManager.instance;
        player = Player.instance;
    }

    public void SelectSushiTower () {
        Debug.Log("Sushi Tower Selected");
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
        selectedTowerPrice = sushiTowerPrice;
    }

    public void SelectBurgerTower () {
        Debug.Log("Burger Tower Selected");
        levelManager.SetTowerToBuild(levelManager.burgerTowerPrefab);
        selectedTowerPrice = burgerTowerPrice;
    }

    public void SelectPizzaTower () {
        Debug.Log("Sushi Tower Selected");
        levelManager.SetTowerToBuild(levelManager.pizzaTowerPrefab);
        selectedTowerPrice = pizzaTowerPrice;
    }

    public void SelectNoodlesTower () {
        Debug.Log("Noodles Tower Selected");
        levelManager.SetTowerToBuild(levelManager.noodlesTowerPrefab);
        selectedTowerPrice = noodlesTowerPrice;
    }

    public void SelectWaiterTower () {
        Debug.Log("Waiter Tower Selected");
        levelManager.SetTowerToBuild(levelManager.waiterTowerPrefab);
        selectedTowerPrice = waiterTowerPrice;
    }

    public void SelectFridgeTower () {
        Debug.Log("Fridge Tower Selected");
        levelManager.SetTowerToBuild(levelManager.fridgeTowerPrefab);
        selectedTowerPrice = fridgeTowerPrice;
    }

    public void BuyTower () {
        Debug.Log("Pressed");

        // no tower selected to build
        if (levelManager.GetTowerToBuild() == null)
            return;

        player.currentNode.BuildTower(levelManager.GetTowerToBuild(), selectedTowerPrice);
    }

    public void SellTower () {
		player.currentNode.DestroyTower();
    }

    public Vector3 GetBuildPosition ()
	{
		return player.GetPosition();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
