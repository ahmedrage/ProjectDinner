using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clues : MonoBehaviour {

	public List<GuestClass> murderedGuests = new List<GuestClass>();

    Dictionary<string, string[]> hobbyKills = new Dictionary<string, string[]>();
    Dictionary<string, string[]> careerKills = new Dictionary<string, string[]>();

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
		careerKills.Add("HomeMaker", homemaker);

        string[] chef = { "Cut", "Stab", "Bludge" };
		careerKills.Add("Chef", chef);

        string[] politician = { "Poison", "Strang", "Shoot" };
		careerKills.Add("Politician", politician);

        string[] filmmaker = { "Strangle", "Bludge", "Shoot" };
		careerKills.Add("FilmMaker", filmmaker);

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

	public Clue getClue(GuestClass guest, GuestClass murderer) {
			List<string> deathMethods = new List<string> ();
			foreach (string s in careerKills[guest.profession.ToString()]) {
				deathMethods.Add (s);
			}
			foreach (string s in hobbyKills[guest.hobby.ToString()]) {
				deathMethods.Add (s);
			}

			int deathNum = Random.Range (0, (deathMethods.Count - 1));
			Clue deathClue = new Clue (guest, deathMethods [deathNum], false);

			int appearanceClue = Random.Range (2, 4);
			if (appearanceClue == 3) {
				deathClue.setAppearanceText(murderer);
			}
			return deathClue;
	}
}

[System.Serializable]
public class Clue {
	//Death Clues
	public bool isAppearance;
	public GuestClass guest;
	public string deathType;
	public string deathText;

	public string appearanceText;


	public Clue(GuestClass _guest, string _deathType, bool _isAppearance) {
		isAppearance = _isAppearance;
		guest = _guest;
		deathType = _deathType;
		//TODO Define clue text
		deathText = "";

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
		int descriptorRand = Random.Range(0, 2);
		if (descriptorRand == 1) {
			switch (murderer.Accessory.ToString ()) {
			case "Hat":
				appearanceText = "I think the murderer was wearing a hat.";
				break;
			case "Glasses":
				appearanceText = "I believe I saw the murderer wearing glasses!";
				break;
			case "WorkAccessory":
				appearanceText = "They had some sort of technical instrument.";
				break;
			default:
				appearanceText = "The murderer didn't seem to have any sort of accessory.";
				break;
			}
		} else {
			switch(murderer.Blemish.ToString()) {
			case "Tattoo": 
				appearanceText = "I saw that the murderer had a tattoo.";
				break;
			case "Scar":
				appearanceText = "They had some sort of horrible scar!";
				break;
			case "SkinCondition":
				appearanceText = "The murderer had the most horrible skin.";
				break;
			default:
				appearanceText = "The murderer had plain skin.";
				break;
		}
	}
}
}
