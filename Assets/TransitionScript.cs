using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x < -45) {
			GameObject.Find ("SceneSwitcher").GetComponent<CinematicSceneSwitch> ().Transition ();
		}
	}
}
