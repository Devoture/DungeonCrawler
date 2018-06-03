using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour {

	public Transform target;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float attackRange = 10.0f;
	public float rateOfFire = .5f;
	public float timeBetweenShots = .25f;
	public float bulletSpeed = 5.0f;

	private NavMeshAgent agent;
	private float distance;
	public int numOfShots = 0;
	public bool canShoot = true;
	public bool canBurst = true;

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
				if(canBurst && canShoot) {
					Attack();
					canShoot = false;
					StartCoroutine(BurstTimer(timeBetweenShots));
				}
				if(numOfShots == 3) {
					canBurst = false;
					numOfShots = 0;
					StartCoroutine(RateOfFire(rateOfFire));
				}
			}
			transform.LookAt(target);
		}
	}

	void Attack() {
		numOfShots++;
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		Destroy(bullet.gameObject, 2.0f);
	}

	private IEnumerator BurstTimer(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		canShoot = true;
    }

	private IEnumerator RateOfFire(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		canBurst = true;
    }
}
