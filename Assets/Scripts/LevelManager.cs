using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// Singleton instance
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

    private int runningCost = 30;
	private int maxReputation = 50;

	public int Money {
		get => money;
		set {
			money = value;
			moneyText.text = string.Format("${0} (Running Cost: ${1})", money, runningCost);
		}
	}

    public int RunningCost {
		get => runningCost;
		set {
			runningCost = value;
			moneyText.text = string.Format("${0} (Running Cost: ${1})", money, runningCost);
		}
	}

	public int Reputation {
		get => reputation;
		set {
			reputation = value;
			reputation = Mathf.Clamp(reputation, 0, maxReputation);
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
		// Ensure unique singleton instance in scene
		if (Instance != null) {
			Debug.Log("More than one LevelManager in scene!");
			return;
		}
		Instance = this;
	}

	private void Start() {
		Money = startingMoney;
		Reputation = startingReputation;

		GenerateNodes();
	}

	private void GenerateNodes() {
		for (int i = -mapWidth / 2; i < mapWidth / 2 + 1; i++) {
			for (int j = -mapHeight / 2; j < mapHeight / 2 + 1; j++) {
				Vector3 nodePos = new Vector3(i * 4, 0f, j * 4);
				if (collectionNodePosition == new Vector2(i, j)) {
					Instantiate(collectionNode, nodePos, Quaternion.identity, kitchenNodeParent);
				} else {
					bool isKitchenArea = i >= kitchenAreaStart.x && j >= kitchenAreaStart.y;
					Transform nodeToInstantiate = isKitchenArea ? kitchenNode : node;
					Transform nodeParent = isKitchenArea ? kitchenNodeParent : this.nodeParent;
					Instantiate(nodeToInstantiate, nodePos, Quaternion.identity, nodeParent);
				}
			}
		}
	}

}
