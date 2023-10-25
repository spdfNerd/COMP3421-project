using System;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance;
	[HideInInspector]
	public Node currentNode;
	[HideInInspector]
	public Node previousNode;
	public float movementSpeed = 40f;

	private bool[] directions = new bool[] { false, false, false, false };
	private Vector3 velocity = Vector3.zero;

	void Awake() {
		if (instance != null) {
			Debug.Log("More than one Player in scene!");
			// Debug.LogError("More than one LevelManager in scene!");
			return;
		}
		instance = this;
	}

	private void Update() {
		GetVelocity();
		Move();
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit)) {
			Node node = hit.transform.GetComponent<Node>();
			if (node != null && node != currentNode) {
				previousNode = currentNode;
				currentNode = node;
				currentNode.OnPlayerEnter();
				previousNode.OnPlayerExit();
			}
		}
	}
	private void GetVelocity() {
		directions[0] = Input.GetKey(KeyCode.W);
		directions[1] = Input.GetKey(KeyCode.A);
		directions[2] = Input.GetKey(KeyCode.S);
		directions[3] = Input.GetKey(KeyCode.D);

		Vector3 zDir = Vector3.forward * (Convert.ToInt32(directions[0]) - Convert.ToInt32(directions[2]));
		Vector3 xDir = Vector3.right * (Convert.ToInt32(directions[3]) - Convert.ToInt32(directions[1]));

		velocity = (xDir + zDir) * movementSpeed * Time.deltaTime;
	}

	private void Move() {
		transform.Translate(velocity);
	}

	public Vector3 GetPosition ()
	{
		return transform.position;
	}

}
