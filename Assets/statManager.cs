using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManager : MonoBehaviour {

	static statManager Instance;
	public Sprite[] deadGuestsPorts;
	public List<Sprite> murderers;
	public int failedArrests;

	// Use this for initialization
  	void Start () {
		if (Instance != null) {
			GameObject.Destroy (gameObject);
		} else {
			GameObject.DontDestroyOnLoad (gameObject);
			Instance = this;
		}		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
