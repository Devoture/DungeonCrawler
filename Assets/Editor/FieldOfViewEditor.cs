using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TestEnemy))]
public class FieldOfViewEditor : Editor {

	void OnSceneGUI() {
		TestEnemy test = (TestEnemy)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(test.transform.position, Vector3.up, Vector3.forward, 360, test.viewRadius);
		Vector3 viewAngleA = test.DirFromAngle(-test.viewAngle / 2, false);
		Vector3 viewAngleB = test.DirFromAngle(test.viewAngle / 2, false);

		Handles.DrawLine(test.transform.position, test.transform.position + viewAngleA * test.viewRadius);
		Handles.DrawLine(test.transform.position, test.transform.position + viewAngleB * test.viewRadius);

		Handles.color = Color.red;
		if(test.canSeePlayer) {
			Handles.DrawLine(test.transform.position, test.player.position);
		}
	}
}
