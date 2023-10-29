using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    LevelManager levelManager;
    Player player;

    public Vector3 positionOffset;
    public Color hoverColour;
    private Renderer rend;
    private Color startColour;
    
    public GameObject tower;
    private int towerHirePrice;
    private int towerSellPrice;
    private int towerRunningCost;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        player = Player.instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    public void BuildTower (GameObject towerToBuild, int selectedTowerHirePrice, int selectedTowerSellPrice, int selectedTowerRunningCost) {
        // if there is already a tower on the tile
        if (tower != null)
		{
            Debug.Log("Can't build there");
			return;
		}

        if (levelManager.Money < selectedTowerHirePrice)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}
        levelManager.Money -= selectedTowerHirePrice;

		GameObject _tower = (GameObject)Instantiate(towerToBuild, player.GetPosition(), Quaternion.identity);
		tower = _tower;
        towerHirePrice = selectedTowerHirePrice;
        towerSellPrice = selectedTowerSellPrice;
        towerRunningCost = selectedTowerRunningCost;
        levelManager.RunningCost += towerRunningCost;

		Debug.Log("Tower built!");
    }

    public void DestroyTower () {
        // if there is no tower on the tile
        if (tower == null)
		{
            Debug.Log("Nothing to sell");
			return;
		}

		Destroy(tower);
		tower = null;

        levelManager.Money += towerSellPrice;
        levelManager.RunningCost -= towerRunningCost;
    }

    public void OnPlayerEnter () {
        rend.material.color = hoverColour;
    }

    public void OnPlayerExit () {
        rend.material.color = startColour;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
