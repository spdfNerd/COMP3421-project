using UnityEngine;

public class Node : MonoBehaviour {

	public NodeType type;

	public Vector3 positionOffset;
    public Vector3 buttonPositionOffset;
    public Color hoverColour;

	private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    public bool isUpgraded;

    public bool CanUpgrade {
        get => !isUpgraded && tower.TryGetComponent<Staff>(out _) && towerCosts.upgradePrice <= LevelManager.Instance.Money;
    }

    private StaffCosts towerCosts;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        isUpgraded = false;
	}

    public void BuildTower(Transform towerToBuild, StaffCosts costs) {
        LevelManager.Instance.Money -= costs.hirePrice;
        UpdateQuest(costs);

		// Create tower model and update costs
		tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerCosts = costs;

		LevelManager.Instance.RunningCost += costs.runningCost;

        Shop.Instance.EnableUpgradePanel(CanUpgrade, towerCosts.upgradePrice);
    }

    public void UpgradeTower() {
        if (!CanUpgrade) {
            Debug.Log("Cannot upgrade");
			return;
		}

        LevelManager.Instance.Money -= towerCosts.upgradePrice;
        towerCosts.sellPrice += towerCosts.upgradePrice / 2;

        tower.GetComponent<Staff>().Upgrade();
        isUpgraded = true;
        Shop.Instance.DisableUpgradeButton();
    }

    public void SellTower() {
        // if there is no tower on the tile
        if (tower == null) {
            Debug.Log("Nothing to sell");
            return;
        }

        // Destroy tower model and reset related fields
        Destroy(tower.gameObject);
        ResetNode();

        LevelManager.Instance.Money += towerCosts.sellPrice;
        LevelManager.Instance.RunningCost -= towerCosts.runningCost;
    }

    /// <summary>
    /// To be called when the player enters the node
    /// </summary>
	public void OnPlayerEnter() {
        rend.material.color = hoverColour;
        if (tower != null) {
			Shop.Instance.EnableUpgradePanel(CanUpgrade, towerCosts.upgradePrice);
		}
	}

    /// <summary>
    /// To be called when the player exits the node
    /// </summary>
	public void OnPlayerExit() {
		rend.material.color = startColour;
        Shop.Instance.DisableUpgradePanel();
	}

    private void UpdateQuest(StaffCosts costs) {
		QuestManager.Instance.TryUpdateSpendQuestProgress(costs.hirePrice);
	}

    private void ResetNode() {
		tower = null;
		isUpgraded = false;
	}

}

public enum NodeType {
    
    NORMAL,
    KITCHEN,
    COLLECTION

}
