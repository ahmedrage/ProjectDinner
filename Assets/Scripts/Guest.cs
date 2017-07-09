﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	public GuestClass[] GuestArray;
	public bool dead;
	public Transform Panel;
	public Transform Portrait;

	public murderSystem murderScript;

	playerController playerScript;
	Transform deathPanel;
	void Start () {
		playerScript = GameObject.FindWithTag ("Player").GetComponent<playerController> ();
		deathPanel = transform.GetChild (0).transform.GetChild (1);
		Portrait = transform.Find ("Canvas").Find ("Portrait");
		murderScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		DisplayText ();
		if (dead) {
			this.gameObject.layer = LayerMask.NameToLayer("deadGuest");
			Physics2D.IgnoreLayerCollision(8,9,true);
		}
	}

	public void setText () {
		Panel = transform.GetChild (0).transform.GetChild (0);
		Panel.transform.GetChild (0).GetComponent<Text> ().text = guestClass.name;
		Panel.transform.GetChild (1).GetComponent<Text> ().text = guestClass.profession.ToString();
		if (guestClass.Accessory.ToString () == "NoAccesories") {
			Panel.transform.GetChild (2).GetComponent<Text> ().text = "Is wearing no Accessories";
		}else
		Panel.transform.GetChild (2).GetComponent<Text> ().text = "Is wearing a " + guestClass.Accessory.ToString();
		
		Panel.transform.GetChild (3).GetComponent<Text> ().text = "Has a " +guestClass.Blemish.ToString();

	}

	public void setDeathText (string deathText) {
		deathPanel.GetChild (0).GetComponent<Text> ().text = deathText;
	}

	public void setHintText (string hintText) {
		print (guestClass.name);
		Panel.transform.GetChild (4).GetComponent<Text> ().text = hintText;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && playerScript.nearbyGuests.Contains(transform) == false) {
			playerScript.addGuest (transform);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && Panel != null && deathPanel != null) {
			playerScript.removeGuest (transform);
		}
	}

	void DisplayText() {
		if (playerScript.nearestGuest == gameObject) {
			if (dead == false && Panel != null) {
				Panel.gameObject.SetActive (true);
			} else if (dead == true && deathPanel != null) {
				print (dead.ToString ());
				deathPanel.gameObject.SetActive (true);
			}

			if (Portrait != null) {
				Portrait.gameObject.SetActive (true);
			}
		} else {
			if (Panel != null && deathPanel != null) {
				Panel.gameObject.SetActive (false);
				deathPanel.gameObject.SetActive (false);
			}
			if (Portrait != null) {
				Portrait.gameObject.SetActive (false);
			}
		}

	}
	public void Die() {
		dead = true;
		transform.Find ("Graphics").GetComponent<Animator> ().enabled = false;
		transform.Find ("Graphics").GetComponent<SpriteRenderer> ().sprite = guestClass.deadSprite;
	}
}
