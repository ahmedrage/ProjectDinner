using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class pauseMenu : MonoBehaviour {

	public bool paused;
	public GameObject buttons;
	public Image pauseScreen;
	public playerController playerScript;

	// Use this for initialization
	void Start () {
		playerScript = GameObject.Find ("player").GetComponent<playerController> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (paused == false) {
				pauseUI (true,0,false);
			} else {
				pauseUI (false,1,true);
			}
		}
	}

	void pauseUI(bool condition, int time, bool playerEnable){
		paused = condition;
		pauseScreen.enabled = condition;
		buttons.SetActive (condition);
		Time.timeScale = time;
		playerScript.enabled = playerEnable;

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
