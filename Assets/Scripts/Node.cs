using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    LevelManager levelManager;

    public Vector3 positionOffset;
    public Color hoverColour;
    private Renderer rend;
    private Color startColour;
    
    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    public Vector3 GetBuildPosition ()
	{
		return transform.position + positionOffset;
	}

    void OnMouseDown () {
        Debug.Log("Pressed");

        // no tower selected to build
        if (levelManager.GetTowerToBuild() == null)
            return;

        // if (tower == null)
		// {
        //     Debug.Log("yikessss");
		// 	return;
		// }

        // if there is already a tower on the tile
        if (tower != null)
		{
            Debug.Log("Can't build there, shall sell now");
            SellTower(tower);
			return;
		}

        BuildTower(levelManager.GetTowerToBuild());
		
    }

    void BuildTower (GameObject towerToBuild) {
        if (levelManager.Money < 350)
		{
			Debug.Log("Not enough money to build that!");
			return;
		}

		// PlayerStats.Money -= blueprint.cost;
        // levelManager.Money -= 20;
        Debug.Log("Money left " + levelManager.Money);

		GameObject _tower = (GameObject)Instantiate(levelManager.sushiTowerPrefab, GetBuildPosition(), Quaternion.identity);
		tower = _tower;

		// towerBlueprint = blueprint;

		// GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Debug.Log("Tower built!");
    }

    void SellTower (GameObject towerToSell) {
        // PlayerStats.Money += turretBlueprint.GetSellAmount();


		// GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		// Destroy(effect, 5f);

		Destroy(tower);
		tower = null;
    }

    void OnMouseEnter () {
        rend.material.color = hoverColour;
    }

    void OnMouseExit () {
        rend.material.color = startColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
