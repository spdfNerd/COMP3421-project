﻿using UnityEngine;

public class BuildManager : MonoBehaviour {

	// Singleton instance
	public static BuildManager Instance;

	[HideInInspector]
	public Transform towerToBuild;

	private void Awake() {
		// Ensure unique singleton instance in scene
		if (Instance != null) {
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		Instance = this;
	}

	public void SetTowerToBuild(Transform towerPrefab) {
		towerToBuild = towerPrefab;
	}

	public void BuildTower(StaffCosts costs) {
		if (CheckCanBuild(costs.hirePrice)) {
			Player.Instance.currentNode.BuildTower(towerToBuild, costs);
		}
	}

	public void Rotate() {
		if (Player.Instance.GetCurrentTowerTransform() != null) {
			Staff staff = Player.Instance.GetCurrentTowerTransform().GetComponent<Staff>();
			if (staff == null) {
				return;
			}
			staff.GetActiveGFX().transform.Rotate(0, 90, 0);
		}
	}

	public void UpgradeTower() {
		Player.Instance.currentNode.UpgradeTower();
	}

	public bool CheckCanBuild(int hirePrice) {
		// Check player is over a tile
		if (Player.Instance.currentNode == null) {
			Debug.Log("No node!");
			return false;
		}

		// Check if there is already a tower on the tile
		if (Player.Instance.GetCurrentTowerTransform() != null) {
			Debug.Log("Can't build there");
			return false;
		}

		// Check that there is enough money to build
		if (LevelManager.Instance.Money < hirePrice) {
			Debug.Log("Not enough money to build that!");
			return false;
		}

		return true;
	}

}
