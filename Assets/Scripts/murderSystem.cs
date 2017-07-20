﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class murderSystem : MonoBehaviour {
	public bool cameraShake = false;
	public Light light;
	public GameObject murderer;
	public float lightDelay;
	public List<GameObject> guests;
	public List<Sprite> deadPortraits;
	public List<string> _method;
	public int deadGuests;
	public int maxMurderedGuests = 6;
	public int initialGuestCount;
	public Timer script;
	public Clues clueScript;
	public Clue clue;
	public finishConditions finishScript;
	public controlSystem controlSys;
	public statManager _statManager;
	public string murdererName;
	public Sprite murdererPortrait;
	public AudioClip Bludge;
	public AudioClip Cut;
	public AudioClip Shot;
	public AudioClip Stab;
	public float accusePercent;
	cameraShake camShake;

	// Use this for initialization
	void Start () {
		camShake = Camera.main.gameObject.GetComponent<cameraShake> ();
		controlSys = GetComponent<controlSystem> ();
		clueScript = GetComponent<Clues> ();
		finishScript = GetComponent<finishConditions> ();
		int x = Random.Range (0, guests.Count);
		murderer = guests [x];
		guests.RemoveAt (x);
		script = GameObject.Find ("timer").GetComponent<Timer> ();
		KillGuest ();
		murdererName = murderer.GetComponent<Guest> ().guestClass.name;
		murdererPortrait = murderer.GetComponent<Guest> ().guestClass.Portrait;
		_statManager = GameObject.Find ("dataManager").GetComponent<statManager> ();
		initialGuestCount = guests.Count;
		_statManager.totalGuests += initialGuestCount;
		_statManager.lastMurderer = murderer.GetComponent<Guest> ().guestClass;
	}
	
	// Update is called once per frame
	void Update () {
		if (murderer.GetComponent<Guest>().dead == true) {
			finishScript.Win ();
		}

		accusePercent = Mathf.Floor(controlSys.initialCalmness - controlSys.calmness);
	}

	public void KillGuest(){
		camShake.Shake (0.5f, lightDelay);
		controlSys.calmness -= (controlSys.initialCalmness / maxMurderedGuests) * controlSys.negativePromiseMod;
		light.enabled = false;
		int i = Random.Range (0, guests.Count);
		Guest _guestScript = guests[i].GetComponent<Guest>();
		deadPortraits.Add(guests [i].GetComponent<Guest> ().guestClass.Portrait);
		murderer.GetComponent<Guest> ().scared = true;
		murderer.GetComponent<Guest> ().threat = false;
		murderer.GetComponent<Guest> ().shakeFrequencey += 0.005f;
		guests[i].GetComponent<Guest>().Die();
		clue = clueScript.getClue (_guestScript.guestClass, murderer.GetComponent<Guest> ().guestClass);
		print (clue.deathType);
		_method.Add (clue.deathType);
		_guestScript.setDeathText (clue.getDeathClue ());
		deadGuests++;

		int x = Random.Range (0, Mathf.FloorToInt(controlSys.calmness));

		if (x <= accusePercent && controlSys.accused == false) {
			controlSys.accuse ();
		}

		if (clue.appearanceText != null) {
			GetComponent<GuestScript> ().dropHint (guests [i], clue.appearanceText);
		}

		guests.RemoveAt (i);
		StartCoroutine ("lightFlash");
		switch (clue.deathType) {
		case "Cut":
			audioManager.instance.playSound(Cut,Vector2.zero);
			break;
		case "Stab":
			audioManager.instance.playSound(Stab,Vector2.zero);
			break;
		case "Strangle":
			audioManager.instance.playSound(Bludge,Vector2.zero);
			break;
		case "Bludge":
			audioManager.instance.playSound(Bludge,Vector2.zero);
			break;
		case "Shoot":
			audioManager.instance.playSound(Shot,Vector2.zero);
			break;
		default:
			break;
		}

		_shake ();

	}
	IEnumerator lightFlash(){
		yield return new WaitForSeconds (lightDelay);
		light.enabled = true;
	}

	public void _shake(){
		for (int y = 0 ; y < guests.Count; y++) {
			guests [y].GetComponent<Guest> ().scared = true;
			guests [y].GetComponent<Guest> ().threat = false;
			guests [y].GetComponent<Guest> ().shakeFrequencey += 0.005f;
		}
	}
}
