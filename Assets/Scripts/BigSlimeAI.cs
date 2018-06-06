using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigSlimeAI : MonoBehaviour {

	public GameObject player;
	public Transform bulletSpawnRight;
	public Transform bulletSpawnMiddle;
	public Transform bulletSpawnLeft;
	public Transform smallSlimeSpawnRight;
	public Transform smallSlimeSpawnMiddle;
	public Transform smallSlimeSpawnLeft;
	public GameObject bulletPrefab;
	public GameObject smallSlimePrefab;
	public float rateOfFire = .5f;
	public float bulletSpeed = 5.0f;
	public RoomEntered roomEntered;

	private NavMeshAgent agent;
	private FieldOfView fov;
	private float distance;
	private bool canShoot = true;
	private bool isQuitting;

	void Start() {
		agent = this.transform.GetComponent<NavMeshAgent>();
		fov = GetComponent<FieldOfView>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() {
		if(player != null) { // && roomEntered.playerIsInRoom
			fov.FindVisibleTargets();
			distance = Vector3.Distance(transform.position, player.transform.position);
			if(distance < fov.attackRange && fov.canSeePlayer) {
				if(canShoot) {
					canShoot = false;
					Attack();
					StartCoroutine(RateOfFire(rateOfFire));
				}
			}
			transform.LookAt(player.transform);
		}
	}

	void Attack() {
		GameObject bulletRight = Instantiate(bulletPrefab, bulletSpawnRight.position, Quaternion.identity);
		bulletRight.GetComponent<Rigidbody>().velocity = bulletSpawnRight.transform.forward * bulletSpeed;

		GameObject bulletMiddle = Instantiate(bulletPrefab, bulletSpawnMiddle.position, Quaternion.identity);
		bulletMiddle.GetComponent<Rigidbody>().velocity = bulletSpawnMiddle.transform.forward * bulletSpeed;

		GameObject bulletLeft = Instantiate(bulletPrefab, bulletSpawnLeft.position, Quaternion.identity);
		bulletLeft.GetComponent<Rigidbody>().velocity = bulletSpawnLeft.transform.forward * bulletSpeed;

		Destroy(bulletRight.gameObject, 2.0f);
		Destroy(bulletMiddle.gameObject, 2.0f);
		Destroy(bulletLeft.gameObject, 2.0f);
	}

	private IEnumerator RateOfFire(float waitTime) {
		yield return new WaitForSeconds(waitTime);
		canShoot = true;
    }

	void OnApplicationQuit() {
		isQuitting = true;
	}

	void OnDestroy() {
		if(!isQuitting) {
			Instantiate(smallSlimePrefab, smallSlimeSpawnRight.position, Quaternion.identity);
			Instantiate(smallSlimePrefab, smallSlimeSpawnMiddle.position, Quaternion.identity);
			Instantiate(smallSlimePrefab, smallSlimeSpawnLeft.position, Quaternion.identity);
		}
	}
}
