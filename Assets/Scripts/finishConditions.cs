using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class finishConditions : MonoBehaviour {

	public string numDeadGuests;
	public string murderer;
	public GameObject endGameMenu;
	public GameObject imgs;
	public GameObject pauseScript;
	public GameObject restartButton;
	public Sprite murdererPortrait;
	public Image murdererPortraitImg;
	public List<Image> murderedGuestPortraits;
	public List<Sprite> murderedGuestSprites;
	public bool win;
	public bool lose;
	public Clues clueScript;
	public murderSystem murdererScript;
	public controlSystem controlScript;
	public Text title;
	public Text murdererName;
	public Text numDeadGuestsText;
	public Texture2D normalCursor;
	public EventSystem eventSystem;

	// Use this for initialization
	void Start () {
		murdererScript = GetComponent<murderSystem> ();
		controlScript = GetComponent<controlSystem> ();
		pauseScript = GameObject.Find ("pauseMenu");
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
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
		controlScript.responseImg.enabled = false;
		controlScript.dialoguePannel.SetActive(false);
		murdererPortraitImg.enabled =true;
		murdererName.enabled = true;
		numDeadGuestsText.enabled = true;
		endGameMenu.SetActive (true);
		imgs.SetActive (true);
		Cursor.SetCursor (normalCursor, Vector2.zero, CursorMode.Auto);
		Destroy (pauseScript);

		if (GamePad.GetState(PlayerIndex.One).IsConnected == false) {
			eventSystem.SetSelectedGameObject(null);
		}

		if (GamePad.GetState(PlayerIndex.One).IsConnected && eventSystem.currentSelectedGameObject == null) {
			eventSystem.SetSelectedGameObject (restartButton);
		}
	}

	public void Win(){
		title.text = "You got the murderer";
		win = true;
		enableUI ();
	}

	public void Lose(){
		title.text = "You lost control of the crowd and the killer got away";
		enableUI ();
		lose = true;
	}
}
