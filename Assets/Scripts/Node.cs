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
    public GameObject rotateButton;
    public GameObject upgradeButton;
    public bool isUpgraded;

    private int towerSellPrice;
    private int towerRunningCost;
    private int towerUpgradePrice;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        isUpgraded = false;
    }

    public void BuildTower(Transform towerToBuild, StaffCosts costs) {
        LevelManager.Instance.Money -= costs.hirePrice;
        UpdateQuest(costs);

		// Create tower model and update related fields
		tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerSellPrice = costs.sellPrice;
        towerRunningCost = costs.runningCost;
        towerUpgradePrice = costs.upgradePrice;
        LevelManager.Instance.RunningCost += costs.runningCost;

        DisplayTowerUIButtons();
    }

    public void UpgradeTower() {
        if (!BuildManager.Instance.CanUpgrade(towerUpgradePrice)) {
            Debug.Log("Cannot upgrade");
			return;
		}

        LevelManager.Instance.Money -= towerUpgradePrice;
        towerSellPrice += towerUpgradePrice / 2;

        tower.GetComponent<Staff>().Upgrade();
        isUpgraded = true;
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

        LevelManager.Instance.Money += towerSellPrice;
        LevelManager.Instance.RunningCost -= towerRunningCost;
    }

    /// <summary>
    /// To be called when the player enters the node
    /// </summary>
	public void OnPlayerEnter() {
        rend.material.color = hoverColour;
        if (tower != null) {
            DisplayTowerUIButtons();
        }
	}

    /// <summary>
    /// To be called when the player exits the node
    /// </summary>
	public void OnPlayerExit() {
		rend.material.color = startColour;

		Transform shopTransform = Shop.Instance.upgradePanel.transform;
		Transform rotateButton = shopTransform.GetChild(0).GetChild(0);
		Transform upgradeButton = shopTransform.GetChild(0).GetChild(1);

        // Make sure the buttons will not do anything relating to this node when clicked
		rotateButton.GetComponent<Button>().onClick.RemoveAllListeners();
		upgradeButton.GetComponent<Button>().onClick.RemoveAllListeners();
		
        Shop.Instance.upgradePanel.SetActive(false);
	}

    /// <summary>
    /// Display the upgrade and rotate buttons related to the tower
    /// </summary>
	private void DisplayTowerUIButtons () {
		Transform shopTransform = Shop.Instance.upgradePanel.transform;
        Transform towerTransform = Player.Instance.GetCurrentTowerTransform();

        // Set buttons position
		Vector3 position = towerTransform.position;
        position.y = shopTransform.position.y;
        shopTransform.position = position;

        // Link button functions so they upgrade and rotate tower when clicked
        Transform rotateButton = shopTransform.GetChild(0).GetChild(0);
        Transform upgradeButton = shopTransform.GetChild(0).GetChild(1);
        rotateButton.GetComponent<Button>().onClick.AddListener(() => Shop.Instance.Rotate());
        upgradeButton.GetComponent<Button>().onClick.AddListener(() => Shop.Instance.UpgradeTower());

        shopTransform.gameObject.SetActive(true);
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
