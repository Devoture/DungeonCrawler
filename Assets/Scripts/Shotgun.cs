using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseGun {

	public int bulletCount;
	public float spreadAngle;
	List<Quaternion> bullets;
	// Use this for initialization
	void Awake() {
		bullets = new List<Quaternion>(bulletCount);
		for(int i = 0; i < bulletCount; i++) {
			bullets.Add(Quaternion.Euler(Vector3.zero));
		}
	}
	
	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Fire1")) {
			Fire();
		}
	}

	public override void Fire() {
		int i = 0;
		foreach(Quaternion quat in bullets) {
			bullets[i] = Random.rotation;
			Rigidbody b = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as Rigidbody;
			b.name = "Bullet";
			b.GetComponent<Bullet>().damage = 2.0f;
			b.transform.rotation = Quaternion.RotateTowards(b.transform.rotation, bullets[i], spreadAngle);
			b.GetComponent<Rigidbody>().AddForce(b.transform.forward * bulletSpeed);
			Destroy(b.gameObject, 2.0f);

		}
	}
}
