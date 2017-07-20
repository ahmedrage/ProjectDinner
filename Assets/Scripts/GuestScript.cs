using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GuestClass {
	public string name;
	public enum professions {Doctor, Scientist, Author, Home_maker, Chef, Politician, Film_maker, Veteran};
	public professions profession;
	public enum Accessories {hat, glasses, work_accessory, no_accesory};
	public Accessories Accessory;
	public enum Blemishes {tattoo, scar, skin_condition, clean_skin};
	public Blemishes Blemish;


	public GameObject GuestGameObject;
	public Sprite idleSprite;
	public Sprite Portrait;
	public Sprite[] interrogationSprites; //0 = bad; 1 = good; 2 = neuteral  
	public Sprite deadSprite;
	public RuntimeAnimatorController animationController;
}

public class GuestScript : MonoBehaviour {
	public GuestClass[] GuestArray;
	public Transform[] SpawnPoints;
	public GameObject GuestPrefab;
	public murderSystem murderScript;
	public Clues clueScript;
	public float maxClueDistance;
	// Use this for initialization
	void Start () {
		murderScript = GetComponent<murderSystem> ();
		SpawnGuests ();
		murderScript.enabled = true;
		clueScript = GetComponent<Clues> ();
	}

	// Update is called once per frame
	void SpawnGuests() {
		int i = 0;
		murderScript.guests = new List<GameObject> ();
		foreach (var item in GuestArray) {
			if (i <= SpawnPoints.Length) { 
				GameObject SpawnedGuest = Instantiate (GuestPrefab, SpawnPoints[i].position,GuestPrefab.transform.rotation) as GameObject;
				SpawnedGuest.transform.Find ("Graphics").rotation = SpawnPoints [i].rotation;
				SpawnedGuest.GetComponent<Guest> ().guestClass = item;
				SpawnedGuest.GetComponent<Guest> ().setText ();
				SpawnedGuest.transform.Find ("Graphics").GetComponent<Animator> ().runtimeAnimatorController = item.animationController;
				SpawnedGuest.transform.Find ("Graphics").GetComponent<SpriteRenderer> ().sprite = item.idleSprite;
				SpawnedGuest.transform.Find ("Canvas").Find ("Portrait").GetComponent<SpriteRenderer> ().sprite = item.Portrait;
				item.GuestGameObject = SpawnedGuest;
				murderScript.guests.Add (SpawnedGuest);
			}
			i++;
		}
		gameObject.GetComponent<AnimationManager> ().guests = murderScript.guests;
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
