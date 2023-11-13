using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

	[Header("Player Stats")]
	public int startingMoney;
	public int startingReputation;

	[Header("HUD Objects")]
	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI reputationText;
	public TextMeshProUGUI roundsText;

	[Header("Map Settings")]
	public Transform nodeParent;
	public Transform node;
	public Transform kitchenNodeParent;
	public Transform kitchenNode;
	public Transform collectionNode;
	public int mapWidth = 24;
	public int mapHeight = 24;
	public Vector2 collectionNodePosition;
	public Vector2 kitchenAreaStart;
	public bool isEndlessMode;

	[Header("Map Constraints")]
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
		set {
			round = value;
			roundsText.text = "Round " + round;
		}
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

		GenerateNodes();
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

	private void GenerateNodes() {
		for (int i = -mapWidth / 2; i < mapWidth / 2 + 1; i++) {
			for (int j = -mapHeight / 2; j < mapHeight / 2 + 1; j++) {
				if (collectionNodePosition == new Vector2(i, j)) {
					Instantiate(collectionNode, new Vector3(i * 4, 0f, j * 4), Quaternion.identity, kitchenNodeParent);
				} else {
					bool isKitchenArea = i >= kitchenAreaStart.x && j >= kitchenAreaStart.y;
					Transform nodeToInstantiate = isKitchenArea ? kitchenNode : node;
					Transform nodeParent = isKitchenArea ? kitchenNodeParent : this.nodeParent;
					Instantiate(nodeToInstantiate, new Vector3(i * 4, 0f, j * 4), Quaternion.identity, nodeParent);
				}
			}
		}
	}

	public GameObject GetTowerToBuild() {
        return towerToBuild;
    }

    public void SetTowerToBuild(GameObject tower) {
        towerToBuild = tower;
    }

}
