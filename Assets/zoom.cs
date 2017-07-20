using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.orthographicSize = Mathf.Lerp (5.9f, 1.6f, Time.time/50);
	}
}
