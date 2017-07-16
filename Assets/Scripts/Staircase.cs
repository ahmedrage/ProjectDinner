using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour {
	public GameObject Destination;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			GameObject player = other.gameObject;
			player.transform.position = Destination.transform.position;
		}
	}
}
