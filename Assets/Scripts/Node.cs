using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

    public Vector3 positionOffset;
    // public Vector3 butttonsPositionOffset;
    public Color hoverColour;
    private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    public Transform upgradedTowerPrefab;
    // public Transform towerUIButtons;
    // public Transform towerUIButtonsPrefab;
    // public Transform towerUIButtons;
    // public Transform towerUIButtonsPrefab;
    // public Transform rotateButtonPrefab;
    // public Transform upgradeButtonPrefab;
    // public Transform rotateButton;
    // public Transform upgradeButton;
    public bool isUpgraded;
    private int towerSellPrice;
    private int towerRunningCost;
    private int towerUpgradePrice;

    private Kitchen kitchen;
    private Chef chef;
    private Waiter waiter;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        isUpgraded = false;

        kitchen = transform.parent.GetComponent<Kitchen>();
    }

    public void BuildTower(Transform towerToBuild, Transform upgradedTowerToBuild, int hirePrice, int sellPrice, int runningCost, int upgradePrice) {
        LevelManager.Instance.Money -= hirePrice;

        tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerSellPrice = sellPrice;
        towerRunningCost = runningCost;
        LevelManager.Instance.RunningCost += runningCost;
        towerUpgradePrice = upgradePrice;
        upgradedTowerPrefab = upgradedTowerToBuild;

        // if (kitchen != null) {
            chef = tower.GetComponent<Chef>();
        // }
    }

    public void UpgradeTower() {
        if (!BuildManager.Instance.CheckCanUpgrade(towerUpgradePrice)) {
            Debug.Log("checked can upgrade");
			return;
		}

        // increase the sell price and running cost after upgrading?
        LevelManager.Instance.Money -= towerUpgradePrice;

        Destroy(tower.gameObject);
        tower = Instantiate(upgradedTowerPrefab, transform.position + positionOffset, Quaternion.identity);
        chef = tower.GetComponent<Chef>();
        waiter = tower.GetComponent<Waiter>();
        if (chef != null) {
            chef.Upgrade();
        } else if (waiter != null) {
            waiter.Upgrade();
        }

        isUpgraded = true;

        Debug.Log("upgraded");
    }

    public void SellTower() {
        // if there is no tower on the tile
        if (tower == null) {
            Debug.Log("Nothing to sell");
            return;
        }

        Destroy(tower.gameObject);
        tower = null;
        chef = null;
        waiter = null;
        isUpgraded = false;

        LevelManager.Instance.Money += towerSellPrice;
        LevelManager.Instance.RunningCost -= towerRunningCost;
    }

	public void OnPlayerEnter() {
        rend.material.color = hoverColour;
        // if (tower != null) {
        //     // TowerUI.Instance.DisplayButtons();
        //     // towerUIButtons = Instantiate(towerUIButtonsPrefab);
        //     upgradeButton = Instantiate(upgradeButtonPrefab, towerUIButtons.transform);
        //     // upgradeButton = Instantiate(upgradeButtonPrefab,Player.Instance.GetPosition() + butttonsPositionOffset, Quaternion.identity);
        //     // upgradeButton = Instantiate<GameObject>(upgradeButtonPrefab, towerUIButtons);

        // }

    }

    public void OnPlayerExit() {
        rend.material.color = startColour;
    }

}
