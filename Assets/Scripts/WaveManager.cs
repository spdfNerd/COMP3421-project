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

	public int EnemyCount { get => enemyCount; }

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
		// Either get random wave if level is endless, or get pre-determined wave from wave list
		Wave wave = HasNoPrescribedWaves() || IsPastPrescribedRounds()
			? GenerateRandomWave()
			: waves[LevelManager.Instance.Round - 1];

		// Spawn all subwaves
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

	public bool HasNoPrescribedWaves() {
		return waves == null || waves.Length == 0;
	}

	public bool IsPastPrescribedRounds() {
		return LevelManager.Instance.isEndlessMode && LevelManager.Instance.Round > waves.Length;
	}

	private Wave GenerateRandomWave() {
		int roundsAfterEnd = LevelManager.Instance.Round - waves.Length - 1;
		float maxSpacing = 8f;
		// Cap the amount of customers that can be spawned based on progress into endless mode
		int normalCustomersCount = Mathf.FloorToInt(roundsAfterEnd / 3f) + 2;
		int karenCustomersCount = Mathf.FloorToInt(roundsAfterEnd / 6f) + 1;

		float MinCustomerSpacing(float i, float multiplier) => Math.Max(0.25f * multiplier, (maxSpacing / multiplier) - (0.25f * multiplier * i));

		Wave wave = new Wave();
		for (int i = 0; i < normalCustomersCount; i++) {
			// Randomise customer type, count,, and spacing
			wave.InsertSubWave((CustomerType) UnityEngine.Random.Range(0, Enum.GetValues(typeof(CustomerType)).Length),
				UnityEngine.Random.Range(3, i),
				UnityEngine.Random.Range(MinCustomerSpacing(i, 1f), maxSpacing));
		}
		for (int i = 0; i < karenCustomersCount; i++) {
			// Randomise karen customer count and spacing
			wave.InsertSubWave(CustomerType.KAREN,
				UnityEngine.Random.Range(1, i),
				UnityEngine.Random.Range(MinCustomerSpacing(i, 2f), maxSpacing / 2f));
		}

		return wave;
	}

	private void CheckCanContinue() {
		if (LevelManager.Instance.Reputation <= 0 || LevelManager.Instance.Money < 0) {
			// Switch to lose screen
			SceneManager.LoadScene("LoseScreen");
		}

		if (!LevelManager.Instance.isEndlessMode) {
			if (LevelManager.Instance.Round == waves.Length && enemyCount == 0) {
				// Switch to win screen
				SceneManager.LoadScene("WinScreen");
			}
		}
	}

}

[Serializable]
public class Wave {

	public List<SubWave> subWaves = new();

	/// <summary>
	/// Insert sub-wave into the list of sub-waves in this wave.
	/// By default, sub-wave is added to the end of the sub-waves list
	/// </summary>
	/// <param name="type">Type of Customer of the sub-wave</param>
	/// <param name="count">Count of customers in the sub-wave</param>
	/// <param name="spawnRate">Spawn rate of customers in the sub-wave</param>
	/// <param name="insertRandomly">Insert the sub-wave into a random place in the list?</param>
	public void InsertSubWave(CustomerType type, int count, float spawnRate, bool insertRandomly = true) {
		SubWave subWave = new(type, count, spawnRate);
		if (insertRandomly) {
			subWaves.Insert(UnityEngine.Random.Range(0, subWaves.Count), subWave);
		} else {
			subWaves.Add(subWave);
		}
	}

	[Serializable]
	public class SubWave {
		
		public CustomerType customer;
		public int count;
		public float spawnRate;

		public SubWave(CustomerType customer, int count, float spawnRate) {
			this.customer = customer;
			this.count = count;
			this.spawnRate = spawnRate;
		}

		public override string ToString() {
			return string.Format("{0}x {1} at {2} per second", count, customer.ToString(), (1f / spawnRate).ToString("0.000"));
		}

	}

	public override string ToString() {
		string result = "";
		for (int i = 0; i < subWaves.Count; i++) {
			result += string.Format("Sub-wave {0}: {1}\n", i + 1, subWaves[i].ToString());
		}
		return result;
	}

}
