using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float speed = 5.0f;

    private Rigidbody rb;
	private Vector3 mousePos;
	
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	void Update () {
     	mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        transform.LookAt (new Vector3 (mousePos.x, transform.position.y, mousePos.z));
     }
   
    void FixedUpdate () {
        rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * speed, 0.8f), 0, Mathf.Lerp(0, Input.GetAxis("Vertical") * speed, 0.8f));
    }
}
