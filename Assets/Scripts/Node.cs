using System;
using UnityEngine;

public class Node : MonoBehaviour
{

    // LevelManager levelManager;
    
    public GameObject tower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnMouseDown () {
        Debug.Log("Pressed");

        // if (tower == null)
		// {
		// 	return;
		// }

        LevelManager.Money -= 20;
        Debug.Log("Money left " + LevelManager.Money);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
