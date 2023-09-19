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
            transform.Translate(Vector2.up * movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.S)) {
            transform.Translate(Vector2.down * movementSpeed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        }
    }

}
