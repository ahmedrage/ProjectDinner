using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class murderSystem : MonoBehaviour {

	public Light light;
	public GameObject murderer;
	public float lightDelay;
	public List<GameObject> guests;
	public List<Sprite> deadPortraits;
	public int deadGuests;
	public Timer script;
	public Clues clueScript;
	public Clue clue;
	public finishConditions finishScript;
	public string murdererName;
	public Sprite murdererPortrait;


	// Use this for initialization
	void Start () {
		clueScript = GetComponent<Clues> ();
		finishScript = GetComponent<finishConditions> ();
		int x = Random.Range (0, guests.Count);
		murderer = guests [x];
		guests.RemoveAt (x);
		script = GameObject.Find ("timer").GetComponent<Timer> ();
		KillGuest ();
		murdererName = murderer.GetComponent<Guest> ().guestClass.name;
		murdererPortrait = murderer.GetComponent<Guest> ().guestClass.Portrait;
	}
	
	// Update is called once per frame
	void Update () {
		if (deadGuests == 6) {
			finishScript.loseCondition = 2;
			finishScript.Lose ();
		}

		if (murderer.GetComponent<Guest>().dead == true) {
			finishScript.Win ();
		}
	}

	public void KillGuest(){
		light.enabled = false;
		int i = Random.Range (0, guests.Count);
		//Vector2 tempGuestPos = guests[i].transform.position;
		//guests [i].GetComponent<Guest>().Die();
		Guest _guestScript = guests[i].GetComponent<Guest>();
		deadPortraits.Add(guests [i].GetComponent<Guest> ().guestClass.Portrait);

		guests[i].GetComponent<Guest>().Die();
		clue = clueScript.getClue (_guestScript.guestClass, murderer.GetComponent<Guest> ().guestClass);
		_guestScript.setDeathText (clue.getDeathClue ());
		deadGuests++;

		if (clue.appearanceText != null) {
			GetComponent<GuestScript> ().dropHint (guests [i], clue.appearanceText);
		}

		guests.RemoveAt (i);
		StartCoroutine ("lightFlash");
		switch (clue.deathType) {
		case "Cut":
			GetComponent<Clues> ().Cut.Play ();
			break;
		case "Stab":
			GetComponent<Clues> ().Stab.Play ();
			break;
		case "Strangle":
			GetComponent<Clues> ().Bludge.Play ();
			break;
		case "Bludge":
			GetComponent<Clues> ().Bludge.Play ();
			break;
		case "Shoot":
			GetComponent<Clues> ().Shot.Play ();
			break;
		default:
			break;
		}
	}
	IEnumerator lightFlash(){
		yield return new WaitForSeconds (lightDelay);
		light.enabled = true;
	}
}
