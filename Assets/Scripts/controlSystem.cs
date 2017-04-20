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
	public Text instruction;
	public float calmness = 100;
	public float initialCalmness = 100;
	public float promiseBonus;
	public float calmBonus;
	public float promiseMod;
	public float controldialogueWaitTime;
	public bool accused;
	public GameObject dialoguePannel;
	public GameObject accuser;

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
		instruction.color = Color.white;
		accused = true;
		responseImg.enabled = true;
		int i = Random.Range (0, murderScript.guests.Count);
		accuser = murderScript.guests [i];
		acussingGuestSprite = accuser.GetComponent<Guest> ().guestClass.Portrait;
		responseImg.sprite = acussingGuestSprite;
		responseImg.color = Color.white;
		typeWritter.displayDialogue (accusations.Count,accusations);
	}

	public void response(int listCount, List<string> responses , bool promise){
		responseImg.enabled = true;
		responseImg.sprite = playerPortraitSprite;
		accused = false;
		responseImg.color = Color.white;
		typeWritter.displayDialogue (listCount, responses);
		StartCoroutine ("playerHalt");

		if (promise) {
			calmness += promiseBonus;
			negativePromiseMod = promiseMod;
		} else { 
			calmness += calmBonus;
			negativePromiseMod = 1;
		}

	}

	IEnumerator playerHalt(){
		yield return new WaitForSeconds (controldialogueWaitTime);
		playerScript.speed = 6;
		playerScript.myAnimator.SetBool ("Moving", true);
		yield return new WaitForSeconds (2.5f);
		StartCoroutine (Fade (Color.white, Color.clear, 1));
	}

	IEnumerator Fade(Color A, Color B, float time)
	{
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) 
		{
			percent += Time.deltaTime * speed;
			typeWritter.dialogueText.color = Color.Lerp (A, B, percent);
			responseImg.color = Color.Lerp (A, B, percent);
			instruction.color = Color.Lerp (A, B, percent);
			yield return null;
		}
	}
}
