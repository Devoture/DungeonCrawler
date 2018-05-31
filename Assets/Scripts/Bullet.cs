using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float damage = 20.0f;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy") {
			other.GetComponent<Health>().TakeDamage(damage);
			Destroy(this.gameObject);
		}
	}
}
