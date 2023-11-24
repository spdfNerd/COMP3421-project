using UnityEngine;

public class Player : MonoBehaviour {

	// Singleton instance
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

	// Map boundaries
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
		// Ensure unique singleton instance in scene
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

		CheckForNode();
		if (shouldCollect) {
			UpdateInventoryFromKitchen();
		}
		if (shouldGiveFood) {
			TransferFood();
		}
	}

	public Transform GetCurrentTowerTransform() {
		return currentNode.tower;
	}

	/// <summary>
	/// Change Direction enum to a vector
	/// </summary>
	/// <returns>Normalised vector representing the current movement direction</returns>
	public Vector3 GetDirectionAsVector() {
		Vector3 _ = direction switch {
			Direction.NORTH => Vector3.forward,
			Direction.NORTHEAST => Vector3.forward + Vector3.right,
			Direction.EAST => Vector3.right,
			Direction.SOUTHEAST => Vector3.back + Vector3.right,
			Direction.SOUTH => Vector3.back,
			Direction.SOUTHWEST => Vector3.back + Vector3.left,
			Direction.WEST => Vector3.left,
			Direction.NORTHWEST => Vector3.forward + Vector3.left,
			_ => Vector3.zero,
		};
		return _.normalized;
	}

	/// <summary>
	/// Set player movement constraints so they move inside the map only
	/// </summary>
	private void SetConstraints() {
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		float width = collider.radius;
		minX = LevelManager.Instance.leftWall.position.x + width;
		maxX = LevelManager.Instance.rightWall.position.x - width;
		minZ = LevelManager.Instance.frontWall.position.z + width;
		maxZ = LevelManager.Instance.backWall.position.z - width;
	}

	/// <summary>
	/// Get movement direction vector
	/// </summary>
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

	/// <summary>
	/// Rotate the model of the player to match the movement direction
	/// </summary>
	private void RotateModel() {
		if (GetDirectionAsVector() == Vector3.zero) {
			return;
		}

		Quaternion lookRotation = Quaternion.LookRotation(GetDirectionAsVector());
		// Using Quaternion.Lerp to smooth out the rotation
		Vector3 rotation = Quaternion.Lerp(gfx.rotation, lookRotation, rotateSpeed).eulerAngles;
		gfx.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}

	private void Move() {
		transform.Translate(movementSpeed * Time.deltaTime * GetDirectionAsVector());
		float xPos = Mathf.Clamp(transform.position.x, minX, maxX);
		float zPos = Mathf.Clamp(transform.position.z, minZ, maxZ);
		transform.position = new Vector3(xPos, transform.position.y, zPos);
	}

	/// <summary>
	/// Try to find the node directly below the player using raycasting
	/// </summary>
	private void CheckForNode() {
		CapsuleCollider collider = GetComponent<CapsuleCollider>();
		float rayLength = collider.height / 2f + 0.3f;

		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, rayLength)) {
			previousNode = currentNode;
			hit.transform.TryGetComponent(out currentNode);
			UpdateCurrentNode();

		}
	}

	private void UpdateInventoryFromKitchen() {
		CollectionNode.Instance.TransferKitchenInventory(inventory);
		inventoryScreen.UpdateTexts(inventory);
	}

	/// <summary>
	/// Transfers selected food from player to waiter
	/// </summary>
	private void TransferFood() {
		FoodType foodType = (FoodType) inventoryScreen.SelectedItem;
		if (currentWaiter.FoodCount > 0 && currentWaiter.FoodType != foodType) {
			return;
		}

		int count = inventory.GetItemCount(foodType);
		if (count > 0) {
			currentWaiter.UpdateFoodType(foodType, count);
			inventory.ClearItem(foodType);
			inventoryScreen.SetCountToZero(foodType);
		}
	}

	/// <summary>
	/// Update which node the player is currently on and adjust fields accordingly
	/// </summary>
	private void UpdateCurrentNode() {
		if (previousNode == currentNode) {
			return;
		}

		if (previousNode != null) {
			previousNode.OnPlayerExit();
		}

		if (currentNode != null) {
			currentNode.OnPlayerEnter();
			shouldCollect = currentNode.GetComponent<CollectionNode>() != null;
			if (GetCurrentTowerTransform() != null) {
				currentWaiter = GetCurrentTowerTransform().GetComponent<Waiter>();
				shouldGiveFood = currentWaiter != null;
			} else {
				currentWaiter = null;
				shouldGiveFood = false;
			}

			UpdateShopButtons();
		} else {
			shouldCollect = false;
			currentWaiter = null;
			shouldGiveFood = false;
		}
	}

	/// <summary>
	/// Tell shop to enable and disable buttons based on player position
	/// </summary>
	private void UpdateShopButtons() {
		Shop.Instance.UpdateButtons(currentNode, currentNode.CompareTag(Shop.KitchenNodeTag));
		if (previousNode != null && !currentNode.CompareTag(previousNode.tag)) {
			Shop.Instance.ClearSelectedTower();
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
