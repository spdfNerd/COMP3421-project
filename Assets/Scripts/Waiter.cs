using System.Linq;
using TMPro;
using UnityEngine;

public class Waiter : MonoBehaviour {

	[Header("Shoot Settings")]
	public Transform[] projectiles;
	public Transform firePoint;
	public Transform foodHolder;

	public float range = 4f;
	public float fireCooldown = 2f;

	[Header("Costs")]
	public StaffCosts costs;

	[Header("Graphics")]
	public Transform[] icons;
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

			if (foodCount == 0 && foodHolder.childCount > 0) {
				Destroy(foodHolder.GetChild(0).gameObject);
			}
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
		if (FoodCount == 0) {
			FoodType = type;
			FoodCount = count;
			if (count > 0) {
				Instantiate(icons[(int) type], foodHolder);
			}
		}
	}

	private void FindTarget() {
		if (target != null) {
			if (target.GetComponent<Customer>().FoodCountRequested == 0) {
				target = null;
			} else {
				return;
			}
		}

		float targetDistance = Mathf.Infinity;
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
			if (customer.FoodCountRequested <= 0) {
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
			Transform foodTransform = Instantiate(projectiles[(int) foodType], firePoint.position, Quaternion.LookRotation(direction));
			Food food = foodTransform.GetComponent<Food>();
			food.SetTarget(target);
			timer = fireCooldown;
			FoodCount--;
		} else {
			timer -= Time.deltaTime;
		}
	}

	public void Upgrade() {
		// range = 20f;
		fireCooldown = 1f;
	}

}
