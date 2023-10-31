using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : MonoBehaviour {

	public int loopCount = 2;
	public float speed = 10f;
	public float rotateSpeed = 0.5f;

	public int reputationPenalty = 10;

	public Transform gfx;
	public Transform foodHolder;
	public Transform[] foods;

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
			foodCountText.text = foodCountRequested == 0 ? "" : foodCountRequested.ToString();
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

		Vector3 moveDir = Move(nextWaypoint);
		RotateModel(moveDir);
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
		FoodCountRequested = UnityEngine.Random.Range(1, 5);
		Instantiate(foods[(int) FoodTypeRequested], foodHolder);
	}

	private int GetFoodTypeMaxValue() {
		return LevelManager.Instance.Round < 2 * 4
			? (int) Mathf.Ceil(LevelManager.Instance.Round / 2f)
			: Enum.GetValues(typeof(FoodType)).Length;
	}

	private Vector3 Move(Transform nextWaypoint) {
		Vector3 direction = nextWaypoint.position - transform.position;
		direction.Normalize();
		if (!reachedWaypoint) {
			transform.Translate(direction * speed * Time.deltaTime, Space.World);
			reachedWaypoint = Vector3.Distance(transform.position, nextWaypoint.position) < 0.1f;
		}
		return direction;
	}

	private void RotateModel(Vector3 direction) {
		if (direction == Vector3.zero) {
			return;
		}

		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(gfx.rotation, lookRotation, rotateSpeed).eulerAngles;
		gfx.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	public void SatisfyRequest(Food food) {
		if (food.type != FoodTypeRequested) {
			return;
		}

		FoodCountRequested--;
		if (FoodCountRequested == 0) {
			requestsSatisfied = true;
			Destroy(foodHolder.GetChild(0).gameObject);
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
