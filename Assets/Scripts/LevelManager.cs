using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

	public int startingMoney;
	public int startingReputation;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI reputationText;

	public Transform nodeParent;
	public Transform node;
	public Transform kitchenNode;
	public int mapWidth = 24;
	public int mapHeight = 24;

	public Transform frontWall;
	public Transform backWall;
	public Transform leftWall;
	public Transform rightWall;

	private int money = 0;
	private int reputation = 0;
	private int round = 0;

	private GameObject towerToBuild;
    private int runningCost = 0;

	public Dictionary<FoodType, int> foodRewards;

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

	private void Awake() {
		if (Instance != null) {
			Debug.Log("More than one LevelManager in scene!");
			return;
		}
		Instance = this;
	}

	private void Start() {
		Money = startingMoney;
		Reputation = startingReputation;
		InitFoodRewards();

		for (int i = -mapWidth / 2; i < mapWidth / 2 + 1; i++) {
			for (int j = -mapHeight / 2; j < mapHeight / 2 + 1; j++) {
				Instantiate(i >= 2 && j >= 6 ? kitchenNode : node, new Vector3(i * 4, 0f, j * 4), Quaternion.identity, nodeParent);
			}
		}
	}

	private void InitFoodRewards() {
		foodRewards = new Dictionary<FoodType, int>();
		foodRewards.Add(FoodType.PIZZA, 50);
		foodRewards.Add(FoodType.BURGER, 100);
		foodRewards.Add(FoodType.SUSHI, 125);
		foodRewards.Add(FoodType.NOODLE, 150);
		foodRewards.Add(FoodType.COKE, 40);
		foodRewards.Add(FoodType.WATER, 20);
	}


	public GameObject GetTowerToBuild() {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower) {
        towerToBuild = tower;
    }

}
