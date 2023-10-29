using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance;

	public int startingMoney;
	public int startingReputation;

    public GameObject sushiTowerPrefab;
    public GameObject burgerTowerPrefab;
    public GameObject pizzaTowerPrefab;
    public GameObject noodlesTowerPrefab;
    public GameObject waiterTowerPrefab;
    public GameObject fridgeTowerPrefab;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI reputationText;

	private int money = 0;
	private int reputation = 0;
	private int round = 0;

	private GameObject towerToBuild;
    private int runningCost = 0;

	public int Money {
		get => money;
		set {
			money = value;
			moneyText.text = "$" + money;
		}
	}

    public int RunningCost {
		get => runningCost;
		set => runningCost = value;
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
			return;
		}
		instance = this;
	}

	private void Start() {
		Money = startingMoney;
		Reputation = startingReputation;
	}

    public GameObject GetTowerToBuild () {
        return towerToBuild;
    }

    public void SetTowerToBuild (GameObject tower) {
        towerToBuild = tower;
    }

}
