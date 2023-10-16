using UnityEngine;

public class Food : MonoBehaviour {

    public float speed = 25f;

    private void Start() {
        
    }

    private void Update() {
        Move();
    }

    private void OnTriggerEnter(Collider collision) {
        if (collision == null) {
            return;
        }

        Collider collider = collision.GetComponent<Collider>();
        if (collider == null) {
            return;
        }

        Customer customer = collider.GetComponent<Customer>();
        if (customer != null) {
            customer.Die(false);
            Destroy(gameObject);
            return;
        }
    }

    private void Move() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

}
