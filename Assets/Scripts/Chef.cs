using TMPro;
using UnityEngine;

public class Chef : MonoBehaviour {

	[Header("Food Settings")]
	public float cooldown = 1f;
	public int foodLimit;
	public FoodType foodType;

	[Header("Costs")]
	public StaffCosts costs;

	[Header("Graphics")]
	public TextMeshProUGUI foodCountText;

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

	private void Start() {
		cooldownTimer = cooldown;
		FoodCount = 0;
	}

	private void Update() {
		if (FoodCount >= foodLimit) {
			return;
		}

		// Produce food if cooldown timer is finished counting down
		if (cooldownTimer <= 0f) {
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

	public void Upgrade(FoodType foodType, int foodCount) {
		foodLimit = 10;
		cooldown = 1f;
		this.foodType = foodType;
		FoodCount = foodCount;
	}

}
