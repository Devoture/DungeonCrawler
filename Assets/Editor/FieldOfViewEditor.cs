using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfView))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
		FieldOfView fov = (FieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.attackRange);
		Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
		Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.attackRange);
		Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.attackRange);

		Handles.color = Color.red;
		if(fov.canSeePlayer) {
			Handles.DrawLine(fov.transform.position, fov.player.transform.position);
		}
	}
}
