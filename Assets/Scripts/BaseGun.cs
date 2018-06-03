using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGun : MonoBehaviour {
	public Rigidbody bulletPrefab;
	public Transform bulletSpawn;
	public float bulletSpeed = 20.0f;

	private bool allowFire = true;
	private float fireRate = 0.5f;
	private float lastShot = 0.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire() {
		allowFire = false;
		Vector3 shootDirection;
		shootDirection = Input.mousePosition;
		shootDirection.z = 0.0f;
		shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
		shootDirection = shootDirection - transform.position;
		 if (Time.time > fireRate + lastShot) {
			Rigidbody bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody;
			bullet.velocity = new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
			lastShot = Time.time;
			Destroy(bullet.gameObject, 2.0f);
		 }
	}
}
