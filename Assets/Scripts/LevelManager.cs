using System.Collections;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public int startingMoney;
    public int startingReputation;

    public Transform waypoints;
    public Transform enemyPrefab;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI reputationText;

    private int money = 0;
    private int reputation = 0;

    private int enemyCount = 0;
    private Transform spawnpoint;

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

    private void Start() {
        Money = startingMoney;
        Reputation = startingReputation;

        spawnpoint = waypoints.GetChild(0).GetChild(0);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (enemyCount < 10) {
            Instantiate(enemyPrefab, spawnpoint.position, Quaternion.identity);
            enemyCount++;
            yield return new WaitForSeconds(1f);
        }
    }

}
