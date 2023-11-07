using TMPro;
using UnityEngine;

public class Fridge : MonoBehaviour {

	[Header("Food Settings")]
	public float cooldown = 1f;

	[Header("Costs")]
	public StaffCosts costs;

	[Header("Graphics")]
	public TextMeshProUGUI cokeCountText;
	public TextMeshProUGUI waterCountText;

	private float cooldownTimer;
	private int cokeCount;
	private int waterCount;

	public int CokeCount {
		get => cokeCount;
		set {
			cokeCount = value;
			cokeCountText.text = cokeCount == 0 ? "No cokes!" : "Cokes: " + cokeCount;
		}
	}

	public int WaterCount {
		get => waterCount;
		set {
			waterCount = value;
			waterCountText.text = waterCount == 0 ? "No water!" : "Water: " + waterCount;
		}
	}

	private void Start() {
		cooldownTimer = cooldown;
		cokeCount = 0;
		waterCount = 0;
	}

	private void Update() {
		if (cooldownTimer <= 0) {
			GenerateDrinks();
			cooldownTimer = cooldown;
		} else {
			cooldownTimer -= Time.deltaTime;
		}
	}

	public void ResetDrinksCount() {
		cokeCount = 0;
		waterCount = 0;
	}

	private void GenerateDrinks() {
		CokeCount++;
		WaterCount++;
	}

}
