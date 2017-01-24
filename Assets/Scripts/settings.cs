using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using System.Collections;
using System.Collections.Generic;

public class settings : MonoBehaviour {

	public Toggle fullscreen;
	public Text instructionText;
	public Text instructionKeyboardText;
	public Text instructionControllerText;
	public Text controlsTitle;
	public List<GameObject> settingUI;

	[HideInInspector] public bool controlsSelected;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (fullscreen.isOn == true) {
			Screen.fullScreen = true;
		} else {
			Screen.fullScreen = false;
		}

		if (GamePad.GetState (PlayerIndex.One).IsConnected == true) {
			instructionText.text = instructionControllerText.text;
		} else {
			instructionText.text = instructionKeyboardText.text;
		}
	}

	public void controls(){
		instructionText.enabled = true;
		controlsTitle.text = "CONTROLS";
		controlsSelected = true;

		foreach (GameObject uiElement in settingUI) {
			uiElement.SetActive (false);
		}
	}
}
