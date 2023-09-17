using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform waypoints;
	public int loopCount = 2;

	private Transform startWaypoints;
	private Transform mainWaypoints;
	private Transform endWaypoints;

	private Transform nextWaypoint;
	private int startWaypointCounter = 1;
	private int mainWaypointCounter = 0;
	private int endWaypointCounter = 0;
	private int loopCounter = 0;

	private void Start() {
		startWaypoints = waypoints.GetChild(0);
		mainWaypoints = waypoints.GetChild(1);
		endWaypoints = waypoints.GetChild(2);

		nextWaypoint = GetNextWaypoint();
	}

	public void Update() {
		Move();
	}

	private void Move() {
		Vector2 direction = nextWaypoint.position - transform.position;
		direction = direction.normalized;
		transform.Translate(direction * 10f * Time.deltaTime, Space.World);

		if (Vector2.Distance(transform.position, nextWaypoint.position) <= 0.1f) {
			nextWaypoint = GetNextWaypoint();
		}
	}

	private Transform GetNextWaypoint() {
		if (startWaypointCounter >= startWaypoints.childCount) {
			if (loopCounter >= loopCount || (loopCounter == loopCount - 1 && mainWaypointCounter == mainWaypoints.childCount)) {
				if (endWaypointCounter >= endWaypoints.childCount) {
					Destroy();
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

	private void Destroy() {
		Destroy(gameObject);
	}

}
