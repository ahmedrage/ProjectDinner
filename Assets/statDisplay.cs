using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class statDisplay : MonoBehaviour {
	public List<Image> murderPortraitImages;
	public List<Text> _murdererNames;
	public List<Image> _deadGuestImages;
	public Text failedArrests;
	public Text saveGuests;
	public Text freqMethod;
	public Text _breakTime;
	public statManager _statManager;



	// Use this for initialization
	void Start () {
		_statManager = GameObject.Find ("dataManager").GetComponent<statManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		for (int x = 0; x < _statManager.murdererNames.Count; x++) {
			_murdererNames [x].text = _statManager.murdererNames [x];
		}

		for (int i = 0; i < _statManager.murderers.Count; i++) {
			murderPortraitImages [i].sprite = _statManager.murderers [i];
		}

		for (int y = 0; y < _statManager.deadGuestsPorts.Length; y++) {
			_deadGuestImages [y].sprite = _statManager.deadGuestsPorts [y];
		}
			
		failedArrests.text = "Failed arrests: " + _statManager.failedArrests.ToString ();
		saveGuests.text = "Saved guest percentage: " + _statManager.savedGuestPercentage.ToString ();
		
	}
}
