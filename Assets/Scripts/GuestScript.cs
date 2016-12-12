using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GuestClass {
	public string name;
	public enum professions {Doctor, Scientist, Author, HomeMaker, Chef, Politician, FilmMaker, Veteran};
	public professions profession;
	public enum Hobbies {Golf, Baseball, Fishing, Hunting, Collecting, Shooting, Fencing, Cooking};
	public Hobbies hobby;
	public enum Accessories {Hat, Glasses, WorkAccessory, NoAccesories};
	public Accessories Accessory;
	public enum Blemishes {Tattoo, Scar, SkinCondition, CleanSkin};
	public Blemishes Blemish;


	public GameObject GuestGameObject;
	public Sprite idleSprite;
	public Sprite Portrait;
	public Sprite deadSprite;
}

public class GuestScript : MonoBehaviour {
	public GuestClass[] GuestArray;
	public Transform[] SpawnPoints;
	public GameObject GuestPrefab;
	public murderSystem murderScript;
	public Clues clueScript;
	public GameObject hintPannel;
	public float maxClueDistance;
	// Use this for initialization
	void Start () {
		murderScript = GetComponent<murderSystem> ();
		SpawnGuests ();
		murderScript.enabled = true;
		clueScript = GetComponent<Clues> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.J)) {
			hintPannel.SetActive (!(hintPannel.activeSelf));
		}
	}

	void SpawnGuests() {
		int i = 0;
		murderScript.guests = new List<GameObject> ();
		foreach (var item in GuestArray) {
			if (i <= SpawnPoints.Length) { 
				GameObject SpawnedGuest = Instantiate (GuestPrefab, SpawnPoints[i].position,GuestPrefab.transform.rotation) as GameObject;
				SpawnedGuest.transform.FindChild ("Graphics").rotation = SpawnPoints [i].rotation;
				SpawnedGuest.GetComponent<Guest> ().guestClass = item;
				SpawnedGuest.GetComponent<Guest> ().setText ();
				SpawnedGuest.transform.FindChild ("Graphics").GetComponent<SpriteRenderer> ().sprite = item.idleSprite;
				SpawnedGuest.transform.FindChild ("Canvas").FindChild ("Portrait").GetComponent<SpriteRenderer> ().sprite = item.Portrait;
				item.GuestGameObject = SpawnedGuest;
				murderScript.guests.Add (SpawnedGuest);
			}
			i++;
		}
	}

	public void dropHint(GameObject deadGuest, string clue) {
		float lowestDist = 100;
		GameObject closestGuest = deadGuest;
		foreach (var item in GuestArray) {
			
			if(Vector2.Distance(item.GuestGameObject.transform.position, deadGuest.transform.position) < lowestDist && item.GuestGameObject.GetComponent<Guest>().dead ==false) {
				closestGuest = item.GuestGameObject;
				lowestDist = Vector2.Distance (item.GuestGameObject.transform.position, deadGuest.transform.position);
			}
		}
		closestGuest.GetComponent<Guest> ().setHintText (clue);
	}
}
