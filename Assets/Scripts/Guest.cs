using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	public GuestClass[] GuestArray;
	public bool dead;
	public Transform Panel;
	public Transform Portrait;
	Transform deathPanel;
	public murderSystem murderScript;

	void Start () {
		deathPanel = transform.GetChild (0).transform.GetChild (1);
		Portrait = transform.FindChild ("Canvas").FindChild ("Portrait");
		murderScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void setText () {
		Panel = transform.GetChild (0).transform.GetChild (0);
		Panel.transform.GetChild (0).GetComponent<Text> ().text = guestClass.name;
		Panel.transform.GetChild (1).GetComponent<Text> ().text = guestClass.profession.ToString();
		Panel.transform.GetChild (2).GetComponent<Text> ().text = "Likes " + guestClass.hobby.ToString();
		Panel.transform.GetChild (3).GetComponent<Text> ().text = "Is wearing a " + guestClass.Accessory.ToString();
		Panel.transform.GetChild (4).GetComponent<Text> ().text = "Has a " +guestClass.Blemish.ToString();

	}

	public void setDeathText (string deathText) {
		deathPanel.GetChild (0).GetComponent<Text> ().text = deathText;
	}

	public void setHintText (string hintText) {
		print (guestClass.name);
		Panel.transform.GetChild (5).GetComponent<Text> ().text = hintText;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && dead == false && Panel != null) {
			Panel.gameObject.SetActive (true);
		} else if (other.gameObject.tag == "Player" && dead == true && deathPanel != null) {
			print (dead.ToString ());
			deathPanel.gameObject.SetActive (true);
		}

		if (other.gameObject.tag == "Player" && Portrait != null) {
			Portrait.gameObject.SetActive (true);
		}
		
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && Panel != null && deathPanel != null) {
			Panel.gameObject.SetActive (false);
			deathPanel.gameObject.SetActive (false);
		}
		if (other.gameObject.tag == "Player" && Portrait != null) {
			Portrait.gameObject.SetActive (false);
		}
	}

	public void Die() {
		dead = true;
		transform.FindChild ("Graphics").GetComponent<SpriteRenderer> ().sprite = guestClass.deadSprite;
	}
}
