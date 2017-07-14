using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trackMover : MonoBehaviour {

	public float speed;
	Transform switchPoint;

	// Use this for initialization
	void Start () {
		switchPoint = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.down * speed;

		if (switchPoint.position.y < Camera.main.ScreenToWorldPoint (new Vector3(0,0,0)).y) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + (GetComponent<SpriteRenderer>().bounds.size.y) * 2 );
		}
	}
}
