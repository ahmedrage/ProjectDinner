using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TypewriterScript : MonoBehaviour {

	public float delay = 0.1f;
	public string text;
	string currentText = "";
	public Text dialogueText;

	private bool spaceBarDown = false;
	private bool completeLineInstant = false;

	bool display;
	IEnumerator WaitForKey()
	{
		while (!display) {
			yield return null;
		}
	}

	void Update () {
		if ((Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) && dialogueText.text.Length >= text.Length) {
			display = true;
		} else if ((Input.GetButtonDown ("Fire1") || Input.GetButtonDown ("Jump")) && dialogueText.text.Length < text.Length) {
			completeLineInstant = true;
		}
	}

	public IEnumerator showText () {
		display = false;
		for (int i = 0; i <= text.Length; i++) {
			currentText = text.Substring (0, i);
			this.GetComponent<Text> ().text = currentText;
			yield return new WaitForSeconds (delay);
			if (completeLineInstant) {
				i = text.Length;
				this.GetComponent<Text> ().text = text;
				completeLineInstant = false;
			}
			if (i == text.Length) {
				display = false;
				yield return StartCoroutine (WaitForKey ());
				text = "";
				dialogueText.text = text;
			}
		}
	}

	public void PrintLine (string line) {
		display = false;
		text = line;
		StartCoroutine (showText ());
	}

	public void displayRandomDialogue (int listCount, List<string> dialogue) {
		dialogueText.color = Color.white;
		int x = Random.Range (0, listCount);
		text = dialogue [x];
		StartCoroutine (showText());
	}

	public IEnumerator displayDialogueInOrder (List<string> dialogue) {
		display = false;
		dialogueText.color = Color.white;
		//dialogue.Add ("");
		foreach (string tx in dialogue) {
			text = tx;
			yield return StartCoroutine (showText ());
		}
	}
}
