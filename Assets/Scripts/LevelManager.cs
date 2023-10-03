using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public Transform waypoints;
    public Transform enemyPrefab;

    private int enemyCount = 0;
    private Transform spawnpoint;

    private void Start() {
        spawnpoint = waypoints.GetChild(0).GetChild(0);

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (enemyCount < 10) {
            Instantiate(enemyPrefab, spawnpoint);
            enemyCount++;
            yield return new WaitForSeconds(1f);
        }
    }

}
