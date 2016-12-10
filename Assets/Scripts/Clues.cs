using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clues : MonoBehaviour {

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

    public Dictionary<string, string[]> getCareerKills()
    {
        return careerKills;
    }

    public Dictionary<string, string[]> getHobbyKills()
    {
        return hobbyKills;
    }
}
