using UnityEngine;

public class Node : MonoBehaviour {

	public NodeType type;

	public Vector3 positionOffset;
    public Color hoverColour;

	private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    private int towerSellPrice;
    private int towerRunningCost;

    private Chef chef;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    public void BuildTower(Transform towerToBuild, StaffCosts costs) {
        LevelManager.Instance.Money -= costs.hirePrice;

        tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerSellPrice = costs.sellPrice;
        towerRunningCost = costs.runningCost;
        LevelManager.Instance.RunningCost += costs.runningCost;

        if (type == NodeType.KITCHEN) {
            chef = tower.GetComponent<Chef>();
        }
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

        LevelManager.Instance.Money += towerSellPrice;
        LevelManager.Instance.RunningCost -= towerRunningCost;
    }

	public void OnPlayerEnter() {
        rend.material.color = hoverColour;
    }

    public void OnPlayerExit() {
        rend.material.color = startColour;
    }

}

public enum NodeType {
    
    NORMAL,
    KITCHEN,
    COLLECTION

}
