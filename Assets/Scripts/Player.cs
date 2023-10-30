using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player Instance;
	[HideInInspector]
	public Node currentNode;
	[HideInInspector]
	public Node previousNode;
	public float movementSpeed = 40f;
	public float rotateSpeed = 0.1f;

	public Transform gfx;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;

	private bool[] directions = new bool[] { false, false, false, false };

	private Inventory inventory;
	private Waiter currentWaiter;
	private bool shouldCollect;
	private bool shouldGiveFood;

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

		inventory = new Inventory();
		shouldCollect = false;
		shouldGiveFood = false;
	}

	private void Update() {
		Vector3 direction = GetDirection();
		RotateModel(direction);
		Move(direction);

		UpdateCurrentNode();
		if (shouldCollect) {
			UpdateInventory();
		}
		if (shouldGiveFood) {
			TransferFood();
		}
	}

	private Vector3 GetDirection() {
		directions[0] = Input.GetKey(KeyCode.W);
		directions[1] = Input.GetKey(KeyCode.A);
		directions[2] = Input.GetKey(KeyCode.S);
		directions[3] = Input.GetKey(KeyCode.D);

		Vector3 zDir = Vector3.forward * (Convert.ToInt32(directions[0]) - Convert.ToInt32(directions[2]));
		Vector3 xDir = Vector3.right * (Convert.ToInt32(directions[3]) - Convert.ToInt32(directions[1]));
		Vector3 direction = xDir + zDir;

		return direction.normalized;
	}

	private void RotateModel(Vector3 direction) {
		if (direction == Vector3.zero) {
			return;
		}

		Quaternion lookRotation = Quaternion.LookRotation(direction);
		Vector3 rotation = Quaternion.Lerp(gfx.rotation, lookRotation, rotateSpeed).eulerAngles;
		gfx.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void Move(Vector3 direction) {
		transform.Translate(movementSpeed * Time.deltaTime * direction);
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

				shouldCollect = hit.transform.GetComponent<CollectionNode>() != null;
				if (node.tower != null) {
					currentWaiter = node.tower.GetComponent<Waiter>();
					shouldGiveFood = currentWaiter != null;
				}
			}
		} else {
			previousNode = currentNode;
			currentNode = null;
			shouldCollect = false;
			currentWaiter = null;
			shouldGiveFood = false;
		}

		if (previousNode != null) {
			previousNode.OnPlayerExit();
		}
	}

	public Vector3 GetPosition () {
		return transform.position;
	}

	private void UpdateInventory() {
		CollectionNode.Instance.TransferInventory(inventory);
	}

	private void TransferFood() {
		if (currentWaiter.FoodCount > 0) {
			return;
		}

		foreach (FoodType type in Enum.GetValues(typeof(FoodType))) {
			int count = inventory.GetItemCount(type);
			Debug.LogFormat("{0}: {1}", type, count);
			if (count > 0) {
				currentWaiter.UpdateFoodType(type, count);
				inventory.ClearItem(type);
			}
		}
	}

}
