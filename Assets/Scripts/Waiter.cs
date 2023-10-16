using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Waiter : MonoBehaviour {

    public Transform projectile;
    public Transform firePoint;

    public float range = 4f;
    public float fireCooldown = 2f;

    private Transform target = null;
    private float timer = 0f;

    private void Start() {

    }

    private void Update() {
        FindTarget();
        if (target != null) {
            Shoot();
        }
    }

    private void FindTarget() {
        if (target != null) {
            return;
        }

        float targetDistance = 0f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        if (colliders.Length == 0) {
            return;
        }

        colliders = colliders.Where(collider => collider.GetComponent<Customer>() != null).ToArray();
        foreach (Collider collider in colliders) {
            if (target == null) {
                target = collider.transform;
                targetDistance = Vector3.Distance(transform.position, target.position);
            } else {
                if (Vector3.Distance(transform.position, collider.transform.position) < targetDistance) {
                    target = collider.transform;
                }
            }
        }
    }

    private void Shoot() {
        Vector3 direction = target.position - firePoint.position;

        if (FloatComparer.AreEqual(0f, timer, 0.1f)) {
            Instantiate(projectile, firePoint.position, Quaternion.LookRotation(direction));
            timer = fireCooldown;
        }

        timer -= Time.deltaTime;
    }

}
