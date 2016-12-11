using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class MurderType {
	public string name;
	public string description;
}

[System.Serializable]
public class Profession {
	public string name;
	public MurderType[] murderTypes;
}

[System.Serializable]
public class Hobby {
	public string name;
	public MurderType[] murderTypes;
}*/


public class Clues : MonoBehaviour {

	List<GuestClass> murderedGuests = new List<GuestClass>();

    Dictionary<string, string[]> hobbyKills = new Dictionary<string, string[]>();
    Dictionary<string, string[]> careerKills = new Dictionary<string, string[]>();

	//public MurderType[] murderTypes;

    // Use this for initialization
    void Start() {

        string[] kills = { "Cut", "Poison", "Stab", "Strangle", "Bludge", "Shoot" };

        string[] golf = { "Strangle", "Bludge" };
        hobbyKills.Add("Golf", golf);

        string[] baseball = { "Strangle", "Bludge" };
        hobbyKills.Add("Baseball", baseball);

        string[] fishing = { "Cut", "Strangle" };
        hobbyKills.Add("Fishing", fishing);

        string[] hunting = { "Poison", "Shoot" };
        hobbyKills.Add("Hunting", hunting);

        string[] collecting = { "Shoot", "Bludge" };
        hobbyKills.Add("Collecting", collecting);

        string[] shooting = { "Bludge", "Shoot" };
        hobbyKills.Add("Shooting", shooting);

        string[] fencing = { "Cut", "Stab" };
        hobbyKills.Add("Fencing", fencing);

        string[] cooking = { "Cut", "Poison" };
        hobbyKills.Add("Cooking", cooking);

        //Careers

        string[] doctor = { "Poison", "Strangle", "Stab" };
        careerKills.Add("Doctor", doctor);

        string[] scientist = { "Cut", "Poison", "Stab" };
        careerKills.Add("Scientist", scientist);

        string[] author = { "Cut", "Poison", "Bludge" };
        careerKills.Add("Author", author);

        string[] homemaker = { "Cut", "Stab", "Bludge" };
        careerKills.Add("Home-Maker", homemaker);

        string[] chef = { "Cut", "Stab", "Bludge" };
        careerKills.Add("Chef", chef);

        string[] politician = { "Poison", "Strang", "Shoot" };
        careerKills.Add("Politician", politician);

        string[] filmmaker = { "Strangle", "Bludge", "Shoot" };
        careerKills.Add("Film-Maker", filmmaker);

        string[] veteran = { "Stab", "Bludge", "Shoot" };
        careerKills.Add("Veteran", veteran);
       }
	
	// Update is called once per frame
	void Update () {
	
	}

	public Dictionary<string, string[]> getHobbyKills() {
		return hobbyKills;
	}

	public Dictionary<string, string[]> getCareerKills() {
		return careerKills;
	}

	public List<Clue> getClue(GuestClass guest) {
		List<Clue> returnList = new List<Clue> ();
		bool guestAlreadyDead = false;
		foreach (GuestClass _guest in murderedGuests) {
			if (_guest == guest) {
				guestAlreadyDead = true;
			}
		}
		if (!guestAlreadyDead) {
			List<string> deathMethods = new List<string> ();
			foreach (string s in careerKills[guest.profession]) {
				deathMethods.Add (s);
			}
			foreach (string s in hobbyKills[guest.hobby]) {
				deathMethods.Add (s);
			}

			int deathNum = Random.Range (0, (deathMethods.Count - 1));
			Clue deathClue = new Clue (guest, deathMethods[deathNum], 0);
			returnList.Add (deathClue);

			int appearanceClue = Random.Range (0, 3);
			if (appearanceClue == 3) {
				//TODO: Write appearance clue code.
			}
			return returnList;
		}
	}
}

[System.Serializable]
public class Clue {
	//Death Clues
	bool isAppearance;
	GuestClass guest;
	string deathType;
	string clueText;

	public Clue(GuestClass _guest, string _deathType, bool _isAppearance) {
		isAppearance = _isAppearance;
		guest = _guest;
		deathType = _deathType;

		//TODO Define clue text
	}

	public string getClue() {
		return clueText;
	}
}
