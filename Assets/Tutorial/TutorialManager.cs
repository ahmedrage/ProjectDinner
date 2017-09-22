using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

	public List<GuestClass> GuestList;
	public List<Transform> SpawnPoints;
	public GameObject GuestPrefab;
	public GameObject Dialogue;

	private bool ButtonPressed;
	private TypewriterScript typewriter;

	// Use this for initialization
	void Start () {
		typewriter = Dialogue.GetComponent<TypewriterScript> ();
		SpawnGuests ();
		StartCoroutine(RunTutorial ());
	}
	
	// Update is called once per frame
	/*void Update () {
		if ((Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) && dialogueText.text.Length >= text.Length) {
			display = true;
		} else if ((Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) && dialogueText.text.Length < text.Length) {
			completeLineInstant = true;
		}
	}*/

	IEnumerator RunTutorial () {
		List<string> textToPrint = new List<string> ();
		textToPrint.Add ("Hello There! (Press Space)");
		textToPrint.Add ("The year is 1932 and you are currently in America.");
		textToPrint.Add ("You have been invited to a dinner party with some old clients of yours, you are a detective after all.");
		textToPrint.Add ("There are five other guests at the party with you.");
		textToPrint.Add ("You can walk up to each of them and they will tell you about themselves.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		yield return new WaitForSeconds (0.1f);
		yield return StartCoroutine (lightFlash (1f));
	}

	void SpawnGuests() {
		int i = 0;
		foreach (var item in GuestList) {
			if (i <= SpawnPoints.Count) { 
				GameObject SpawnedGuest = Instantiate (GuestPrefab, SpawnPoints[i].position,GuestPrefab.transform.rotation) as GameObject;
				SpawnedGuest.transform.Find ("Graphics").rotation = SpawnPoints [i].rotation;
				SpawnedGuest.GetComponent<Guest> ().guestClass = item;
				SpawnedGuest.GetComponent<Guest> ().setText ();
				SpawnedGuest.transform.Find ("Graphics").GetComponent<Animator> ().runtimeAnimatorController = item.animationController;
				SpawnedGuest.transform.Find ("Graphics").GetComponent<SpriteRenderer> ().sprite = item.idleSprite;
				SpawnedGuest.transform.Find ("Canvas").Find ("Portrait").GetComponent<SpriteRenderer> ().sprite = item.Portrait;
				item.GuestGameObject = SpawnedGuest;
			}
			i++;
		}
	}

	IEnumerator lightFlash(float lightDelay){
		GameObject.Find ("Directional light").GetComponent<Light> ().enabled = false;
		yield return new WaitForSeconds (lightDelay);
		GameObject.Find ("Directional light").GetComponent<Light> ().enabled = true;
	}
}
