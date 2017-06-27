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
	public Slider[] volumeSliders;

	[HideInInspector] public bool controlsSelected;


	// Use this for initialization
	void Start () {
		volumeSliders [0].value = audioManager.instance.masterLevel;
		volumeSliders [1].value = audioManager.instance.sfxLevel;
		volumeSliders [2].value = audioManager.instance.musicLevel;

		bool isFullscreen = (PlayerPrefs.GetInt ("isFullscreen") == 1) ? true : false;
	}
	
	// Update is called once per frame
	void Update () {
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

	public void MasterVolume(float value){
		audioManager.instance.setVolume (value, audioManager.AudioChannel.Master);
	}

	public void MusicVolume(float value){
		audioManager.instance.setVolume (value, audioManager.AudioChannel.Music);
	}

	public void SFXVolume(float value){
		audioManager.instance.setVolume (value, audioManager.AudioChannel.Sfx);
	}

	public void fullscreenToggle(bool isFullscreen){
		if (isFullscreen) {
			Screen.fullScreen = true;
			print ("fullscreen");
		} else {
			Screen.fullScreen = false;
			print ("not full screen");
		}
		PlayerPrefs.SetInt ("isFullscreen", (fullscreen.isOn) ? 1 : 0);
		PlayerPrefs.Save ();
	}
}
