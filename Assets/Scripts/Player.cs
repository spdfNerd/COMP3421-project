using UnityEngine;

public class Player : MonoBehaviour {

	public static Player Instance;

	[Header("Movement Settings")]
	public float movementSpeed = 40f;
	public float rotateSpeed = 0.1f;

	[Header("Graphics")]
	public Transform gfx;
	public InventoryScreen inventoryScreen;

	[HideInInspector]
	public Node currentNode;
	[HideInInspector]
	public Node previousNode;

	private float minX;
	private float maxX;
	private float minZ;
	private float maxZ;

	private Direction direction;

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
		SetConstraints();
		direction = Direction.NONE;

		inventory = new Inventory();
		shouldCollect = false;
		shouldGiveFood = false;
	}

	private void Update() {
		GetDirection();
		RotateModel();
		Move();

		UpdateCurrentNode();
		if (shouldCollect) {
			UpdateInventory();
		}
		if (shouldGiveFood) {
			TransferFood();
		}
	}

	private void SetConstraints() {
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		float width = collider.radius;
		minX = LevelManager.Instance.leftWall.position.x + width;
		maxX = LevelManager.Instance.rightWall.position.x - width;
		minZ = LevelManager.Instance.frontWall.position.z + width;
		maxZ = LevelManager.Instance.backWall.position.z - width;
	}

	private void GetDirection() {
		if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
			if (Input.GetKey(KeyCode.A)) {
				direction = Direction.NORTHWEST;
			} else if (Input.GetKey(KeyCode.D)) {
				direction = Direction.NORTHEAST;
			} else {
				direction = Direction.NORTH;
			}
		} else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) {
			if (Input.GetKey(KeyCode.A)) {
				direction = Direction.SOUTHWEST;
			} else if (Input.GetKey(KeyCode.D)) {
				direction = Direction.SOUTHEAST;
			} else {
				direction = Direction.SOUTH;
			}
		} else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
			direction = Direction.WEST;
		} else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
			direction = Direction.EAST;
		} else {
			direction = Direction.NONE;
		}
	}

	private void RotateModel() {
		if (GetDirectionAsVector() == Vector3.zero) {
			return;
		}

		Quaternion lookRotation = Quaternion.LookRotation(GetDirectionAsVector());
		Vector3 rotation = Quaternion.Lerp(gfx.rotation, lookRotation, rotateSpeed).eulerAngles;
		gfx.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void Move() {
		transform.Translate(movementSpeed * Time.deltaTime * GetDirectionAsVector());
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

	public Vector3 GetDirectionAsVector() {
		Vector3 dirVec = Vector3.zero;
		switch (direction) {
			case Direction.NORTH:
				dirVec = Vector3.forward;
				break;
			case Direction.NORTHEAST:
				dirVec = Vector3.forward + Vector3.right;
				break;
			case Direction.EAST:
				dirVec = Vector3.right;
				break;
			case Direction.SOUTHEAST:
				dirVec = Vector3.back + Vector3.right;
				break;
			case Direction.SOUTH:
				dirVec = Vector3.back;
				break;
			case Direction.SOUTHWEST:
				dirVec = Vector3.back + Vector3.left;
				break;
			case Direction.WEST:
				dirVec = Vector3.left;
				break;
			case Direction.NORTHWEST:
				dirVec = Vector3.forward + Vector3.left;
				break;
			case Direction.NONE:
			default:
				dirVec = Vector3.zero;
				break;

		}
		return dirVec.normalized;
	}

	private void UpdateInventory() {
		CollectionNode.Instance.TransferInventory(inventory);
		inventoryScreen.UpdateTexts(inventory);
	}

	private void TransferFood() {
		if (currentWaiter.FoodCount > 0) {
			return;
		}

		FoodType type = (FoodType) inventoryScreen.SelectedItem;
		int count = inventory.GetItemCount(type);
		if (count > 0) {
			currentWaiter.UpdateFoodType(type, count);
			inventory.ClearItem(type);
		}
	}

}

public enum Direction {
	
	NONE,
	NORTH,
	NORTHEAST,
	EAST,
	SOUTHEAST,
	SOUTH,
	SOUTHWEST,
	WEST,
	NORTHWEST

}
