using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statScript : MonoBehaviour {
	public GuestClass lastMurderer;
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
