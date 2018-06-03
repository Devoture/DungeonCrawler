using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour {
	public Rigidbody bulletPrefab;
	public Transform bulletSpawn;
	public float bulletSpeed = 20.0f;

	private float fireRate = 0.5f;
	private float lastShot = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire() {
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.y = 0.0f;
		shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		shootDirection = shootDirection - transform.position;
		 if (Time.time > fireRate + lastShot) {
			Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody;
			bullet.velocity = new Vector3(shootDirection.x * bulletSpeed, 0, shootDirection.z * bulletSpeed);
			lastShot = Time.time;
			bullet.name = "Bullet";
			Destroy(bullet.gameObject, 2.0f);
		 }
	}
}
