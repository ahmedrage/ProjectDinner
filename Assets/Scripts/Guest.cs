using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	public GuestClass[] GuestArray;
	public bool dead;
	public bool scared;
	public float shakeTime= 3f;
	public float shakeFrequencey = 0.04f;
	public Transform Panel;
	public Transform Portrait;
	public murderSystem murderScript;
	public Vector3 initialPosition;
	public Vector3 currentPosition;
	public GameObject playerPos;

	playerController playerScript;
	Transform deathPanel;

	void Start () {
		playerScript = GameObject.FindWithTag ("Player").GetComponent<playerController> ();
		deathPanel = transform.GetChild (0).transform.GetChild (1);
		Portrait = transform.Find ("Canvas").Find ("Portrait");
		murderScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		initialPosition = transform.localPosition;
		playerPos = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		DisplayText ();
		if (dead) {
			this.gameObject.layer = LayerMask.NameToLayer("deadGuest");
			Physics2D.IgnoreLayerCollision(8,9,true);
		}

		if (scared) {
			Shake ();
		}
	}

	public void setText () {
		Panel = transform.GetChild (0).transform.GetChild (0);
		Panel.transform.GetChild (0).GetComponent<Text> ().text = guestClass.name;
		Panel.transform.GetChild (1).GetComponent<Text> ().text = guestClass.profession.ToString ();

		if (guestClass.Accessory.ToString () == "no_accesories") {
			Panel.transform.GetChild (2).GetComponent<Text> ().text = "Is wearing no accessories";
		}else{

			Panel.transform.GetChild (2).GetComponent<Text> ().text = "Is wearing a" + guestClass.Accessory.ToString ();;
			Panel.transform.GetChild (3).GetComponent<Text> ().text = "Has a" + guestClass.Blemish.ToString ();
		}

		if (guestClass.Accessory.ToString () == "glasses") {
			Panel.transform.GetChild (2).GetComponent<Text> ().text = "Is wearing glasses";
		}
	}

	public void setDeathText (string deathText) {
		deathPanel.GetChild (0).GetComponent<Text> ().text = deathText;
	}

	public void setHintText (string hintText) {
		print (guestClass.name);
		Panel.transform.GetChild (4).GetComponent<Text> ().text = hintText;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && playerScript.nearbyGuests.Contains(transform) == false) {
			playerScript.addGuest (transform);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player" && Panel != null && deathPanel != null) {
			playerScript.removeGuest (transform);
		}
	}

	void DisplayText() {
		if (playerScript.nearestGuest == gameObject) {
			if (dead == false && Panel != null) {
				Panel.gameObject.SetActive (true);
			} else if (dead == true && deathPanel != null) {
				print (dead.ToString ());
				deathPanel.gameObject.SetActive (true);
			}

			if (Portrait != null) {
				Portrait.gameObject.SetActive (true);
			}
		} else {
			if (Panel != null && deathPanel != null) {
				Panel.gameObject.SetActive (false);
				deathPanel.gameObject.SetActive (false);
			}
			if (Portrait != null) {
				Portrait.gameObject.SetActive (false);
			}
		}

	}
	public void Die() {
		dead = true;
		transform.Find ("Graphics").GetComponent<Animator> ().enabled = false;
		transform.Find ("Graphics").GetComponent<SpriteRenderer> ().sprite = guestClass.deadSprite;
	}

	public void Shake(){
		currentPosition = transform.localPosition;
		transform.localPosition = initialPosition + Random.insideUnitSphere * shakeFrequencey;
		StartCoroutine ("coroutineShake");
		if (playerScript.rmb == false) {
			transform.rotation = Quaternion.Slerp (transform.localRotation, Quaternion.identity, Time.deltaTime * 1.5f);
			//transform.rotation = Quaternion.identity;
		}
	}

	IEnumerator coroutineShake(){
		yield return new WaitForSeconds (shakeTime);
		transform.localPosition = initialPosition;
		scared = false;
	}
}
