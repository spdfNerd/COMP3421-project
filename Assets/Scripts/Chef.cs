using TMPro;
using UnityEngine;

public class Chef : MonoBehaviour {

	public float cooldown = 1f;
	public FoodType foodType;
	public TextMeshProUGUI foodCountText;
	
	private float cooldownTimer;
	private int foodCount;

	private void Start() {
		cooldownTimer = cooldown;
		foodCount = 0;
		foodCountText.text = "";
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
		if (foodCount == 0) {
			foodCountText.text = "";
		} else {
			foodCountText.text = foodCount.ToString();
		}
	}

}
