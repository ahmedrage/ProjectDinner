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

	public string getClue() {
		int appearanceClue = Random.Range (0, 3);
		if (appearanceClue == 3) {
			//TODO: Write appearance clue code.
		}
		//TODO: Write non-appearance clue code
		return "Unity, you are a fucking dick cunt whore face!";
	}
}

[System.Serializable]
public class Clue {
	bool isAppearance;
	string clueText;
	string guest;
	//TODO: Change the player reference to the player or guest class
	public Clue(string _guest, string _clueText, bool _isAppearance) {
		isAppearance = _isAppearance;
		clueText = _clueText;
		guest = _guest;
	}
}
