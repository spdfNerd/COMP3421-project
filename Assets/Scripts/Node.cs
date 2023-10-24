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
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        player = Player.instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    // public Vector3 GetPlayerPosition ()
	// {
	// 	return player.transform.position;
	// }

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

    public void BuildTower (GameObject towerToBuild) {
        if (levelManager.Money < 350)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

        if (tower != null)
		{
            Debug.Log("Can't build there");
            // SellTower(Node.tower);
			return;
		}

		// PlayerStats.Money -= blueprint.cost;
        // levelManager.Money -= 20;
        Debug.Log("Money left " + levelManager.Money);

		GameObject _tower = (GameObject)Instantiate(levelManager.sushiTowerPrefab, player.GetPosition(), Quaternion.identity);
		tower = _tower;

		// towerBlueprint = blueprint;

		// GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetPlayerPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Debug.Log("Tower built!");
    }

    void SellTower (GameObject towerToSell) {
        // PlayerStats.Money += turretBlueprint.GetSellAmount();


		// GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetPlayerPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Destroy(tower);
		tower = null;
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
        // if (transform.position == player.GetPosition()) {
        //     rend.material.color = hoverColour;
        // } else {
        //     rend.material.color = startColour;
        // }
        // Debug.Log(player.GetPosition());
    }
}
