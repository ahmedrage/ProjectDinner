using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endGameMenu : MonoBehaviour {
	public string nextScene;
	public finishConditions finishScript;
	public void Play(){
		finishScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<finishConditions>();
		if (finishScript.win == true) {
			SceneManager.LoadScene (nextScene);
		} else {
			SceneManager.LoadScene ("GuestScene");
		}
	}

	public void Quit(){
		SceneManager.LoadScene("menuScene");
	}
}
