using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class murderSystem : MonoBehaviour {

	public Light light;
	public GameObject murderer;
	public List<GameObject> guests;
	public float timeInterval = 30;

	// Use this for initialization
	void Start () {
		int x = Random.Range (0, guests.Count);
		murderer = guests [x];
		guests.RemoveAt (x);
		print (murderer.name + ": Is the murderer");
		InvokeRepeating ("KillGuest", 0f, timeInterval);
	}
	
	// Update is called once per frame
	void Update () {
		if (guests.Count <= 6 || murderer == null) {
			CancelInvoke();
		}

		if (guests.Count <= 6 && murderer != null) {
			print ("you lose");
		}

		if (murderer == null) {
			print ("you win");
		}
	}

	void KillGuest(){
		light.enabled = false;
		int i = Random.Range (0, guests.Count);
		Vector2 tempGuestPos = guests [i].transform.position;
			
		List<Clue> clues = gameObject.GetComponent<Clues> ().getClue (guests [i].GetComponent<Guest> (), murderer.GetComponent<Guest> ());

		foreach (Clue clue in clues) {
			print (clue.getDeathClue ());
		}

		Destroy (guests [i]);
		//Instantiate (murderedGuests[i], tempGuestPos, Quaternion.identity);
		guests.RemoveAt (i);
		timeInterval -= 5;
		StartCoroutine ("lightFlash");
	}
	IEnumerator lightFlash(){
		yield return new WaitForSeconds (0.5f);
		light.enabled = true;
	}
}
