using UnityEngine;

public class Food : MonoBehaviour {

	public float speed = 25f;
	public FoodType type;
	public int reward;

	private Transform target;
	private Customer targetCustomer;
	// Internal flag to ensure quest is not updated multiple times from one food
	private bool questUpdated;

	private void Start() {
		if (target != null) {
			targetCustomer = target.GetComponent<Customer>();
		}
		questUpdated = false;
	}

	private void Update() {
		if (target == null || targetCustomer == null) {
			// Destroy projectile if no target or target is not customer
			Destroy(gameObject);
			return;
		} else if (targetCustomer.RequestsSatisfied) {
			// Destroy projectile if customer requests are satisfied
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
			// Satisfy the customer requests, then destroy projectile
			customer.SatisfyRequest(this);
			UpdateQuest();
			Destroy(gameObject);
			return;
		}
	}

	public void SetTarget(Transform target) {
		this.target = target;
	}

	private void UpdateQuest() {
		if (!questUpdated) {
			QuestManager.Instance.TryUpdateServeFoodQuestProgress(type);
			questUpdated = true;
		}
	}

	private void Move() {
		Vector3 direction = target.position - transform.position;
		direction.Normalize();
		transform.Translate(speed * Time.deltaTime * direction, Space.World);
		transform.LookAt(target);
	}

}
