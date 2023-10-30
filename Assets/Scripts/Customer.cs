using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour {

	public int loopCount = 2;
	public float speed = 10f;

	public int reputationPenalty = 10;

	public TextMeshProUGUI foodCountText;

	private Transform nextWaypoint;
	private Queue<Transform> nextWaypoints;
	private bool reachedWaypoint = false;

	private int loopCounter = 0;
	private bool isLooping = false;
	private bool pathFinished = false;

	private int rewardsToGrant = 0;

	private FoodType foodTypeRequested;
	private int foodCountRequested;
	private bool requestsSatisfied = false;

	public FoodType FoodTypeRequested {
		get => foodTypeRequested;
		set {
			foodTypeRequested = value;
		}
	}

	public int FoodCountRequested {
		get => foodCountRequested;
		set {
			foodCountRequested = value;
			foodCountText.text = foodCountRequested.ToString();
		}
	}

	private void Start() {
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
			QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(0));
			isLooping = true;
		} else {
			if (loopCounter < loopCount && !requestsSatisfied) {
				QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(1));
				loopCounter++;
			} else {
				QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(2));
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
		FoodTypeRequested = (FoodType) UnityEngine.Random.Range(0, GetFoodTypeMaxValue());
		FoodCountRequested = UnityEngine.Random.Range(1, 4);
	}

	private int GetFoodTypeMaxValue() {
		return LevelManager.Instance.Round < 2 * 4 ? (int) Mathf.Ceil(LevelManager.Instance.Round / 2f) : Enum.GetValues(typeof(FoodType)).Length;
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
		if (food.type != FoodTypeRequested) {
			return;
		}

		FoodCountRequested--;
		if (FoodCountRequested == 0) {
			Debug.Log("Satisfied!");
		}

		int foodReward;
		bool success = LevelManager.Instance.foodRewards.TryGetValue(food.type, out foodReward);
		if (success) {
			rewardsToGrant += foodReward;
		} else {
			Debug.LogError("Food reward has not been entered yet!");
		}
	}

	public void Exit() {
		if (!requestsSatisfied) {
			LevelManager.Instance.Reputation -= reputationPenalty;
		} else {
			LevelManager.Instance.Money += rewardsToGrant;
		}
		WaveManager.Instance.DecrementEnemyCount();
		Destroy(gameObject);
	}

}
