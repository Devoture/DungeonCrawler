using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : MonoBehaviour {

	public GameObject player;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float rateOfFire = .5f;
	public float timeBetweenShots = .25f;
	public float bulletSpeed = 5.0f;
	public RoomEntered roomEntered;

	private NavMeshAgent agent;
	private FieldOfView fov;
	private float distance;
	private int numOfShots = 0;
	private bool canShoot = true;
	private bool canBurst = true;
	private bool startedBurst;

	void Start() {
		agent = this.transform.GetComponent<NavMeshAgent>();
		fov = GetComponent<FieldOfView>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if(player != null ) { // && roomEntered.playerIsInRoom
			fov.FindVisibleTargets();
			distance = Vector3.Distance(transform.position, player.transform.position);
			if(distance > fov.attackRange || !fov.canSeePlayer) {
				agent.isStopped = false;
				agent.SetDestination(player.transform.position);
			} 
			if((distance < fov.attackRange && fov.canSeePlayer) || startedBurst) {
				agent.isStopped = true;
				if(canBurst && canShoot) {
					startedBurst = true;
					Attack();
					canShoot = false;
					StartCoroutine(BurstTimer(timeBetweenShots));
				}
				if(numOfShots == 3) {
					canBurst = false;
					startedBurst = false;
					numOfShots = 0;
					StartCoroutine(RateOfFire(rateOfFire));
				}
			}
			transform.LookAt(player.transform);
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
