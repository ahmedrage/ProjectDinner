using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class textTest : MonoBehaviour {

	public float delay = 0.1f;
	public string text;
	string currentText = "";
	public Text dialogueText;

	// Use this for initialization
	void Start () {
	}
	
	IEnumerator showText(){
		for (int i = 0; i < text.Length; i++) {
			currentText = text.Substring (0, i);
			this.GetComponent<Text> ().text = currentText;
			yield return new WaitForSeconds (delay);
		}
	}
		

	public void displayDialogue(int listCount, List<string> dialogue){
		int x = Random.Range (0, listCount);
		text = dialogue [x];
		StartCoroutine (showText());
	}
}
