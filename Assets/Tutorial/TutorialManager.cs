using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public List<GuestClass> GuestList;
	public List<Transform> SpawnPoints;
	public GameObject GuestPrefab;
	public GameObject Dialogue;
	public string LastInvestigatedGuestName;

	public List<GameObject> SpawnedGuests;
	private int murderedGuests;

	private bool ButtonPressed;
	private TypewriterScript typewriter;

	public int InteractedGuests;

	private controlSystem controlSys;
	private bool journalOpened;

	public GameObject Focus;
	public GameObject FocusCalm;
	public GameObject Timer;

	private float timerMax;
	private float timerTime;

	private bool timerDone;

	private bool finalVictim;
	private bool gameWon;

	public GameObject BlackScreen;
	public GameObject EndMenu;

	void Start () {
		timerMax = 20f;
		timerTime = timerMax;
		typewriter = Dialogue.GetComponent<TypewriterScript> ();
		controlSys = GetComponent<controlSystem> ();
		SpawnGuests ();
		StartCoroutine(RunTutorial ());
	}

	void Update () {
		if (Input.GetButtonDown ("Fire3")) {
			journalOpened = true;
		}
		if (Timer.activeSelf) {
			if (timerTime > 0) {
				timerTime -= Time.deltaTime;
			} else {
				if (finalVictim) {
					Lose ();
				}
				timerTime = timerMax;
				timerMax -= 5;
				timerDone = true;
			}
			Timer.GetComponent<Text> ().text = "NEXT VICTIM: " + Mathf.Floor (timerTime).ToString ();
		}
		if (SpawnedGuests [3].GetComponent<Guest> ().arrested) {
			Win ();
		}
	}

	private void Lose () {
		Timer.SetActive (false);
		GameObject.Find ("bar").SetActive (false);
		GameObject.Find ("calmness").SetActive (false);
		BlackScreen.SetActive (true);
		EndMenu.SetActive (true);
		GameObject.Find ("title").GetComponent<Text> ().text = "You didn't get the murderer in time!";
	}

	private void Win() {
		Timer.SetActive (false);
		GameObject.Find ("bar").SetActive (false);
		GameObject.Find ("calmness").SetActive (false);
		BlackScreen.SetActive (true);
		EndMenu.SetActive (true);
		GameObject.Find ("title").GetComponent<Text> ().text = "You got the murderer!";
	}

	IEnumerator RunTutorial () {
		List<string> textToPrint = new List<string> ();
		textToPrint.Add ("Hello There! (Press Space)");
		textToPrint.Add ("The year is 1932 and you are currently in America.");
		textToPrint.Add ("You have been invited to a dinner party with some old clients of yours, you are a detective after all.");
		textToPrint.Add ("There are five other guests at the party with you.");
		textToPrint.Add ("You can walk up to each of them and they will tell you about themselves.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		yield return new WaitForSeconds (20f);
		yield return new WaitForSeconds (0.1f);
		yield return StartCoroutine (lightFlash (2f));
		KillGuest (0, "This guest was bludgeoned to death!");
		textToPrint.Add ("Oh no! It appears a guest has been murdered!");
		textToPrint.Add ("The other guests are looking at you to help!");
		textToPrint.Add ("Walk over to the murdered guest to investigate what happened.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		yield return new WaitForSeconds (3f);
		//StartCoroutine (WaitForGuestInvestigate ("G1"));
		textToPrint.Add("As you can see, this guest has been bludgeoned to death.");
		textToPrint.Add ("This gives you a clue as to whom the murderer may be.");
		textToPrint.Add ("Press Left Shift to access your journal.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		journalOpened = false;
		yield return new WaitForSeconds (1.5f);
		textToPrint.Add ("Your journal tells you which roles can kill in which ways.");
		textToPrint.Add ("You can see that the only people in this room that can bludgeon people are the chef, veteran and author.");
		textToPrint.Add ("Sadly, the author is dead, which leaves only the chef and the veteran.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		yield return new WaitForSeconds (0.1f);
		yield return StartCoroutine (lightFlash (2f));
		KillGuest (1, "This guest was also bludgeoned to death!");
		textToPrint.Add ("Oh no! It appears another guest has died!");
		textToPrint.Add ("Go investigate their body!");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		yield return new WaitForSeconds (3f);
		textToPrint.Add ("This guest appears to have died in the same way!");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		StartCoroutine (HighlightCalmBar (5f));
		textToPrint.Add ("This is the calm bar.");
		textToPrint.Add ("The calm bar indicates how calm the room is.");
		textToPrint.Add ("The room becomes less calm every time a guest dies");
		textToPrint.Add ("As the investigator, it is your job to keep the room calm so you can find the killer.");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		yield return new WaitForSeconds (2f);
		yield return StartCoroutine (lightFlash (0.3f));
		yield return new WaitForSeconds (0.1f);
		yield return StartCoroutine (lightFlash (0.3f));
		textToPrint.Add ("Unfortunately, the killer isn't going to wait for you.");
		textToPrint.Add ("You are on a timer, and the time you have gets shorter after each murder.");
		textToPrint.Add ("You must hurry!");
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		Timer.SetActive (true);
		while (!timerDone) {
			yield return null;
		}
		timerDone = false;
		yield return StartCoroutine (lightFlash (2f));
		KillGuest (4, "This guest has died from multiple cut wounds!");
		yield return new WaitForSeconds (3f);
		textToPrint.Add ("This victim died from cut wounds!");
		textToPrint.Add ("This helps us eliminate the veteran as a potential suspect as he cannot cut.");
		textToPrint.Add ("This leaves only the chef!");
		textToPrint.Add ("To arrest the chef, walk up to him and hold down the right mouse button then the left mouse button."); 
		yield return StartCoroutine (typewriter.displayDialogueInOrder (textToPrint));
		textToPrint.Clear ();
		finalVictim = true;
	}

	IEnumerator WaitForAllGuests() {
		while (InteractedGuests < SpawnedGuests.Count) {
			yield return null;
		}
	}

	IEnumerator HighlightCalmBar (float time) {
		Focus.SetActive (true);
		FocusCalm.SetActive (true);
		yield return new WaitForSeconds (2f);
		Focus.SetActive (false);
		FocusCalm.SetActive (false);
	}

	IEnumerator WaitForJournal() {
		while (!journalOpened) {
			yield return null;
		}
	}

	IEnumerator WaitForGuestInvestigate(string GuestName) {
		while (LastInvestigatedGuestName != GuestName) {
			yield return null;
		}
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
				SpawnedGuests.Add (SpawnedGuest);
			}
			i++;
		}
	}

	void KillGuest(int guestIndex, string deathClue) {
		GameObject guestObject = SpawnedGuests [guestIndex];
		Guest guest = guestObject.GetComponent<Guest> ();
		guest.Die ();
		guest.setDeathText (deathClue);
		murderedGuests++;
		controlSys.calmness -= 20;
		/*		camShake.Shake (0.5f, lightDelay);
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

		_shake ();*/
	}

	IEnumerator lightFlash(float lightDelay){
		GameObject.Find ("Directional light").GetComponent<Light> ().enabled = false;
		yield return new WaitForSeconds (lightDelay);
		GameObject.Find ("Directional light").GetComponent<Light> ().enabled = true;
	}
}
