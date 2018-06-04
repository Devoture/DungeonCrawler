using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmallSlimeAI : MonoBehaviour {

	public GameObject player;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float rateOfFire = .5f;
	public float bulletSpeed = 5.0f;
	public RoomEntered roomEntered;

	private NavMeshAgent agent;
	private FieldOfView fov;
	private float distance;
	private bool canShoot = true;

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
			if(distance < fov.attackRange && fov.canSeePlayer) {
				agent.isStopped = true;
				if(canShoot) {
					Attack();
					canShoot = false;
					StartCoroutine(RateOfFire(rateOfFire));
				}
			}
			transform.LookAt(player.transform);
		}
	}

	void Attack() {
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		Destroy(bullet.gameObject, 2.0f);
	}

	private IEnumerator RateOfFire(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		canShoot = true;
    }
}
