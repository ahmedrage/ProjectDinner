using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interrogationSystem : MonoBehaviour {
	public GuestClass murderer;
	public int fear;
	public int comfort;
	public int minDifference = 50;
	public int upperInitial = 80;
	public int lowerInitial = 20;
	public int interactionValue = 10;
	public string[] positiveComfortDialouge;
	public string[] negativeComfortDialouge;
	public string[] positiveThreatDialouge;
	public string[] negativeThreatDialouge;
	public string[] threatDialouge;
	public string[] comfortDialouge;
	public string[] finalDialouge;
	public float waitTime;
	public Text comfortText;
	public Text threatText;
	public Text reactText;
	public Button comfortButton;
	public Button threatButton;
	public Button accuseButton;
	bool accusing;
	bool canAccuse = false;
	int ind = 0;
	float timeToPrint;
	// Use this for initialization
	void Start () {
		GenerateStats ();
		comfortText.text = comfortDialouge [Random.Range (0, comfortDialouge.Length)];
		threatText.text = threatDialouge [Random.Range (0, threatDialouge.Length)];
	}
	void GenerateStats() {
		fear = Random.Range (lowerInitial, upperInitial);
		int difference = Random.Range (minDifference, 90 - fear);
		if (fear - difference > 0) {
			comfort = fear - difference;
		} else if (fear + difference <= 100) {
			comfort = fear + difference;
		} else {
			print ("Difference is too high" + difference.ToString());
		}
	}

	public void Interact(bool isComfort) {
		int initialDifference = fear - comfort;
		if (isComfort) {
			comfort += 10;
			fear -= 5;
			comfortText.text = comfortDialouge [Random.Range (0, comfortDialouge.Length)];
		} else {
			fear += 10;
			comfort += 5;
			threatText.text = threatDialouge [Random.Range (0, threatDialouge.Length)];

		}
		if (Mathf.Abs (initialDifference) > Mathf.Abs (fear - comfort)) { 
			React (true, isComfort);
			print ("Test");
		} else {
			React (false, isComfort);
			print ("Test1");
		}
	}
	public void React(bool isPositive, bool isComfort) {
		reactText.text+= @"
";
		//playanimation
		if (isPositive) {
			if (isComfort) {
				reactText.text += positiveComfortDialouge [Random.Range (0, positiveComfortDialouge.Length)];
			} else {
				reactText.text += positiveThreatDialouge [Random.Range (0, positiveThreatDialouge.Length)];
			}
		} else {
			if (isComfort) {
				reactText.text += negativeComfortDialouge [Random.Range (0, negativeComfortDialouge.Length)];
			} else {
				reactText.text += negativeThreatDialouge [Random.Range (0, negativeThreatDialouge.Length)];
			}
		}
		removeText ();
	}

	public void Accuse () {
		if (canAccuse) {
			//display dialouge
			//play some animation I guess
			//then move to next season
			React(true,false);
			comfortButton.interactable = false;
			threatButton.interactable = false;
			accuseButton.interactable = false;
			accusing = true;
		} else {
			React (false, false);
		}
	}
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (comfort - fear) <= 10) {
			canAccuse = true;
		}
		if (fear >= 100 || comfort >= 100) {
			GenerateStats ();
		}

		if (accusing == true && timeToPrint < Time.time && ind <= finalDialouge.Length -1) {
			reactText.text += @"
" +finalDialouge [ind];
			ind++;
			timeToPrint = Time.time + waitTime;
			removeText ();
		} else if (finalDialouge.Length - 1 > ind) {
			//next scene
		}
	}

	void removeText() {
		List<string> text = (reactText.text).Split ('\n').ToList();
		if (text.Count > 9) {
			text.RemoveAt (0);
		}
		reactText.text = string.Join ("\n", text.ToArray());
	}
}
