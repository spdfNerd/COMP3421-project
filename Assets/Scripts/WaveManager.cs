using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour {

	public static WaveManager Instance;

	public Transform waypoints;
	public Button startWaveButton;

	public Transform adultCustomer;
	public Transform childCustomer;
	public Transform elderlyCustomer;
	public Transform karenCustomer;

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
		Wave wave = LevelManager.Instance.IsEndlessMode && LevelManager.Instance.Round >= waves.Length
			? GenerateRandomWave()
			: waves[LevelManager.Instance.Round - 1];

		for (int i = 0; i < wave.subWaves.Count; i++) {
			Wave.SubWave subWave = wave.subWaves[i];
			for (int j = 0; j < subWave.count; j++) {
				Instantiate(GetCustomerTransform(subWave.customer), spawnpoint.position, Quaternion.identity);
				enemyCount++;
				yield return new WaitForSeconds(subWave.spawnRate);
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

	public Transform GetCustomerTransform(CustomerType type) {
		return type switch {
			CustomerType.CHILD => childCustomer,
			CustomerType.ELDERLY => elderlyCustomer,
			CustomerType.KAREN => karenCustomer,
			_ => adultCustomer,
		};
	}

	private Wave GenerateRandomWave() {
		int roundsAfterEnd = LevelManager.Instance.Round - waves.Length - 1;
		float maxSpacing = 8f;

		Wave wave = new Wave();
		for (int i = 0; i < Mathf.FloorToInt(roundsAfterEnd / 3f); i++) {
			wave.AddSubWave((CustomerType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(CustomerType)).Length - 1),
				UnityEngine.Random.Range(3, i),
				UnityEngine.Random.Range(maxSpacing - 0.25f * i, maxSpacing));
		}
		for (int i = 0; i < Mathf.FloorToInt(roundsAfterEnd / 6f); i++) {
			wave.AddSubWave(CustomerType.KAREN,
				UnityEngine.Random.Range(1, i),
				UnityEngine.Random.Range(maxSpacing / 2f - 0.5f * i, maxSpacing / 2f));
		}

		return wave;
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

	public List<SubWave> subWaves;

	public void AddSubWave(CustomerType type, int count, float spawnRate) {
		subWaves.Add(new SubWave(type, count, spawnRate));
	}

	public void RandomiseSubWaves() {
	}

	[System.Serializable]
	public class SubWave {
		
		public CustomerType customer;
		public int count;
		public float spawnRate;

		public SubWave(CustomerType customer, int count, float spawnRate) {
			this.customer = customer;
			this.count = count;
			this.spawnRate = spawnRate;
		}

	}

}
