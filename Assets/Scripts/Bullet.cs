using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float m_damage = 20.0f;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Enemy") {
			other.GetComponent<Health>().TakeDamage(m_damage);
			Destroy(this.gameObject);
		}
	}
}
