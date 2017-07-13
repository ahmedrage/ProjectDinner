using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour {
	public bool light = false;
	public bool recentDead;
	public Color darkColor;
	public Color lightColor;
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (light || recentDead) {
			sprite.color = lightColor;
		} else {
			sprite.color = darkColor;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && recentDead == false) {
			light = true;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && recentDead == false) {
			light = false;
		}
	}
}
