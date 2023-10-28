using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

	public int startingMoney;
	public int startingReputation;
    
	public GameObject sushiTowerPrefab;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI reputationText;

	public Transform frontWall;
	public Transform backWall;
	public Transform leftWall;
	public Transform rightWall;

	private int money = 0;
	private int reputation = 0;
	private int round = 0;

	private GameObject towerToBuild;

	public int Money {
		get => money;
		set {
			money = value;
			moneyText.text = "$" + money;
		}
	}

	public int Reputation {
		get => reputation;
		set {
			reputation = value;
			reputationText.text = reputation + " Reputation";
		}
	}

	public int Round {
		get => round;
		set => round = value;
	}

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one LevelManager in scene!");
			// Debug.LogError("More than one LevelManager in scene!");
			return;
		}
		instance = this;
	}

	private void Start() {
		Money = startingMoney;
		Reputation = startingReputation;

	}

    public GameObject GetTowerToBuild () {
        if (towerToBuild == null)
		{
            Debug.Log("brooooo null tower");
		}
        return towerToBuild;
    }

    public void SetTowerToBuild (GameObject tower) {
        // Debug.Log(tower);
        if (tower != null)
		{
            Debug.Log("building ayyy");
		}
        towerToBuild = tower;
    }

}
