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
			foodCountText.text = foodCount == 0 ? "" : foodCount.ToString();
		}
	}

	public int FoodLimit {
		get => foodLimit;
		set {
			foodLimit = value;
		}
	}

	private void Start() {
		cooldownTimer = cooldown;
		FoodCount = 0;
	}

	private void Update() {
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

	private void ProduceFood() {
		if (FoodCount < foodLimit) {
			FoodCount++;
		}
	}

	public void Upgrade() {
		foodLimit = 10;
		cooldown = 1f;
		Debug.Log("Increased limit");
	}

}
