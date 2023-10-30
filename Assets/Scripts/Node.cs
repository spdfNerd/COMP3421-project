using UnityEngine;

public class Node : MonoBehaviour {

    public Vector3 positionOffset;
    public Color hoverColour;
    private Color startColour;
    private Renderer rend;

    [HideInInspector]
    public Transform tower;
    private int towerSellPrice;
    private int towerRunningCost;

    private Kitchen kitchen;
    private Chef chef;

    private void Start() {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;

        kitchen = transform.parent.GetComponent<Kitchen>();
    }

    public void BuildTower(Transform towerToBuild, int hirePrice, int sellPrice, int runningCost) {
        LevelManager.Instance.Money -= hirePrice;

        tower = Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.identity);
        towerSellPrice = sellPrice;
        towerRunningCost = runningCost;
        LevelManager.Instance.RunningCost += runningCost;

        if (kitchen != null) {
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
