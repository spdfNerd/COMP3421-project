using System.Linq;
using TMPro;
using UnityEngine;

public class Waiter : MonoBehaviour {

	public Transform projectile;
	public Transform firePoint;

	public float range = 4f;
	public float fireCooldown = 2f;

	public int hirePrice;
	public int sellPrice;
	public int runningCost;

	public TextMeshProUGUI foodCountText;

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
		}
	}

	private void Start() {
		UpdateFoodType(0, 0);
	}

	private void Update() {
		FindTarget();
		if (target != null) {
			Shoot();
		}
	}

	public void UpdateFoodType(FoodType type, int count) {
		FoodType = type;
		FoodCount = count;
	}

	private void FindTarget() {
		if (target != null) {
			return;
		}

		float targetDistance = 0f;
		Collider[] colliders = Physics.OverlapSphere(transform.position, range);
		if (colliders.Length == 0) {
			return;
		}

		colliders = colliders.Where(collider => collider.GetComponent<Customer>() != null).ToArray();
		foreach (Collider collider in colliders) {
			Customer customer = collider.GetComponent<Customer>();
			if (customer.FoodTypeRequested != FoodType) {
				continue;
			}

			if (target == null) {
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
			return;
		}

		if (timer <= 0f) {
			Vector3 direction = target.position - firePoint.position;
			Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction));
			timer = fireCooldown;
			FoodCount--;
		} else {
			timer -= Time.deltaTime;
		}
	}

}
