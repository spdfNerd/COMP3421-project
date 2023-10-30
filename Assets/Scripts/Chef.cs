using TMPro;
using UnityEngine;

public class Chef : MonoBehaviour {

	public float cooldown = 1f;
	public FoodType foodType;
	public TextMeshProUGUI foodCountText;

	public int hirePrice;
	public int sellPrice;
	public int runningCost;

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
		foodCountText.text = foodCount == 0 ? "" : foodCount.ToString();
	}

}
