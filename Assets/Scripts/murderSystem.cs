using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class murderSystem : MonoBehaviour {

	public Light light;
	public GameObject murderer;
	public float lightDelay;
	public List<GameObject> guests;
	public Timer script;
	public Clues clueScript;
	public Clue clue;
	// Use this for initialization
	void Start () {
		clueScript = GetComponent<Clues> ();
		int x = Random.Range (0, guests.Count);
		murderer = guests [x];
		guests.RemoveAt (x);
		print (murderer.name + ": Is the murderer");
		script = GameObject.Find ("timer").GetComponent<Timer> ();
		KillGuest ();
	}
	
	// Update is called once per frame
	void Update () {
		if (guests.Count <= 6) {
			print ("you lose");
		}

		if (murderer.GetComponent<Guest>().dead == true) {
			print ("You win");
		}

		if (script.restartTimer) {
			KillGuest();
		}
	}

	void KillGuest(){
		light.enabled = false;
		int i = Random.Range (0, guests.Count);
		//Vector2 tempGuestPos = guests[i].transform.position;
		//guests [i].GetComponent<Guest>().Die();
		Guest _guestScript = guests[i].GetComponent<Guest>();
		guests[i].GetComponent<Guest>().Die();
		clue = clueScript.getClue (_guestScript.guestClass, murderer.GetComponent<Guest> ().guestClass);
		print(clueScript.getClue (_guestScript.guestClass, murderer.GetComponent<Guest> ().guestClass).getDeathClue());
		_guestScript.setDeathText (clueScript.getClue (_guestScript.guestClass, murderer.GetComponent<Guest> ().guestClass).getDeathClue ());

		if (clue.appearanceText != null) {
			GetComponent<GuestScript> ().dropHint (guests [i], clue.appearanceText);
		}

		guests.RemoveAt (i);
		StartCoroutine ("lightFlash");
	}
	IEnumerator lightFlash(){
		yield return new WaitForSeconds (lightDelay);
		light.enabled = true;
	}
}
