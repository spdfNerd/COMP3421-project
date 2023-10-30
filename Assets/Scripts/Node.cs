using UnityEngine;

public class Node : MonoBehaviour {

    private LevelManager levelManager;
    private Player player;

    public Vector3 positionOffset;
    public Color hoverColour;
    private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    private int towerSellPrice;
    private int towerRunningCost;

	private void Start() {
        levelManager = LevelManager.Instance;
        player = Player.Instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    public void BuildTower(Transform towerToBuild, int hirePrice, int sellPrice, int runningCost) {
        levelManager.Money -= hirePrice;

        Transform tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        this.tower = tower;
        towerSellPrice = sellPrice;
        towerRunningCost = runningCost;
        levelManager.RunningCost += runningCost;

        Debug.Log("Tower built!");
    }

    public void SellTower() {
        // if there is no tower on the tile
        if (tower == null) {
            Debug.Log("Nothing to sell");
            return;
        }

        Destroy(tower);
        tower = null;

        levelManager.Money += towerSellPrice;
        levelManager.RunningCost -= towerRunningCost;
    }

    public void OnPlayerEnter() {
        rend.material.color = hoverColour;
    }

    public void OnPlayerExit() {
        rend.material.color = startColour;
    }

}
