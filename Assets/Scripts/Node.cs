using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    LevelManager levelManager;

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
            Debug.Log("Can't build there");
			return;
		}

		levelManager.Money -= 20;
        Debug.Log("Money left " + levelManager.Money);
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
