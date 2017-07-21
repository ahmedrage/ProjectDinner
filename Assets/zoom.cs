using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour {
	float initialSize;
	// Use this for initialization
	void Start () {
		initialSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame 3.5
	void Update () {
		
		Camera.main.orthographicSize = Mathf.Lerp (initialSize, initialSize/3.5f, Time.time/50);
	}
}
