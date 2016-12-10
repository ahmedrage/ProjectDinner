using UnityEngine;
using System.Collections;
[System.Serializable]
public class GuestClass {
	public string name;
	public enum professions {Doctor, Scientist, Author, HomeMaker, Chef, Politician, FilmMaker, Vetran};
	public professions profession;
	public enum Hobbies {Golf, Baseball, Fishing, Hunting, Collecting, Shooting, Fencing, Cooking};
	public Hobbies hobby;
	public enum Accessories {Hat, Glasses, WorkAccessory, None};
	public Accessories Accessory;
	public enum Blemishes {Tattoo, Scar, SkinCondition, None};
	public Blemishes Blemish;


	public GameObject GuestGameObject;
	public UnityEditor.Animations.AnimatorController GuestAnimatorController;
}

public class GuestScript : MonoBehaviour {
	public GuestClass[] GuestArray;
	public Transform[] SpawnPoints;
	public GameObject GuestPrefab;
	// Use this for initialization
	void Start () {
		SpawnGuests ();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void SpawnGuests() {
		int i = 0;
		foreach (var item in GuestArray) {
			if (i <= SpawnPoints.Length) { 
				GameObject SpawnedGuest = Instantiate (GuestPrefab, SpawnPoints[i].position,GuestPrefab.transform.rotation) as GameObject;
				SpawnedGuest.GetComponent<Guest> ().guestClass = item;
				SpawnedGuest.GetComponent<Guest> ().setText ();
			}
			i++;
		}
	}
}
