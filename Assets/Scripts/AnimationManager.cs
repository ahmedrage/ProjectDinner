using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

	//public RuntimeAnimatorController Char1Anim;
	public List<GameObject> guests;

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

	public void StopAllNPC () {
		foreach (GameObject g in guests) {
			if (!g.GetComponent<Guest> ().dead) {
				g.transform.Find ("Graphics").GetComponent<Animator> ().enabled = false;
			}
		}
		GetComponent<murderSystem> ().murderer.transform.Find ("Graphics").GetComponent<Animator> ().enabled = false;
	}

	public void PlayAllNPC () {
		foreach (GameObject g in guests) {
			if (!g.GetComponent<Guest> ().dead) {
				g.transform.Find ("Graphics").GetComponent<Animator> ().enabled = true;
			}
		}
		GetComponent<murderSystem> ().murderer.transform.Find ("Graphics").GetComponent<Animator> ().enabled = true;
	}
}
