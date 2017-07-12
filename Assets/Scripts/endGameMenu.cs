using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class endGameMenu : MonoBehaviour {

	public void Play(){
		SceneManager.LoadScene ("GuestScene");

	}

	public void Quit(){
		SceneManager.LoadScene("menuScene");
	}

	public void Stats(){
		SceneManager.LoadScene ("statScene");
	}
}
