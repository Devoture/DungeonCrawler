using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public float damage = 20.0f;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<Health>().TakeDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
