using UnityEngine;

public class Food : MonoBehaviour {

	public float speed = 25f;
	public FoodType type;

	private Transform target;

	private void Update() {
		if (target == null) {
			Destroy(gameObject);
			return;
		}
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
			customer.SatisfyRequest(this);
			Destroy(gameObject);
			return;
		}
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}

	private void Move() {
		transform.Translate(speed * Time.deltaTime * (target.position - transform.position).normalized, Space.World);
		transform.LookAt(target);
	}

}
