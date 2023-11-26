using TMPro;
using UnityEngine;

public class Fridge : MonoBehaviour {

	[Header("Food Settings")]
	public float cooldown = 1f;
	public int waterLimit;
	public int cokeLimit;

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
			// Make sure that the coke is between 0 and the coke limit
			cokeCount = Mathf.Clamp(cokeCount, 0, cokeLimit);
			cokeCountText.text = cokeCount == 0 ? "No cokes!" : "Cokes: " + cokeCount;
			// Change the text colour to red if the coke limit is reached
			cokeCountText.color = cokeCount == cokeLimit ? Color.red : Color.white;
		}
	}

	public int WaterCount {
		get => waterCount;
		set {
			waterCount = value;
			// Make sure that the water is between 0 and the water limit
			waterCount = Mathf.Clamp(waterCount, 0, waterLimit);
			waterCountText.text = waterCount == 0 ? "No water!" : "Water: " + waterCount;
			// Change the text colour to red if the water limit is reached
			waterCountText.color = waterCount == waterLimit ? Color.red : Color.white;
		}
	}

	private void Start() {
		cooldownTimer = cooldown;
		cokeCount = 0;
		waterCount = 0;
	}

	private void Update() {
		// Produce drinks if countdown timer is finished counting down
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

	/// <summary>
	/// Increment each of coke and water count
	/// </summary>
	private void GenerateDrinks() {
		CokeCount++;
		WaterCount++;
	}

}
