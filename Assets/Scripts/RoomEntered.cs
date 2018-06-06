using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntered : MonoBehaviour {

	public bool playerIsInRoom;

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			playerIsInRoom = true;
		}
	}
}
