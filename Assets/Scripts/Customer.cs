using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour {

	public int loopCount = 2;
	public float speed = 10f;
	public int worth = 50;

	private LevelManager levelManager;
	private WaveManager waveManager;

	private Transform nextWaypoint;
	private Queue<Transform> nextWaypoints;
	private bool reachedWaypoint = false;

	private List<FoodType> requests;
	private bool requestsSatisfied = false;

	private void Start() {
		levelManager = FindFirstObjectByType<LevelManager>();
		waveManager = FindFirstObjectByType<WaveManager>();

		nextWaypoints = new Queue<Transform>();
		GetWaypoints();

		GenerateRequests();
	}

	public void Update() {
		if (nextWaypoint == null || reachedWaypoint) {
			if (nextWaypoints.Count == 0) {
				Exit();
				return;
			}
			nextWaypoint = nextWaypoints.Dequeue();
			reachedWaypoint = false;
		}
		Move(nextWaypoint);
	}

	private void GetWaypoints() {
		foreach (Transform child in waveManager.waypoints.GetChild(0)) {
			nextWaypoints.Enqueue(child);
		}
		for (int i = 0; i < loopCount; i++) {
			foreach (Transform child in waveManager.waypoints.GetChild(1)) {
				nextWaypoints.Enqueue(child);
			}
		}
		foreach (Transform child in waveManager.waypoints.GetChild(2)) {
			nextWaypoints.Enqueue(child);
		}
	}

	private void GenerateRequests() {
		requests = new List<FoodType>(2);
		int numFoodTypes = Enum.GetNames(typeof(FoodType)).Length;
		requests.ForEach(request => { request = (FoodType)UnityEngine.Random.Range(0, numFoodTypes - 1); });
	}

	private void Move(Transform nextWaypoint) {
		if (!reachedWaypoint) {
			Vector3 direction = nextWaypoint.position - transform.position;
			direction = direction.normalized;
			transform.Translate(direction * speed * Time.deltaTime, Space.World);
			reachedWaypoint = Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f;
		}
	}

	public void SatisfyRequest(Food food) {
		if (!requestsSatisfied && requests.Contains(food.type)) {
			requests.Remove(food.type);
			if (requests.Count == 0) {
				requestsSatisfied = true;
				Debug.Log("Satisfied!");
			}
		}
	}

	public void Exit() {
		if (!requestsSatisfied) {
			levelManager.Reputation--;
		} else {
			levelManager.Money += worth;
		}
		waveManager.DecrementEnemyCount();
		Destroy(gameObject);
	}

}
