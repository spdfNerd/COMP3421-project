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

	private int loopCounter = 0;
	private bool isLooping = false;
	private bool pathFinished = false;

	private List<FoodType> requests;
	private bool requestsSatisfied = false;

	private void Start() {
		levelManager = FindFirstObjectByType<LevelManager>();
		waveManager = FindFirstObjectByType<WaveManager>();

		nextWaypoints = new Queue<Transform>();
		QueueWaypoints();

		GenerateRequests();
	}

	public void Update() {
		if (nextWaypoint == null || reachedWaypoint) {
			if (nextWaypoints.Count == 0) {
				QueueWaypoints();
			} else {
				nextWaypoint = nextWaypoints.Dequeue();
				reachedWaypoint = false;
			}
		}
		Move(nextWaypoint);
	}

	private void QueueWaypoints() {
		if (pathFinished) {
			Exit();
			return;
		}

		if (!isLooping) {
			QueueWaypointsFromTransform(waveManager.waypoints.GetChild(0));
			isLooping = true;
		} else {
			if (loopCounter < loopCount && !requestsSatisfied) {
				QueueWaypointsFromTransform(waveManager.waypoints.GetChild(1));
				loopCounter++;
			} else {
				QueueWaypointsFromTransform(waveManager.waypoints.GetChild(2));
				pathFinished = true;
			}
		}
	}

	private void QueueWaypointsFromTransform(Transform transform) {
		foreach (Transform child in transform) {
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
