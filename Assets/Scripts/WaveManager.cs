using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public static WaveManager Instance;

	public Transform waypoints;
	public Button startWaveButton;

	public Wave[] waves;

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
		spawnpoint = waypoints.GetChild(0).GetChild(0);
	}

	private void Update() {
		startWaveButton.interactable = enemyCount == 0;
	}

	public void StartWave() {
		LevelManager.Instance.Round++;
		startWaveButton.interactable = false;
		StartCoroutine(SpawnEnemies());
	}

	private IEnumerator SpawnEnemies() {
		if (LevelManager.Instance.Round >= waves.Length && LevelManager.Instance.IsEndlessMode) {
			// Generate random waves for endless mode
		} else {
			Wave wave = waves[LevelManager.Instance.Round - 1];
			for (int i = 0; i < wave.subWaves.Length; i++) {
				Wave.SubWave subWave = wave.subWaves[i];
				for (int j = 0; j < subWave.count; j++) {
					Instantiate(subWave.customer, spawnpoint.position, Quaternion.identity);
					enemyCount++;
					yield return new WaitForSeconds(subWave.spawnRate);
				}
			}
		}
	}

	public void DecrementEnemyCount() {
		enemyCount--;
		if (enemyCount == 0) {
			LevelManager.Instance.Money -= LevelManager.Instance.RunningCost;
		}

		CheckCanContinue();
	}

	private void CheckCanContinue() {
		if (LevelManager.Instance.IsEndlessMode) {
			if (LevelManager.Instance.Reputation <= 0 || LevelManager.Instance.Money < 0) {
				// Switch to game summary screen
			}
		} else {
			if (LevelManager.Instance.Reputation <= 0 || LevelManager.Instance.Money < 0) {
				// Switch to lose screen
				SceneManager.LoadScene("LoseScreen");
			} else if (LevelManager.Instance.Round == waves.Length && enemyCount == 0) {
				// Switch to win screen
				SceneManager.LoadScene("WinScreen");
			}
		}
	}

}

[System.Serializable]
public class Wave {

	public SubWave[] subWaves;

	[System.Serializable]
	public class SubWave {
		public Transform customer;
		public int count;
		public float spawnRate;
	}

}
