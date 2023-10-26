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
    private int towerPrice;
    private int runningCost;
    
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        player = Player.instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    // void BuyTower () {
    //     Debug.Log("Pressed");

    //     // no tower selected to build
    //     if (levelManager.GetTowerToBuild() == null)
    //         return;

    //     // if (tower == null)
	// 	// {
    //     //     Debug.Log("yikessss");
	// 	// 	return;
	// 	// }

    //     // if there is already a tower on the tile
    //     if (tower != null)
	// 	{
    //         Debug.Log("Can't build there, shall sell now");
    //         SellTower(tower);
	// 		return;
	// 	}

    //     BuildTower(levelManager.GetTowerToBuild());
		
    // }

    public void BuildTower (GameObject towerToBuild, int selectedTowerPrice) {
        // if there is already a tower on the tile
        if (tower != null)
		{
            Debug.Log("Can't build there");
			return;
		}

        if (levelManager.Money < selectedTowerPrice)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}
        levelManager.Money -= selectedTowerPrice;
        Debug.Log("Money left " + levelManager.Money);

		GameObject _tower = (GameObject)Instantiate(towerToBuild, player.GetPosition(), Quaternion.identity);
		tower = _tower;
        towerPrice = selectedTowerPrice;
		// GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetPlayerPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Debug.Log("Tower built!");
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

        levelManager.Money += towerPrice;
        Debug.Log("Money left " + levelManager.Money);
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
