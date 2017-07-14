using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour {
	public bool light = false;
	public bool recentDead;
	public Color darkColor;
	public Color lightColor;
	public AudioClip doorSound;
	SpriteRenderer sprite;
	audioManager audio;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		audio = GameObject.Find("audioManager").GetComponent<audioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (light || recentDead) {
			sprite.color = lightColor;
		} else {
			sprite.color = darkColor;
		}
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && recentDead == false) {
			light = true;
			audio.playSound (doorSound, transform.position);
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.gameObject.tag == "Player" && recentDead == false) {
			light = false;
		}
	}
}
