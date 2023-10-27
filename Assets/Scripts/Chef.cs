using UnityEngine;

public class Chef : MonoBehaviour {

	public float cooldown = 1f;
	public FoodType foodType;
	
	private float cooldownTimer;
	private int foodCount;

	private void Start() {
		cooldownTimer = cooldown;
		foodCount = 0;
	}

	private void Update() {
		if (cooldownTimer <= 0f) {
			ProduceFood();
			cooldownTimer = cooldown;
		} else {
			cooldownTimer -= Time.deltaTime;
		}
	}

	private void ProduceFood() {
		foodCount++;
	}

}
