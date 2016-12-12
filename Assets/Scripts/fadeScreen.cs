using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class fadeScreen : MonoBehaviour {

	public Image Fadecolor;
	public finishConditions finishScript;
	public float speed;

	// Use this for initialization
	void Start () 
	{
		finishScript =  GameObject.Find ("Gm").GetComponent<finishConditions> ();
		Fadecolor = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (finishScript.win) {
			OnFade ();
		}

		if (finishScript.lose) {
			StartCoroutine ("endGame");
			OnFade ();
		}
	}

	void OnFade()
	{
		StartCoroutine (Fade (Color.clear, Color.black, 1));
	}

	IEnumerator Fade(Color A, Color B, float time)
	{
		float speed = 1 / time;
		float percent = 0;
	
		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			Fadecolor.color = Color.Lerp (A, B, percent);
			yield return null;
		}

		if (percent >= 1) {
			StartCoroutine ("respawnDelay");
		}
	}

	IEnumerator respawnDelay()
	{
		yield return new WaitForSeconds (0.3f);
		//Application.LoadLevel (Application.loadedLevel);
	}

	IEnumerator endGame()
	{
		yield return new WaitForSeconds (1f);
		//SceneManager.LoadScene ("menuScene");
	}
}
