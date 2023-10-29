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
        Debug.Log("Money left " + levelManager.Money);

		GameObject _tower = (GameObject)Instantiate(towerToBuild, player.GetPosition(), Quaternion.identity);
		tower = _tower;
        towerHirePrice = selectedTowerHirePrice;
        towerSellPrice = selectedTowerSellPrice;
        towerRunningCost = selectedTowerRunningCost;
        levelManager.RunningCost += towerRunningCost;
		// GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetPlayerPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Debug.Log("Tower built!");
        Debug.Log(levelManager.RunningCost);
    }

    public void DestroyTower () {
        // if there is no tower on the tile
        if (tower == null)
		{
            Debug.Log("Nothing to sell");
			return;
		}
		// GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetPlayerPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Destroy(tower);
		tower = null;

        levelManager.Money += towerSellPrice;
        Debug.Log("Money left " + levelManager.Money);
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
