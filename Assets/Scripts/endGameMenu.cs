using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endGameMenu : MonoBehaviour {
	public string nextScene;
	public finishConditions finishScript;
	public murderSystem murderSys;
	public statManager _statManager;
	public void Play(){
		finishScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<finishConditions>();
		murderSys = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		_statManager = GameObject.Find ("dataManager").GetComponent<statManager> ();
		if (finishScript.win == true) {
			for (int i = 0; i < murderSys.deadPortraits.Count; i++) {
				_statManager.deadGuestsPorts.Add (murderSys.deadPortraits [i]);
			}
			_statManager.murderers.Add (murderSys.murdererPortrait);
			_statManager.murdererNames.Add (murderSys.murdererName);
			_statManager.savedGuests = murderSys.guests.Count;
			SceneManager.LoadScene (nextScene);
		} else {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}

	public void Quit(){
		SceneManager.LoadScene("menuScene");
	}

	public void Stats(){
		SceneManager.LoadScene ("statScene");
	}
}
