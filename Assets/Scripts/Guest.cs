using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Guest : MonoBehaviour {

	// Use this for initialization
	public GuestClass guestClass;
	public GuestClass[] GuestArray;
	public bool dead;
	public bool arrested;
	public bool scared;
	public bool threat;
	public float shakeTime= 3f;
	public float shakeFrequencey = 0.04f;
	public Transform Panel;
	public Transform Portrait;
	public murderSystem murderScript;
	public Vector3 initialPosition;
	public Vector3 currentPosition;
	public GameObject playerPos;
	public GameObject graphics;
	public Vector3 initialRotataion;

	playerController playerScript;
	Transform deathPanel;

	void Start () {
		playerScript = GameObject.FindWithTag ("Player").GetComponent<playerController> ();
		deathPanel = transform.GetChild (0).transform.GetChild (1);
		Portrait = transform.Find ("Canvas").Find ("Portrait");
		murderScript = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		initialPosition = transform.Find ("Graphics").GetComponent<Transform> ().localPosition;
		playerPos = GameObject.FindWithTag ("Player");
		initialRotataion =  transform.Find("Graphics").GetComponent<Transform>().rotation.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		DisplayText ();
		if (dead || arrested) {
			this.gameObject.layer = LayerMask.NameToLayer("deadGuest");
			graphics.layer = LayerMask.NameToLayer ("deadGuest");
			Physics2D.IgnoreLayerCollision(8,9,true);
			Portrait.gameObject.SetActive (false);
		}

		if (scared && graphics.layer == LayerMask.NameToLayer("Guest") && gameObject.layer == LayerMask.NameToLayer("Guest") ) {
			Shake ();
		}

		Vector2 directionToPlayer = (playerPos.transform.position - transform.localPosition);
		if (graphics.layer == LayerMask.NameToLayer("Guest") && gameObject.layer == LayerMask.NameToLayer("Guest") && playerScript.rmb == false || directionToPlayer.magnitude > 4f && graphics.layer == LayerMask.NameToLayer("Guest") && gameObject.layer == LayerMask.NameToLayer("Guest")) { // maximum magnitude should be a variabe, but I dont give a fuck.
			SlerpRot ();		
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
			if (arrested == true) {
				Panel.gameObject.SetActive (false);
				deathPanel.gameObject.SetActive (false);
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

		if (GameObject.FindGameObjectsWithTag ("Room") != null) {
			float smallestDistance = Mathf.Infinity;
			GameObject nearestRoom = gameObject;
			foreach (var room in GameObject.FindGameObjectsWithTag ("Room")) {
				room.GetComponent<roomScript> ().recentDead = false;
				if (Vector3.Distance (transform.position, room.transform.position) < smallestDistance) {
					smallestDistance = Vector3.Distance (transform.position, room.transform.position);
					nearestRoom = room;
				}
			}
			if (nearestRoom != null) {
				nearestRoom.GetComponent<roomScript> ().recentDead = true;
			}
		}
	}

	public void arrest(){
		arrested = true;
		this.gameObject.layer = LayerMask.NameToLayer("deadGuest");
		deathPanel.gameObject.SetActive (false);
		transform.Find ("Graphics").GetComponent<Animator> ().enabled = false;
		transform.Find ("Graphics").GetComponent<SpriteRenderer> ().sprite = guestClass.deadSprite; // need to change to dead sprite with no blood
	}

	public void Rotate(){
		Vector2 directionToPlayer = (playerPos.transform.position - transform.localPosition).normalized;
		Debug.DrawRay (transform.position, directionToPlayer, Color.red);
		Transform graphicTransform = transform.Find("Graphics").GetComponent<Transform>();
		Debug.DrawRay(transform.position,graphicTransform.up,Color.white);
		float rotZ = Mathf.Atan2 (directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;	
		graphicTransform.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);
	}

	public void SlerpRot(){
		Transform graphicTransform = transform.Find("Graphics").GetComponent<Transform>();
		graphicTransform.rotation = Quaternion.Slerp (graphicTransform.localRotation, Quaternion.Euler(initialRotataion), Time.deltaTime * 1.5f);
		threat = false;
	}

	public void Shake(){
		currentPosition = transform.Find("Graphics").GetComponent<Transform>().localPosition;
		transform.Find("Graphics").GetComponent<Transform>().localPosition = initialPosition + Random.insideUnitSphere * shakeFrequencey;
		if (threat == false) {
			StartCoroutine ("coroutineShake");
		}
	}

	IEnumerator coroutineShake(){
		yield return new WaitForSeconds (shakeTime);
		transform.Find("Graphics").GetComponent<Transform>().localPosition = initialPosition;
		scared = false;
	}
}
