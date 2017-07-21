using UnityEngine;
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
	public Text optionTitle;
	public Text optionInstructionText;
	public List<GameObject> menuElements;
	public List<GameObject> optionElements;
	public Texture2D normalCursor;
	public EventSystem eventSystem;
		
	bool canContinue;
	bool controllerConnected;
	settings settingScript;

	void Start(){
		Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
		eventSystem = GetComponent<EventSystem> ();
		settingScript = GameObject.Find ("optionsMenu").GetComponent<settings> ();

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			eventSystem.firstSelectedGameObject = menuElements [1];
		}
	}

	public void Update(){
		
		if (canContinue) {
			clickToContinue.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong(Time.time,1.5f));

			if (Input.GetButtonDown ("Fire1")) {
				SceneManager.LoadScene ("Openinc");
			}
		}

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			controllerConnected = true;
			instructText.text = instructTextController.text; // this should be its own function 
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

		if (settingScript.controlsSelected == true && controllerConnected == true) {
			eventSystem.SetSelectedGameObject (optionElements [1]);
			print("should select the fucking back button you cunt");
		}
	}

	public void Play(){
		OnPromptFade ();
	}

	public void Option(){
		uiHandler (false, true);
		eventSystem.SetSelectedGameObject (optionElements[2]);
	}

	public void Quit(){
		Application.Quit ();
	}
		
	public void Back(){

		if (settingScript.controlsSelected) {
			foreach (GameObject optionElement in optionElements) {
				optionElement.SetActive (true);
				optionInstructionText.enabled = false;
			}
			optionTitle.text = "OPTIONS";
			settingScript.controlsSelected = false;
			print ("back to menu");
			eventSystem.SetSelectedGameObject (optionElements [2]);
		} else {
			uiHandler (true, false);
			print ("back to main menu");
			eventSystem.SetSelectedGameObject (menuElements [1]);
		}
	}

	void uiHandler(bool menuUi, bool optionUi){
		
		foreach(GameObject uiElement in menuElements){
			uiElement.SetActive (menuUi);
		}

		foreach (GameObject optionElement in optionElements) {
			optionElement.SetActive (optionUi);
		}
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
