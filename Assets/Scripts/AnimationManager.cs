using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	//public RuntimeAnimatorController Char1Anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: Make player 1 randomly spill his drink. 
	}

	public void AttackAnimationEnded () {
		Debug.Log ("Attack should Stop");
		gameObject.GetComponent<Animator> ().SetBool ("Attacking", false);
	}
}
