using UnityEngine;

public class Player : MonoBehaviour {

	public float movementSpeed = 40f;

	private bool[] directions = new bool[] { false, false, false, false };
	//private Rigidbody rb;

	private void Start() {
		//rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		directions[0] = Input.GetKey(KeyCode.W);
		directions[1] = Input.GetKey(KeyCode.A);
		directions[2] = Input.GetKey(KeyCode.S);
		directions[3] = Input.GetKey(KeyCode.D);

		Move();
	}

	private void Move() {
		transform.Translate(Vector3.forward * (ToInt(directions[0]) - ToInt(directions[2])) * movementSpeed * Time.deltaTime);
		transform.Translate(Vector3.right * (ToInt(directions[3]) - ToInt(directions[1])) * movementSpeed * Time.deltaTime);
		//Vector3 zDir = Vector3.forward * (Convert.ToInt32(directions[0]) - Convert.ToInt32(directions[2]));
		//Vector3 xDir = Vector3.right * (Convert.ToInt32(directions[3]) - Convert.ToInt32(directions[1]));

		//Vector3 direction = (xDir + zDir) * movementSpeed * Time.deltaTime;
		//Vector3 newPosition = rb.position + direction;
		//rb.MovePosition(newPosition);
	}

	private int ToInt(bool value) {
		return value ? 1 : 0;
	}

}
