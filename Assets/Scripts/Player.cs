using System;
using UnityEngine;

public class Player : MonoBehaviour {

	public float movementSpeed = 40f;

	[HideInInspector]
	public Node currentNode;

	private bool[] directions = new bool[] { false, false, false, false };
	private Vector3 velocity = Vector3.zero;

	private void Update() {
		GetVelocity();
		Move();
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

}
