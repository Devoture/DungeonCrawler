using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	public float attackRange;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	public bool canSeePlayer;
	public Transform player;

	public void FindVisibleTargets() {
		Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position, attackRange, targetMask);
		for(int i = 0; i < TargetsInViewRadius.Length; i++) {
			Transform target = TargetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2) {
				Debug.Log("Inside angle");
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
}
