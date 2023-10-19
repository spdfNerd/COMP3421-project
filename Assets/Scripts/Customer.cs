using UnityEngine;

public class Customer : MonoBehaviour {

	public int loopCount = 2;
	public float speed = 10f;
	public int worth = 50;

	private LevelManager levelManager;
	private WaveManager waveManager;

	private Transform waypoints;
	private Transform startWaypoints;
	private Transform mainWaypoints;
	private Transform endWaypoints;

	private Transform nextWaypoint;
	private int startWaypointCounter = 1;
	private int mainWaypointCounter = 0;
	private int endWaypointCounter = 0;
	private int loopCounter = 0;

	private void Start() {
		levelManager = FindFirstObjectByType<LevelManager>();
		waveManager = FindFirstObjectByType<WaveManager>();

		waypoints = waveManager.waypoints;

		startWaypoints = waypoints.GetChild(0);
		mainWaypoints = waypoints.GetChild(1);
		endWaypoints = waypoints.GetChild(2);

		nextWaypoint = GetNextWaypoint();
	}

	public void Update() {
		Move();
	}

	private void Move() {
		Vector3 direction = nextWaypoint.position - transform.position;
		direction = direction.normalized;
		transform.Translate(direction * speed * Time.deltaTime, Space.World);

		if (Vector3.Distance(transform.position, nextWaypoint.position) <= 0.1f) {
			nextWaypoint = GetNextWaypoint();
		}
	}

	private Transform GetNextWaypoint() {
		if (startWaypointCounter >= startWaypoints.childCount) {
			if (loopCounter >= loopCount || (loopCounter == loopCount - 1 && mainWaypointCounter == mainWaypoints.childCount)) {
				if (endWaypointCounter >= endWaypoints.childCount) {
					Die(true);
					return null;
				} else {
					return endWaypoints.GetChild(endWaypointCounter++);
				}
			} else {
				if (mainWaypointCounter >= mainWaypoints.childCount) {
					mainWaypointCounter = 0;
					loopCounter++;
				}
				return mainWaypoints.GetChild(mainWaypointCounter++);
			}
		} else {
			return startWaypoints.GetChild(startWaypointCounter++);
		}
	}

	public void Die(bool survived) {
		if (survived) {
			levelManager.Reputation--;
		} else {
			levelManager.Money += worth;
		}
		waveManager.DecrementEnemyCount();
		Destroy(gameObject);
	}

}
