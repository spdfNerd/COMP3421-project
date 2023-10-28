using System;
using UnityEngine;

public class Player : MonoBehaviour {

	public float movementSpeed = 40f;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;

	private bool[] directions = new bool[] { false, false, false, false };
	private Vector3 velocity = Vector3.zero;

	[HideInInspector]
	public Node currentNode;

	private void Start() {
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		float width = collider.radius;
		minX = LevelManager.Instance.leftWall.position.x + width;
		maxX = LevelManager.Instance.rightWall.position.x - width;
		minZ = LevelManager.Instance.frontWall.position.z + width;
		maxZ = LevelManager.Instance.backWall.position.z - width;
	}

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
		float xPos = Mathf.Clamp(transform.position.x, minX, maxX);
		float zPos = Mathf.Clamp(transform.position.z, minZ, maxZ);
		transform.position = new Vector3(xPos, transform.position.y, zPos);
	}

}
