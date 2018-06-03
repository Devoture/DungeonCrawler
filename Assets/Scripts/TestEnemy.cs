using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour {

	public Transform target;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float attackRange = 10.0f;
	public float rateOfFire = .25f;

	private NavMeshAgent agent;
	private float distance;
	public bool canShoot = true;

	void Start() {
		agent = this.transform.GetComponent<NavMeshAgent>();
	}

	void Update() {
		if(target != null) {
			distance = Vector3.Distance(transform.position, target.position);
			if(distance > attackRange) {
				agent.isStopped = false;
				agent.SetDestination(target.position);
			} else {
				agent.isStopped = true;
				if(canShoot) {
					Attack();
					StartCoroutine(RateOfFire(rateOfFire));
				}
			}
			transform.LookAt(target);
		}
	}

	void Attack() {
		canShoot = false;
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = transform.forward;
		Destroy(bullet.gameObject, 2.0f);
	}

	private IEnumerator RateOfFire(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		canShoot = true;
    }
}
