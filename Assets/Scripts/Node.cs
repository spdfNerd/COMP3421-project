using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	public NodeType type;

	public Vector3 positionOffset;
    public Vector3 buttonPositionOffset;
    public Color hoverColour;

	private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    public Transform upgradedTowerPrefab;
    public GameObject rotateButton;
    public GameObject upgradeButton;
    public bool isUpgraded;
    private int towerSellPrice;
    private int towerRunningCost;
    private int towerUpgradePrice;

    private Chef chef;
    private Waiter waiter;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        isUpgraded = false;
    }

    public void BuildTower(Transform towerToBuild, Transform upgradedTowerToBuild, StaffCosts costs) {
        LevelManager.Instance.Money -= costs.hirePrice;
		QuestManager.Instance.TryUpdateSpendQuestProgress(costs.hirePrice);

		tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerSellPrice = costs.sellPrice;
        towerRunningCost = costs.runningCost;
        LevelManager.Instance.RunningCost += costs.runningCost;
        towerUpgradePrice = costs.upgradePrice;
        upgradedTowerPrefab = upgradedTowerToBuild;

        if (type == NodeType.KITCHEN) {
            chef = tower.GetComponent<Chef>();
        }
        DisplayTowerUIButtons();
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
        if (tower != null) {
            DisplayTowerUIButtons();
        }
    }

    private void DisplayTowerUIButtons () {
		Transform shopTransform = Shop.Instance.upgradePanel.transform;
        Transform towerTransform = Player.Instance.currentNode.tower.transform;

		Vector3 position = shopTransform.position;
        position.x = towerTransform.position.x;
        position.z = towerTransform.position.z;
        shopTransform.position = position;

        Transform rotateButton = shopTransform.GetChild(0).GetChild(0);
        Transform upgradeButton = shopTransform.GetChild(0).GetChild(1);
        rotateButton.GetComponent<Button>().onClick.AddListener(() => Shop.Instance.Rotate());
        upgradeButton.GetComponent<Button>().onClick.AddListener(() => Shop.Instance.UpgradeTower());

        shopTransform.gameObject.SetActive(true);
    }

	public void OnPlayerExit() {
        rend.material.color = startColour;
        Shop.Instance.upgradePanel.SetActive(false);
	}

}

public enum NodeType {
    
    NORMAL,
    KITCHEN,
    COLLECTION

}
