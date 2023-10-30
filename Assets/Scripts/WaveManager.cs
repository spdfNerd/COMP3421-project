using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public static WaveManager Instance;

	public Transform waypoints;
	public Button startWaveButton;

	public Wave[] waves;

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
		levelManager.Money -= levelManager.RunningCost;
	}

	private IEnumerator SpawnEnemies() {
		Wave wave = waves[LevelManager.Instance.Round - 1];
		for (int i = 0; i < wave.subWave.Length; i++) {
			Wave.SubWave subWave = wave.subWave[i];
			for (int j = 0; j < subWave.count; j++) {
				Instantiate(subWave.customer, spawnpoint.position, Quaternion.identity);
				enemyCount++;
				yield return new WaitForSeconds(subWave.spawnRate);
			}
		}
	}

	public void DecrementEnemyCount() {
		enemyCount--;
	}

}

[System.Serializable]
public class Wave {

	public SubWave[] subWave;

	[System.Serializable]
	public class SubWave {
		public Transform customer;
		public int count;
		public float spawnRate;
	}

}
