using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class interrogationSystem : MonoBehaviour {
	public statManager stats;
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
	public float timeToComplete;
	public Text comfortText;
	public Text threatText;
	public Text reactText;
	public Text timerText;
	public Button comfortButton;
	public Button threatButton;
	public Button accuseButton;
	public Image portrait;
	bool accusing;
	bool canAccuse = false;
	int ind = 0;
	float timeToPrint;
	int timeLeft;
	float initialTime;
	// Use this for initialization
	void Start () {
		portrait = GameObject.Find ("port").GetComponent<Image>();
		stats = GameObject.Find("dataManager").GetComponent<statManager> ();
		if (stats.lastMurderer != null) {
			murderer = stats.lastMurderer;
		} else {
			Debug.LogError ("MURDERER NOT FOUND!");
		}
		portrait.sprite = murderer.interrogationSprites [2];
		GenerateStats ();
		comfortText.text = comfortDialouge [Random.Range (0, comfortDialouge.Length)];
		threatText.text = threatDialouge [Random.Range (0, threatDialouge.Length)];
		initialTime = Time.time;
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
			portrait.sprite = murderer.interrogationSprites [1];
			if (isComfort) {
				reactText.text += positiveComfortDialouge [Random.Range (0, positiveComfortDialouge.Length)];
			} else {
				reactText.text += positiveThreatDialouge [Random.Range (0, positiveThreatDialouge.Length)];
			}
		} else {
			portrait.sprite = murderer.interrogationSprites [0];
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
		timeLeft = Mathf.RoundToInt(timeToComplete - (Time.time - initialTime));
		if (timeLeft <= 0) {
			comfortButton.interactable = false;
			threatButton.interactable = false;
			accuseButton.interactable = false;
		} else {
			timerText.text = timeLeft.ToString();
		}

	}

	void removeText() {
		List<string> text = (reactText.text).Split ('\n').ToList();
		if (text.Count > 8) {
			text.RemoveAt (0);
		}
		reactText.text = string.Join ("\n", text.ToArray());
	}
}
