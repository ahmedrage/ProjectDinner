﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public Text instructText;
	public Text instructTextController;
	public Text instructTextMouse;
	public Text clickToContinue;
	public List<GameObject> menuElements;
	public Texture2D normalCursor;
	public EventSystem eventSystem;
		
	bool canContinue;
	bool controllerConnected;

	void Start(){
		Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
		eventSystem = GetComponent<EventSystem> ();

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			eventSystem.firstSelectedGameObject = menuElements [1];
		}
	}

	public void Update(){
		
		if (canContinue) {
			clickToContinue.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong(Time.time,1.5f));

			if (Input.GetButtonDown ("Fire1")) {
				SceneManager.LoadScene ("GuestScene");
			}
		}

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			controllerConnected = true;
			instructText.text = instructTextController.text;
			clickToContinue.text = "Press A to continue";
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			controllerConnected = false;
			instructText.text = instructTextMouse.text;
			clickToContinue.text = "Click to continue";
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		if (controllerConnected == false) {
			eventSystem.SetSelectedGameObject(null);
		}

		if (controllerConnected && eventSystem.currentSelectedGameObject == null) {
			eventSystem.SetSelectedGameObject (menuElements [1]);
		}
	}

	public void Play(){
		OnPromptFade (); 
	}

	public void Quit(){
		Application.Quit ();
	}

	void OnPromptFade(){
		StartCoroutine (promptFade (Color.clear, Color.white, 1));

		foreach (GameObject uiElement in menuElements) {
			Destroy (uiElement);	
		}
	}

	IEnumerator promptFade(Color A, Color B, float time){

		float fadeInSpeed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * fadeInSpeed;
			instructText.color = Color.Lerp (A, B, percent);
			yield return null;
		}

		if (percent >= 1) {
			StartCoroutine ("continueDelay");
		}
	}

	IEnumerator continueDelay(){
		yield return new WaitForSeconds (2f);
		canContinue = true;
	}
}
