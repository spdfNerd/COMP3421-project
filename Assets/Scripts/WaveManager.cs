using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public Transform waypoints;
	public Transform enemyPrefab;

	public Button startWaveButton;

	private LevelManager levelManager;

	private int enemyCount = 0;
	private bool waveIsActive = false;

	private Transform spawnpoint;

	private void Start() {
		levelManager = FindFirstObjectByType<LevelManager>();
		spawnpoint = waypoints.GetChild(0).GetChild(0);
	}

	private void Update() {
		if (waveIsActive) {
			if (enemyCount <= 0) {
				enemyCount = 0;
				waveIsActive = false;
				startWaveButton.interactable = true;
			}
		}
	}

	public void StartWave() {
		levelManager.Round++;
		startWaveButton.interactable = false;
		StartCoroutine(SpawnEnemies());
		levelManager.Money -= levelManager.RunningCost;
	}

	private IEnumerator SpawnEnemies() {
		waveIsActive = true;
		for (int i = 0; i < 10; i++) {
			Instantiate(enemyPrefab, spawnpoint.position, Quaternion.identity);
			enemyCount++;
			yield return new WaitForSeconds(1f);
		}
	}

	public void DecrementEnemyCount() {
		enemyCount--;
	}

}
