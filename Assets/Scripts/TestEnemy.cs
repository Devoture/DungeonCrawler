using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemy : MonoBehaviour {

	public Transform player;
	public Transform bulletSpawn;
	public GameObject bulletPrefab;
	public float attackRange = 10.0f;
	public float rateOfFire = .5f;
	public float timeBetweenShots = .25f;
	public float bulletSpeed = 5.0f;
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public List<Transform> visibleTargets = new List<Transform>();
	public bool canSeePlayer;

	private NavMeshAgent agent;
	private float distance;
	private int numOfShots = 0;
	private bool canShoot = true;
	private bool canBurst = true;
	private bool startedBurst;

	void Start() {
		agent = this.transform.GetComponent<NavMeshAgent>();
	}

	void Update() {
		if(player != null) {
			FindVisibleTargets();
			distance = Vector3.Distance(transform.position, player.position);
			if(distance > attackRange || !canSeePlayer) {
				agent.isStopped = false;
				agent.SetDestination(player.position);
			} 
			if(distance < attackRange && canSeePlayer || startedBurst) {
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
			transform.LookAt(player);
		}
	}

	void Attack() {
		numOfShots++;
		GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
		bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
		Destroy(bullet.gameObject, 2.0f);
	}

	void FindVisibleTargets() {
		Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		for(int i = 0; i < TargetsInViewRadius.Length; i++) {
			Transform target = TargetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask)) {
					canSeePlayer = true;
				} else {
					canSeePlayer = false;
				}
			}
		}
	}

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if(!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
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
