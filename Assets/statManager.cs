using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statManager : MonoBehaviour {

	static statManager Instance;
	public Sprite[] deadGuestsPorts;
	public GuestClass lastMurderer;
	public List<Sprite> murderers;
	public List<string> murdererNames;
	public List<string> levelNames;
	public float[] breakTimes;
	public string[] method;
	public int[] methodFreq;
	public int failedArrests;
	public int savedGuests;
	public int totalGuests;
	public int freq;
	public double savedGuestPercentage;
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
		savedGuestPercentage = Mathf.Round(((float)savedGuests / totalGuests)*100);
		breakTime = Mathf.Min(breakTime);
		//print (savedGuestPercentage);

		string element = "";
		int count = 0;
		for (int i = 0; i < method.Length; i++) {
			string tempElement = method [i];
			int tempCount = 0;
			for (int x = 0; x < method.Length; x++) {
				if (method [x] == tempElement) {
					tempCount++;
				}
				if (tempCount > count) {
					element = tempElement;
					count = tempCount;
				}
			}	

		}

		frequentMethod = element;
	}
}
