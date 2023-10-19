using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;
    public GameObject sushiTowerPrefab;
    private GameObject towerToBuild;

    public static int Money;
	public int startMoney = 400;

    public Transform waypoints;
    public Transform enemyPrefab;

    private int enemyCount = 0;
    private Transform spawnpoint;

    private void Start() {
        spawnpoint = waypoints.GetChild(0).GetChild(0);

        StartCoroutine(SpawnEnemies());
        Money = startMoney;
    }

    private IEnumerator SpawnEnemies() {
        while (enemyCount < 10) {
            Instantiate(enemyPrefab, spawnpoint);
            enemyCount++;
            yield return new WaitForSeconds(1f);
        }
    }

    public GameObject GetTowerToBuild () {
        return towerToBuild;
    }

    public void SetTowerToBuild (GameObject tower) {
        towerToBuild = tower;
    }

}
