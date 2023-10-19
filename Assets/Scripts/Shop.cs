using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    LevelManager levelManager;

    void Start()
    {
        levelManager = LevelManager.instance;
    }

    public void PurchaseSushiTower () {
        Debug.Log("Sushi Tower Purchased");
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
