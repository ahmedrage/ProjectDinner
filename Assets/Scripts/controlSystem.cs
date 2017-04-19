using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class controlSystem : MonoBehaviour {

	public finishConditions finishScript;
	public murderSystem murderScript;
	public playerController playerScript;
	public textTest typeWritter;
	public List<string> accusations;
	public List<string> promises;
	public List<string> calming;
	public Image responseImg;
	public Image calmnessBar;
	public Sprite playerPortraitSprite;
	public Sprite acussingGuestSprite;
	public float calmness = 100;
	public float initialCalmness = 100;
	public float promiseBonus;
	public float calmBonus;
	public float promiseMod;
	public float controldialogueWaitTime;
	public bool accused;
	public GameObject dialoguePannel;

	[HideInInspector]public float negativePromiseMod;


	// Use this for initialization
	void Start () {
		negativePromiseMod = 1;
		finishScript = GetComponent<finishConditions> ();
		murderScript = GetComponent<murderSystem> ();
		typeWritter = GameObject.Find ("dialogueTxt").GetComponent<textTest> ();
		playerScript = GameObject.Find ("player").GetComponent<playerController> ();
		accused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Floor(calmness) <= 0) {
			finishScript.Lose ();
		}

		calmnessBar.fillAmount = calmness/initialCalmness;
	}

	public void accuse(){
		accused = true;
		responseImg.enabled = true;
		int i = Random.Range (0, murderScript.guests.Count);
		GameObject accuser = murderScript.guests [i];
		acussingGuestSprite = accuser.GetComponent<Guest> ().guestClass.Portrait;
		responseImg.sprite = acussingGuestSprite;
		typeWritter.displayDialogue (accusations.Count,accusations);
		// fade in response
	}

	public void response(int listCount, List<string> responses , bool promise){
		responseImg.enabled = true;
		responseImg.sprite = playerPortraitSprite;
		accused = false;
		typeWritter.displayDialogue (listCount, responses);
		StartCoroutine ("playerHalt");

		if (promise) {
			calmness += promiseBonus;
			negativePromiseMod = promiseMod;
		} else { 
			calmness += calmBonus;
			negativePromiseMod = 1;
		}
		// fade response away
	}

	IEnumerator playerHalt(){
		yield return new WaitForSeconds (controldialogueWaitTime);
		playerScript.speed = 6;
	}
}
