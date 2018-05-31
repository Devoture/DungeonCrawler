using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float m_speed = 5.0f;

    private Rigidbody m_rb;
	
	void Start() {
		m_rb = GetComponent<Rigidbody>();
	}

	void Update () {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f;

		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;

		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
     }
   
    void FixedUpdate () {
        m_rb.velocity = new Vector3(Mathf.Lerp(0, Input.GetAxis("Horizontal") * m_speed, 0.8f),Mathf.Lerp(0, Input.GetAxis("Vertical") * m_speed, 0.8f), 0);
    }
}
