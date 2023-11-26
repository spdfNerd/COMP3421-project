using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Customer : MonoBehaviour {

	public CustomerType customerType;

	[Header("Movement Settings")]
	public int loopCount = 2;
	public float speed = 10f;
	public float rotateSpeed = 0.5f;

	[Header("Penalty Settings")]
	public int reputationPenalty = 10;

	[Header("Graphics")]
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

	public bool RequestsSatisfied {
		get => requestsSatisfied;
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

	/// <summary>
	/// Fulfill the customer requests
	/// </summary>
	/// <param name="food">The food used to try to fulfill customer request</param>
	public void SatisfyRequest(Food food) {
		if (food.type != FoodTypeRequested) {
			return;
		}

		FoodCountRequested--;
		if (FoodCountRequested == 0) {
			requestsSatisfied = true;
			// Disable collider to prevent projectiles from hitting the customer again
			GetComponent<Collider>().enabled = false;
			// Remove food icon
			Destroy(foodHolder.GetChild(0).gameObject);
		}

		rewardsToGrant += food.reward;
	}

	/// <summary>
	/// Exit the map and performing relevant functions based on customer satsifaction
	/// </summary>
	public void Exit() {
		if (!requestsSatisfied) {
			LevelManager.Instance.Reputation -= reputationPenalty;
		} else {
			LevelManager.Instance.Money += rewardsToGrant;
			UpdateQuest();
		}

		WaveManager.Instance.DecrementEnemyCount();
		Destroy(gameObject);
	}

	/// <summary>
	/// Get the next waypoints the customer should travel to
	/// </summary>
	private void QueueWaypoints() {
		if (pathFinished) {
			Exit();
			return;
		}

		if (!isLooping) {
			// Add the starting waypoints, before the main loop
			QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(0));
			isLooping = true;
		} else {
			if (loopCounter < loopCount && !requestsSatisfied) {
				// Add the looping waypoints
				QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(1));
				loopCounter++;
			} else {
				// Add the ending waypoints, after the main loop
				QueueWaypointsFromTransform(WaveManager.Instance.waypoints.GetChild(2));
				pathFinished = true;
			}
		}
	}

	/// <summary>
	/// Add the next waypoints into the queue
	/// </summary>
	/// <param name="transform">The parent of the waypoints</param>
	private void QueueWaypointsFromTransform(Transform transform) {
		foreach (Transform child in transform) {
			nextWaypoints.Enqueue(child);
		}
	}

	/// <summary>
	/// Generate the food requests to be fulfilled
	/// </summary>
	private void GenerateRequests() {
		FoodTypeRequested = (FoodType) UnityEngine.Random.Range(0, GetFoodTypeMaxValue());
		FoodCountRequested = UnityEngine.Random.Range(1, 5);
		// Make a new icon of the requested food
		Instantiate(foods[(int) FoodTypeRequested], foodHolder);
	}

	/// <summary>
	/// Get the food types which can be generated for requests
	/// </summary>
	/// <returns>The int value of the hardest food type that can be generated</returns>
	private int GetFoodTypeMaxValue() {
		return LevelManager.Instance.Round < 2 * 4
			? Mathf.CeilToInt(LevelManager.Instance.Round / 2f)
			: Enum.GetValues(typeof(FoodType)).Length;
	}

	private Vector3 Move(Transform nextWaypoint) {
		Vector3 direction = nextWaypoint.position - transform.position;
		direction.Normalize();
		if (!reachedWaypoint) {
			transform.Translate(speed * Time.deltaTime * direction, Space.World);
			float distanceToWaypoint = Vector3.Distance(transform.position, nextWaypoint.position);
			// Check that the customer has reached the waypoint within a tolerance
			reachedWaypoint = FloatComparer.AreEqual(distanceToWaypoint, 0f, 0.1f);
		}
		return direction;
	}

	/// <summary>
	/// Rotate the model of the customer to match the movement direction
	/// </summary>
	/// <param name="direction">The direction in which the customer is moving</param>
	private void RotateModel(Vector3 direction) {
		// Don't look if the player is not moving
		if (direction == Vector3.zero) {
			return;
		}

		Quaternion lookRotation = Quaternion.LookRotation(direction);
		// Using Quaternion.Lerp to smooth out the rotation
		Vector3 rotation = Quaternion.Lerp(gfx.rotation, lookRotation, rotateSpeed).eulerAngles;
		gfx.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void UpdateQuest() {
		QuestManager.Instance.TryUpdateServeCustomerQuestProgress(customerType);
	}

}
