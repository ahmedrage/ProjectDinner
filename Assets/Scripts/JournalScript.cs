using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class JournalScript : MonoBehaviour {

	public bool crossed = false;
	public enum profession {Doctor, Scientist, Author, Home_maker, Chef, Politician, Film_maker, Veteran};
	public profession prof;
	public Dictionary<string, string> profession_desc = new Dictionary<string, string> ();
	public Dictionary<string, string> profession_name = new Dictionary<string, string> ();

	// Use this for initialization
	void Start () {
		profession_desc.Add ("Doctor", "- Poison \n- Stab \n- Strangle");
		profession_desc.Add ("Scientist", "- Cut \n- Poison \n- Stab");
		profession_desc.Add ("Author", "- Cut \n- Poison \n- Bludgeon");
		profession_desc.Add ("Home_maker", "- Cut \n- Stab \n- Bludgeon");
		profession_desc.Add ("Chef", "- Cut \n- Stab \n- Bludgeon");
		profession_desc.Add ("Politician", "- Poison \n- Strangle \n- Shoot");
		profession_desc.Add ("Film_maker", "- Strangle \n- Bludgeon \n- Shoot");
		profession_desc.Add ("Veteran", "- Stab \n- Bludgeon \n- Shoot");

		profession_name.Add ("Doctor", "Doctor");
		profession_name.Add ("Scientist", "Scientist");
		profession_name.Add ("Author", "Author");
		profession_name.Add ("Home_maker", "Home maker");
		profession_name.Add ("Chef", "Chef");
		profession_name.Add ("Politician", "Politician");
		profession_name.Add ("Film_maker", "Film maker");
		profession_name.Add ("Veteran", "Veteran");

		DisplayInfo ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleCrossed () {
		crossed = crossed; // !
		DisplayInfo ();
	}

	public void DisplayInfo() {
		if (crossed) {
			gameObject.GetComponentInChildren<Text> ().text = prof.ToString ();
			gameObject.GetComponentInChildren<Text> ().color = new Color (1, 0, 0, 1);
		} else {
			gameObject.GetComponentInChildren<Text> ().text = profession_name[prof.ToString ()].ToString() + ": \n" + profession_desc [prof.ToString ()].ToString ();
			gameObject.GetComponentInChildren<Text> ().color = new Color (0, 0, 0, 1);
		}

	}
}