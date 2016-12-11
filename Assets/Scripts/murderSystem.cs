using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class murderSystem : MonoBehaviour {

	public Light light;
	public GameObject murderer;
	public float lightDelay;
	public List<GameObject> guests;
	public Timer script;

	// Use this for initialization
	void Start () {
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
			//print ("you lose");
		}

		if (murderer == null) {
			//print ("you win");
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
		Destroy(guests[i]);
		guests.RemoveAt (i);
		StartCoroutine ("lightFlash");
	}
	IEnumerator lightFlash(){
		yield return new WaitForSeconds (lightDelay);
		light.enabled = true;
	}
}
