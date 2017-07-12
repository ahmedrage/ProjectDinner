using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour {

	public Sprite[] sprites;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [1];
		}
	}
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [0];
	}
}
