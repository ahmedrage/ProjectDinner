using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDialogue : MonoBehaviour {

	public GameObject DialogueBox;
	public List<string> dialogues;

	void Start() {
		StartCoroutine (RunText ());
	}

	IEnumerator RunText () {
		yield return StartCoroutine(DialogueBox.gameObject.GetComponent<TypewriterScript> ().displayDialogueInOrder (dialogues));
		GameObject.Find ("SceneSwitcher").GetComponent<CinematicSceneSwitch> ().Transition ();
	}

}
