using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public int startingMoney;
	public int startingReputation;

	public TextMeshProUGUI moneyText;
	public TextMeshProUGUI reputationText;

	private int money = 0;
	private int reputation = 0;
	private int round = 0;

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

	private void Start() {
		Money = startingMoney;
		Reputation = startingReputation;

	}

}
