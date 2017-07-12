using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(playerController))]
public class fieldOfViewEditor : Editor {

	void OnSceneGUI()
	{
		playerController gui = (playerController)target;
		Handles.color = Color.white;
		Handles.DrawWireArc (gui.transform.position, gui.transform.forward,Vector2.up, 360, gui.radius); 
	}
}
