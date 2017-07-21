using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour {
	public GameObject Destination;
	public GameObject Camera;
	public Vector3 CameraFocus;
	public bool isDown;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player")) {
			int adjust = 1;
			if (isDown) {
				adjust = -1;
			}
			GameObject player = other.gameObject;
			player.transform.position = new Vector3 (Destination.transform.position.x, Destination.transform.position.y + adjust, 0); //Destination.transform.position;
			Camera.transform.position = CameraFocus;
			Camera.gameObject.GetComponent<cameraShake> ().initialPosition = CameraFocus;
		}
	}
}
