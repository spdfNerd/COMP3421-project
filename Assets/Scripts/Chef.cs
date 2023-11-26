using UnityEngine;

public class Chef : Staff {

	[Header("Food Settings")]
	public float cooldown = 2f;
	public float upgradedCooldown = 1f;
	public int foodLimit;
	public FoodType foodType;

	private float cooldownTimer;
	private int foodCount;

	public int FoodCount {
		get => foodCount;
		set {
			foodCount = value;
			// Make sure that the food is between 0 and the food limit
			foodCount = Mathf.Clamp(foodCount, 0, foodLimit);
			foodCountText.text = foodCount == 0 ? "" : foodCount.ToString();
			// Change the text colour to red if the food limit is reached
			foodCountText.color = foodCount == foodLimit ? Color.red : Color.white;
		}
	}

	private void Update() {
		if (FoodCount >= foodLimit) {
			return;
		}

		// Produce food if cooldown timer is finished counting down
		// and there are enemies on the map
		if (cooldownTimer <= 0f && WaveManager.Instance.EnemyCount > 0) {
			ProduceFood();
			cooldownTimer = cooldown;
		} else {
			cooldownTimer -= Time.deltaTime;
		}
	}

	public void ResetFoodCount() {
		FoodCount = 0;
	}

	/// <summary>
	/// Increment chef food count
	/// </summary>
	private void ProduceFood() {
		FoodCount++;
	}

	protected override void InitStaff() {
		cooldownTimer = cooldown;
		FoodCount = 0;
	}

	protected override void UpgradeStats() {
		foodLimit = 10;
		upgradedCooldown = 1f;
	}

}
