using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicDialogue : MonoBehaviour {

	public GameObject DialogueBox;
	public List<string> dialogues;

	void Start() {
		StartCoroutine(DialogueBox.gameObject.GetComponent<TypewriterScript> ().displayDialogueInOrder (dialogues));
	}

}
