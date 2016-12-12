using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {

	public Text timerText;
	public murderSystem murdererScript;
	public finishConditions finishScript;
	public float t = 30f;
	public float restartTime = 25f;

	// Update is called once per frame
	void Start(){
		murdererScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		finishScript =  GameObject.Find ("Gm").GetComponent<finishConditions> ();

	}

	void Update () {
		t -= Time.deltaTime; 

		//string minutes = ((int)t / 60).ToString ();
		string seconds = t.ToString("f0");

		timerText.text = "NEXT VICTIM " + seconds;
		if (t <= 0.0f) {
			//print ("done");
			t = restartTime;
			restartTime -= 5;
			murdererScript.KillGuest ();
		} 

		if (restartTime == 0 || finishScript.win || finishScript.lose) {
			Destroy (this);
		}
	}
}
