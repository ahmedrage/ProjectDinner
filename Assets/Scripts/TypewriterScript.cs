using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TypewriterScript : MonoBehaviour {

	public float delay = 0.1f;
	public string text;
	string currentText = "";
	public Text dialogueText;

	IEnumerator showText () {
		for (int i = 0; i <= text.Length; i++) {
			currentText = text.Substring (0, i);
			this.GetComponent<Text> ().text = currentText;
			yield return new WaitForSeconds (delay);

			if (i == text.Length) {
				print ("ended stooge");
			}
		}
	}

	public void displayRandomDialogue (int listCount, List<string> dialogue) {
		dialogueText.color = Color.white;
		int x = Random.Range (0, listCount);
		text = dialogue [x];
		StartCoroutine (showText());
	}

	public IEnumerator displayDialogueInOrder (List<string> dialogue) {
		dialogueText.color = Color.white;
		foreach (string tx in dialogue) {
			text = tx;
			yield return StartCoroutine (showText());
		}
	}
}
