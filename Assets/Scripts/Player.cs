using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public Rigidbody m_bulletPrefab;
	public Transform m_bulletSpawn;
	public float m_bulletSpeed = 20.0f;

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			Fire();
		}
	}

	void Fire() {
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0.0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection-transform.position;
		Rigidbody bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody;
		bullet.velocity = new Vector2(shootDirection.x * m_bulletSpeed, shootDirection.y * m_bulletSpeed);
		Destroy(bullet.gameObject, 2.0f);
	}
}
