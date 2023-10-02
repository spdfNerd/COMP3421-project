using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 5f;

    private void Start() {
    
    }

    private void Update() {
        Move();
    }

    private void Move() {
        if (Input.GetKey(KeyCode.W)) {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector3.back* movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
    }

}
