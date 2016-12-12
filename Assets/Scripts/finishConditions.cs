using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class finishConditions : MonoBehaviour {

	public string numDeadGuests;
	public string murderer;
	public Sprite murdererPortrait;
	public Image murdererPortraitImg;
	public bool win;
	public bool lose;
	public Clues clueScript;
	public murderSystem murdererScript;
	public Text title;
	public Text murdererName;
	public Text numDeadGuestsText;
	public int loseCondition;

	string x;


	// Use this for initialization
	void Start () {
		murdererScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		murdererPortraitImg.enabled =false;
		murdererName.enabled = false;
		numDeadGuestsText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		endGameInfo ();

		if (numDeadGuests == "1") {
			x = "";
		} else {
			x = "s";
		}
	}

	void endGameInfo(){
		numDeadGuests = murdererScript.deadGuests.ToString();
		numDeadGuestsText.text = "You let " + numDeadGuests +" Guest"+x +" die"; // even to a guy like me that's cold
		murderer = murdererScript.murdererName;
		murdererName.text = "Murderer: " + murderer;
		murdererPortrait = murdererScript.murdererPortrait;
		murdererPortraitImg.sprite = murdererPortrait;
	}

	public void Win(){
		print ("you win");
		title.text = "You got the murderer";
		win = true;
		murdererPortraitImg.enabled =true;
		murdererName.enabled = true;
		numDeadGuestsText.enabled = true;
	}

	public void Lose(){
		switch (loseCondition) {
		case 1:
			print ("You shot the wrong person");
			title.text = "You shot the wrong person";
			break;
		case 2:
			print ("you let to many people died");
			title.text = "You let to many people died";
			break;
		case 3:
			print ("you wasted your one bullet");
			title.text = "You wasted your one bullet";
			break;
		}
		murdererPortraitImg.enabled =true;
		murdererName.enabled = true;
		numDeadGuestsText.enabled = true;
		lose = true;
	}
}
