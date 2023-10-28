using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public static WaveManager Instance;

	public Transform waypoints;
	public Transform enemyPrefab;

	public Button startWaveButton;

	private LevelManager levelManager;

	private int enemyCount = 0;

	private Transform spawnpoint;

	private void Awake() {
		if (Instance != null) {
			Debug.LogError("More than one WaveManager in scene!");
			return;
		}
		Instance = this;
	}

	private void Start() {
		levelManager = FindFirstObjectByType<LevelManager>();
		spawnpoint = waypoints.GetChild(0).GetChild(0);
	}

	private void Update() {
		startWaveButton.interactable = enemyCount == 0;
	}

	public void StartWave() {
		levelManager.Round++;
		startWaveButton.interactable = false;
		StartCoroutine(SpawnEnemies());
	}

	private IEnumerator SpawnEnemies() {
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
