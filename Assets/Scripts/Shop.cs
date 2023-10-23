using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    LevelManager levelManager;

    void Start()
    {
        levelManager = LevelManager.instance;
    }

    public void SelectSushiTower () {
        Debug.Log("Sushi Tower Selected");
        Debug.Log(levelManager.sushiTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
    }

    public void SelectBurgerTower () {
        Debug.Log("Burger Tower Selected");
        Debug.Log(levelManager.burgerTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.burgerTowerPrefab);
    }

    public void SelectPizzaTower () {
        Debug.Log("Sushi Tower Selected");
        Debug.Log(levelManager.pizzaTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.pizzaTowerPrefab);
    }

    public void SelectNoodlesTower () {
        Debug.Log("Noodles Tower Selected");
        Debug.Log(levelManager.noodlesTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.noodlesTowerPrefab);
    }

    public void SelectWaiterTower () {
        Debug.Log("Waiter Tower Selected");
        Debug.Log(levelManager.waiterTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.waiterTowerPrefab);
    }

    public void SelectFridgeTower () {
        Debug.Log("Fridge Tower Selected");
        Debug.Log(levelManager.fridgeTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.fridgeTowerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
