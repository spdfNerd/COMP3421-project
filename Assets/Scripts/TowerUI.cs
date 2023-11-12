using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public static TowerUI Instance;
    public Transform rotateButtonPrefab;
    public Transform upgradeButtonPrefab;
    public Vector3 butttonsPositionOffset;
    public Transform rotateButton;
    public Transform upgradeButton;
    

    // private void Start() {
	// 	if (Instance != null) {
	// 		Debug.LogError("More than one TowerUI in scene!");
	// 		return;
	// 	}
	// 	Instance = this;
	// }

    public void Rotate () {
        if (Player.Instance.currentNode.tower != null) {
            Player.Instance.currentNode.tower.transform.Rotate(0, 90, 0); 
        }
    }

    public void UpgradeTower() {
        Player.Instance.currentNode.UpgradeTower();
    }

    // public void DisplayButtons() {
    //     Debug.Log("attempting to display");
    //     rotateButton = Instantiate(upgradeButtonPrefab, Player.Instance.GetPosition() + butttonsPositionOffset, Quaternion.identity);
    // }

    // public void RotateRight () {å
    //     Player.Instance.currentNode.tower.transform.Rotate(0, 90, 0); 
    // }


    // Update is called once per frame
    void Update()
    {
        
    }
}
