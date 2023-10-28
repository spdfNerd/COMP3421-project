using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    LevelManager levelManager;

    void Start()
    {
        levelManager = LevelManager.Instance;
    }

    public void SelectSushiTower () {
        Debug.Log("Sushi Tower Selected");
        Debug.Log(levelManager.sushiTowerPrefab);
        levelManager.SetTowerToBuild(levelManager.sushiTowerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
		
    }
}
