using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public BaseGun baseGun;

	void Update() {
		if(Input.GetMouseButtonDown(0)) {
			baseGun.Fire();
		}
	}
}
