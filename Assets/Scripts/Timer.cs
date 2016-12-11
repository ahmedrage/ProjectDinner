using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public Text timerText;
	public float t = 30f;
	public float restartTime = 25f;
	[HideInInspector] public bool restartTimer;
	
	// Update is called once per frame
	void Update () {
		t -= Time.deltaTime; 

		//string minutes = ((int)t / 60).ToString ();
		string seconds = t.ToString("f0");

		timerText.text = "NEXT VICTIM " + seconds;
		if (t <= 0.0f) {
			//print ("done");
			restartTimer = true;
			t = restartTime;
			restartTime -= 5;
		} else {
			restartTimer = false;
		}
	}
}
