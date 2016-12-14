using UnityEngine;
using System.Collections;

public class shotMover : MonoBehaviour {

	public float speed;
	public float timeTillDestroy;
	public string shotGuestName;
	public murderSystem script;
	public finishConditions finishScript;


	// Use this for initialization
	void Start () {
		script = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		finishScript =  GameObject.Find ("Gm").GetComponent<finishConditions> ();
	}
	
	// Update is called once per frame
	void Awake () {
		StartCoroutine ("destroyDelay");
	}

	void Update(){
		transform.Translate (Vector2.up * speed);
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "guest") {
			other.gameObject.GetComponent<Guest> ().Die ();
			Destroy (this.gameObject);

			if (other.gameObject.tag == "guest" && other.gameObject != script.murderer) {
				script.deadPortraits.Add(other.gameObject.GetComponent<Guest> ().guestClass.Portrait);
				script.deadGuests++;
				finishScript.loseCondition = 1;
				finishScript.Lose ();
				Destroy (this.gameObject);
			}
		} else {
			Destroy (this.gameObject);
			finishScript.loseCondition = 3;
			finishScript.Lose ();
		}
	}

	IEnumerator destroyDelay(){
		yield return new WaitForSeconds (timeTillDestroy);
		Destroy (gameObject);
	}
}
