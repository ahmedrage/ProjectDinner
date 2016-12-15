using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class finishConditions : MonoBehaviour {

	public string numDeadGuests;
	public string murderer;
	public GameObject endGameMenu;
	public GameObject imgs;
	public GameObject pauseScript;
	public Sprite murdererPortrait;
	public Image murdererPortraitImg;
	public List<Image> murderedGuestPortraits;
	public List<Sprite> murderedGuestSprites;
	public bool win;
	public bool lose;
	public Clues clueScript;
	public murderSystem murdererScript;
	public Text title;
	public Text murdererName;
	public Text numDeadGuestsText;
	public Texture2D normalCursor;
	public int loseCondition;

	// Use this for initialization
	void Start () {
		murdererScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		pauseScript = GameObject.Find ("pauseMenu");
		murdererPortraitImg.enabled =false;
		murdererName.enabled = false;
		numDeadGuestsText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		endGameInfo ();
	}

	void endGameInfo(){
		numDeadGuests = murdererScript.deadGuests.ToString();
		numDeadGuestsText.text = "You let " + numDeadGuests +" Guest die"; // even to a guy like me that's cold
		murderer = murdererScript.murdererName;
		murdererName.text = "Murderer: " + murderer;
		murdererPortrait = murdererScript.murdererPortrait;
		murdererPortraitImg.sprite = murdererPortrait;
		murderedGuestSprites = murdererScript.deadPortraits;

		for (int i = 0; i < murderedGuestSprites.Count; i++) {
			murderedGuestPortraits[i].sprite = murderedGuestSprites[i];
			murderedGuestPortraits[i].enabled = true;
		}
	}

	void enableUI(){
		murdererPortraitImg.enabled =true;
		murdererName.enabled = true;
		numDeadGuestsText.enabled = true;
		endGameMenu.SetActive (true);
		imgs.SetActive (true);
		Cursor.SetCursor (normalCursor, Vector2.zero, CursorMode.Auto);
		Destroy (pauseScript);
	}

	public void Win(){
		print ("you win");
		title.text = "You got the murderer";
		win = true;
		enableUI ();
	}

	public void Lose(){
		switch (loseCondition) {
		case 1:
			print ("You shot the wrong person");
			title.text = "You shot the wrong person";
			break;
		case 2:
			print ("You let to many people die");
			title.text = "You let to many people die";
			break;
		case 3:
			print ("You wasted your one bullet");
			title.text = "You wasted your one bullet";
			break;
		}
		enableUI ();
		lose = true;
	}
}
