using System;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player Instance;
	[HideInInspector]
	public Node currentNode;
	[HideInInspector]
	public Node previousNode;
	public float movementSpeed = 40f;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;

	private bool[] directions = new bool[] { false, false, false, false };
	private Vector3 velocity = Vector3.zero;

	private void Awake() {
		if (Instance != null) {
			Debug.Log("More than one Player in scene!");
			return;
		}
		Instance = this;
	}

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
		UpdateCurrentNode();
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

	private void UpdateCurrentNode() {
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		float rayLength = collider.height / 2f + 0.5f;

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength)) {
			Node node = hit.transform.GetComponent<Node>();
			if (node != null && node != currentNode) {
				previousNode = currentNode;
				currentNode = node;
				currentNode.OnPlayerEnter();
			}
		} else {
			previousNode = currentNode;
			currentNode = null;
		}

		if (previousNode != null) {
			previousNode.OnPlayerExit();
		}
	}

	public Vector3 GetPosition () {
		return transform.position;
	}

}
