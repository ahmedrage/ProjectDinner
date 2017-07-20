using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicSceneSwitch : MonoBehaviour {

	public int SceneIndex;

	public void Transition () {
		SceneManager.LoadScene (SceneIndex);
	}
}
