using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour {

	public Text instructText;
	public Text clickToContinue;
	public List<GameObject> menuElements;
	public Texture2D normalCursor;
		
	bool canContinue;

	void Start(){
		Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
	}

	public void Update(){
		
		if (canContinue) {
			clickToContinue.color = Color.Lerp (Color.clear, Color.white, Mathf.PingPong(Time.time,1.5f));

			if (Input.GetButtonDown ("Fire1")) {
				SceneManager.LoadScene ("GuestScene");
			}
		}
	}

	public void Play(){
		OnPromptFade (); 
	}

	public void Quit(){
		Application.Quit ();
	}

	void OnPromptFade(){
		StartCoroutine (promptFade (Color.clear, Color.white, 1));

		foreach (GameObject uiElement in menuElements) {
			Destroy (uiElement);	
		}
	}

	IEnumerator promptFade(Color A, Color B, float time){

		float fadeInSpeed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * fadeInSpeed;
			instructText.color = Color.Lerp (A, B, percent);
			yield return null;
		}

		if (percent >= 1) {
			StartCoroutine ("continueDelay");
		}
	}

	IEnumerator continueDelay(){
		yield return new WaitForSeconds (2f);
		canContinue = true;
	}
}
