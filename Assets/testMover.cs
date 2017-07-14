using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMover : MonoBehaviour {
	public float speed;
	public Transform point1;
	public Transform point2;
	bool t = false;
	Vector3 move;
	// Use this for initialization
	void Start () {
		move = Vector3.down * speed;

	}
	
	// Update is called once per frame
	void Update () {
		transform.position += move;
		if (transform.position.y >= point1.position.y || transform.position.y <= point2.position.y && t == false) {
			print ("Salmon");
			move = -move;
			t = true;
		} else if (transform.position.y <= point1.position.y && transform.position.y >= point2.position.y) {
			t = false;
		}
	}
}
