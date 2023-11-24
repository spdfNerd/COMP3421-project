using TMPro;
using UnityEngine;

public abstract class Staff : MonoBehaviour {

	[Header("Costs")]
	public StaffCosts costs;

	[Header("Graphics")]
	public GameObject baseGFX;
	public GameObject upgradedGFX;
	public TextMeshProUGUI foodCountText;

	private void Start() {
		// Make sure only the base model is active
		baseGFX.SetActive(true);
		upgradedGFX.SetActive(false);

		InitStaff();
	}

	public void Upgrade() {
		// Disable base model and enable upgraded model
		baseGFX.SetActive(false);
		upgradedGFX.SetActive(true);

		UpgradeStats();
	}

	public GameObject GetActiveGFX() {
		if (upgradedGFX.activeSelf) {
			return upgradedGFX;
		} else {
			return baseGFX;
		}
	}

	protected abstract void InitStaff();
	protected abstract void UpgradeStats();

}
