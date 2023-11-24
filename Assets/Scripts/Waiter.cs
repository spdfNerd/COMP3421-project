using System.Linq;
using UnityEngine;

public class Waiter : Staff {

	[Header("Shoot Settings")]
	public Transform[] projectiles;
	public Transform firePoint;
	public Transform foodHolder;

	public float range = 4f;
	public float fireCooldown = 2f;

	[Header("Food Icons")]
	public Transform[] icons;

	private Transform target = null;
	private float timer = 0f;

	private FoodType foodType;
	private int foodCount;

	public FoodType FoodType {
		get => foodType;
		set {
			foodType = value;
		}
	}

	public int FoodCount {
		get => foodCount;
		set {
			foodCount = value;
			foodCountText.text = foodCount == 0 ? "" : foodCount.ToString();

			if (foodCount == 0 && foodHolder.childCount > 0) {
				Destroy(foodHolder.GetChild(0).gameObject);
			}
		}
	}

	private void Update() {
		FindTarget();
		if (target != null) {
			Shoot();
		}
	}

	/// <summary>
	/// Update the type of food the waiter is to shoot, along with the amount of food they have to shoot
	/// </summary>
	/// <param name="type">Type of food to be shot</param>
	/// <param name="count">Number of food available to be shot</param>
	public void UpdateFoodType(FoodType type, int count) {
		if (FoodCount == 0) {
			FoodType = type;
			FoodCount = count;
			// Only make icon appear if the updated food type has count more than 0
			if (count > 0) {
				Instantiate(icons[(int) type], foodHolder);
			}
		} else if (FoodType == type) {
			FoodCount += count;
		}
	}

	private void FindTarget() {
		if (target != null && target.GetComponent<Customer>().FoodCountRequested == 0) {
			target = null;
		}

		float targetDistance = Mathf.Infinity;
		// Find all colliders which are within the range sphere of the waiter
		Collider[] colliders = Physics.OverlapSphere(transform.position, range);
		// Find all colliders which are customers
		colliders = colliders.Where(collider => collider.GetComponent<Customer>() != null).ToArray();
		if (colliders.Length == 0) {
			return;
		}

		foreach (Collider collider in colliders) {
			Customer customer = collider.GetComponent<Customer>();
			// Ignore customer if they don't match the food type or the customer is satisfied
			if (customer.FoodTypeRequested != FoodType) {
				continue;
			}
			if (customer.FoodCountRequested <= 0) {
				continue;
			}

			if (target == null) {
				// Set target if target is already null
				target = collider.transform;
				targetDistance = Vector3.Distance(transform.position, target.position);
			} else {
				// Find closest enemy
				if (Vector3.Distance(transform.position, collider.transform.position) < targetDistance) {
					target = collider.transform;
				}
			}
		}
	}

	private void Shoot() {
		if (FoodCount == 0) {
			if (foodHolder.childCount > 0) {
				Destroy(foodHolder.GetChild(0).gameObject);
			}
			return;
		}

		if (timer <= 0f) {
			// Get food projectile direction
			Vector3 direction = target.position - firePoint.position;
			Transform foodTransform = Instantiate(projectiles[(int) foodType], firePoint.position, Quaternion.LookRotation(direction));
			Food food = foodTransform.GetComponent<Food>();
			food.SetTarget(target);
			timer = fireCooldown;
			FoodCount--;
		} else {
			timer -= Time.deltaTime;
		}
	}

	protected override void InitStaff() {
		UpdateFoodType(0, 0);
	}

	protected override void UpgradeStats() {
		fireCooldown = 1f;
	}

}
