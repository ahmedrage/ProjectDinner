using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using XInputDotNetPure;
using System.Collections;


public class pauseMenu : MonoBehaviour {

	public bool paused;
	public GameObject resumeButton;
	public GameObject pauseButtons;
	public GameObject pauseTitle;
	public Image pauseScreen;
	public playerController playerScript;
	public EventSystem eventSystem;

	bool controllerConnected;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("player").GetComponent<playerController> ();
		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("controllerStart")) {
			if (paused == false) {
				pauseUI (true,0,false);
			} else {
				pauseUI (false,1,true);
			}
		}

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			controllerConnected = true;
		} else {
			controllerConnected = false;
		}
	}
		
	void pauseUI(bool condition, int time, bool playerEnable){
		paused = condition;
		pauseScreen.enabled = condition;
		pauseButtons.SetActive (condition);
		pauseTitle.SetActive (condition);
		Time.timeScale = time;
		playerScript.enabled = playerEnable;

		if (controllerConnected == false) {
			eventSystem.SetSelectedGameObject(null);
		}

		if (controllerConnected && eventSystem.currentSelectedGameObject == null) {
			eventSystem.SetSelectedGameObject (resumeButton);
		} else {
			eventSystem.SetSelectedGameObject(null);
		}
	}

	public void Resume(){
		pauseUI (false, 1,true);
	}

	public void Menu(){
		Time.timeScale = 1;
		SceneManager.LoadScene("menuScene");
	}

	public void Quit(){
		Application.Quit ();
	}
}
