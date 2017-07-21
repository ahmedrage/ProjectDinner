using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class statDisplay : MonoBehaviour {
	public List<Image> murderPortraitImages;
	public List<Text> _murdererNames;
	public List<Image> _deadGuestImages;
	public Text failedArrests;
	public Text saveGuests;
	public Text freqMethod;
	public Text credBody;
	public Text credTitle;
	public Text deadG;
	public GameObject title;
	public GameObject backToStat;
	public GameObject _credits;
	public GameObject lvlNameGroup;
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

		for (int y = 0; y < _statManager.deadGuestsPorts.Count; y++) {
			_deadGuestImages [y].sprite = _statManager.deadGuestsPorts [y];
			_deadGuestImages [y].enabled = true;
		}
			
		failedArrests.text = "Failed arrests: " + _statManager.failedArrests.ToString ();
		saveGuests.text = "Saved guest percentage: " + _statManager.savedGuestPercentage.ToString () +"%";
		freqMethod.text = "Most frequent murder method: " + _statManager.frequentMethod;
		
	}

	public void Credits(){
		saveGuests.enabled = false;
		freqMethod.enabled = false;
		failedArrests.enabled = false;
		credTitle.enabled = true;
		credBody.enabled = true;
		deadG.enabled = false;
		backToStat.SetActive (true);
		_credits.SetActive (false);
		title.SetActive (false);
		lvlNameGroup.SetActive (false);

		for (int i = 0; i < _deadGuestImages.Count; i++) {
			_deadGuestImages [i].enabled = false;
		}

		for (int z = 0; z < _murdererNames.Count; z++) {
			_murdererNames [z].enabled = false;
		}

		for (int y = 0; y < murderPortraitImages.Count; y++) {
			murderPortraitImages [y].enabled = false;
		}
	}

	public void Menu(){
		SceneManager.LoadScene ("menuScene");
	}

	public void Stats(){ // make own function with parameters
		saveGuests.enabled = true;
		freqMethod.enabled = true;
		failedArrests.enabled = true;
		credTitle.enabled = false;
		credBody.enabled = false;
		deadG.enabled = true;
		backToStat.SetActive (false);
		_credits.SetActive (true);
		title.SetActive (true);
		lvlNameGroup.SetActive (true);

		for (int i = 0; i < _deadGuestImages.Count; i++) {
			_deadGuestImages [i].enabled = true;
		}

		for (int z = 0; z < _murdererNames.Count; z++) {
			_murdererNames [z].enabled = true;
		}

		for (int y = 0; y < murderPortraitImages.Count; y++) {
			murderPortraitImages [y].enabled = true;
		}
	}
}
