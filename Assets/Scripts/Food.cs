using NUnit.Framework.Interfaces;
using UnityEngine;

public class Food : MonoBehaviour {

	public float speed = 25f;
	public FoodType type;

	private Transform target;
	private Customer targetCustomer;

	private void Start() {
		if (target != null) {
			targetCustomer = target.GetComponent<Customer>();
		}
	}

	private void Update() {
		if (target == null || targetCustomer == null) {
			Destroy(gameObject);
			return;
		} else if (targetCustomer.RequestsSatisfied) {
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
			QuestManager.Instance.TryUpdateServeFoodQuestProgress(type);
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
