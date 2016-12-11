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
			foreach (string s in careerKills[guest.profession.ToString()]) {
				deathMethods.Add (s);
			}
			foreach (string s in hobbyKills[guest.hobby.ToString()]) {
				deathMethods.Add (s);
			}

			int deathNum = Random.Range (0, (deathMethods.Count - 1));
			Clue deathClue = new Clue (guest, deathMethods [deathNum], false);
			returnList.Add (deathClue);

			int appearanceClue = Random.Range (0, 3);
			if (appearanceClue == 3) {
				//deathClue.setAppearanceText(***MURDERER***);
			}
			return returnList;
		} else {
			return null;
		}
	}
}

[System.Serializable]
public class Clue {
	//Death Clues
	bool isAppearance;
	GuestClass guest;
	string deathType;
	string deathText;

	string appearanceText;


	public Clue(GuestClass _guest, string _deathType, bool _isAppearance) {
		isAppearance = _isAppearance;
		guest = _guest;
		deathType = _deathType;
		//TODO Define clue text
		string deathText = "";

		switch (deathType) {
		case "Cut":
			deathText = "" + guest.name + " appears to have died from multiple cut wounds.";
			break;
		case "Poison":
			deathText = "From what you can tell, " + guest.name + " was poisoned.";
			break;
		case "Stab":
			deathText = "There are multiple stab wounds in " + guest.name + "'s body. This seems to be the cause of death.";
			break;
		case "Strangle":
			deathText = "" + guest.name + " has bruising around their neck; it appears they were strangled to death.";
			break;
		case "Bludge":
			deathText = "It seems as if " + guest.name + " was bludgeoned to death!";
			break;
		case "Shoot":
			deathText = "" + guest.name + " has a bullet wound, they must have been shot.";
			break;
		default:
			deathText = "" + guest.name + "'s death appears to be a mystery!";
			break;
		}
	}

	public string getDeathClue() {
		return deathText;
	}

	public void setAppearanceText(GuestClass murderer) {
		int descriptorRand = Random.Range(0, 1);
		string appearanceClueText = "";
		if (descriptorRand == 1) {
			switch (murderer.Accessory.ToString ()) {
			case "Hat":
				appearanceClueText = "I think the murderer was wearing a hat.";
				break;
			case "Glasses":
				appearanceClueText = "I believe I saw the murderer wearing glasses!";
				break;
			case "WorkAccessory":
				appearanceClueText = "They had some sort of technical instrument.";
				break;
			default:
				appearanceClueText = "The murderer didn't seem to have any sort of accessory.";
				break;
			}
		} else {
			switch(murderer.Blemish.ToString()) {
			case "Tattoo": 
				appearanceClueText = "I saw that the murderer had a tattoo.";
				break;
			case "Scar":
				appearanceClueText = "They had some sort of horrible scar!";
				break;
			case "SkinCondition":
				appearanceClueText = "The murderer had the most horrible skin.";
				break;
			default:
				appearanceClueText = "The murderer had plain skin.";
				break;
		}
	}
}
}
