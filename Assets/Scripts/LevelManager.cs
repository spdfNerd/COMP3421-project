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

    void Awake ()
	{
		if (instance != null)
		{
			Debug.Log("More than one LevelManager in scene!");
            // Debug.LogError("More than one LevelManager in scene!");
			return;
		}
		instance = this;
	}

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
        if (towerToBuild == null)
		{
            Debug.Log("brooooo null tower");
		}
        return towerToBuild;
    }

    public void SetTowerToBuild (GameObject tower) {
        // Debug.Log(tower);
        if (tower != null)
		{
            Debug.Log("building ayyy");
		}
        towerToBuild = tower;
    }

}
