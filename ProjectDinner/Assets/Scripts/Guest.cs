using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	public bool dead;
	public Transform Panel;
	Transform deathPanel;
	void Start () {
		deathPanel = transform.GetChild (0).transform.GetChild (1);


	}
	
	// Update is called once per frame
	void Update () {

	}
	public void setText () {
		Panel = transform.GetChild (0).transform.GetChild (0);
		Panel.transform.GetChild (0).GetComponent<Text> ().text = guestClass.name;
		Panel.transform.GetChild (1).GetComponent<Text> ().text = guestClass.profession.ToString();
		Panel.transform.GetChild (2).GetComponent<Text> ().text = guestClass.hobby.ToString();
		Panel.transform.GetChild (3).GetComponent<Text> ().text = guestClass.Accessory.ToString();
		Panel.transform.GetChild (4).GetComponent<Text> ().text = guestClass.Blemish.ToString();

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && dead == false && Panel != null) {
			Panel.gameObject.SetActive (true);
		} else if (other.gameObject.tag == "Player" && dead == true && deathPanel != null) {
			print (dead.ToString ());
			deathPanel.gameObject.SetActive (true);
		}
		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && Panel != null && deathPanel != null) {
			Panel.gameObject.SetActive (false);
			deathPanel.gameObject.SetActive (false);
		}
	}

	public void Die(string clue) {
		dead = true;
		if (clue != null) {
			deathPanel.transform.GetChild (0).GetComponent<Text> ().text = clue;
		}
	}
}
