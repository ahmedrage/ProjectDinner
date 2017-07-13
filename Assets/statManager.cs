using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManager : MonoBehaviour {

	static statManager Instance;
	public Sprite[] deadGuestsPorts;
	public List<Sprite> murderers;
	public List<string> murdererNames;
	public List<string> levelNames;
	public float[] breakTimes;
	public int failedArrests;
	public int savedGuests;
	public int totalGuests;
	public float savedGuestPercentage;
	public string frequentMethod;
	public float breakTime;

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
		savedGuestPercentage = savedGuests / totalGuests;
		breakTime = Mathf.Min(breakTime);
	}
}
