using UnityEngine;
using System.Collections;

public class shotMover : MonoBehaviour {

	public float speed;
	public float timeTillDestroy;
	public string shotGuestName;
	public murderSystem script;


	// Use this for initialization
	void Start () {
		script = GameObject.Find ("Gm").GetComponent<murderSystem> ();
	}
	
	// Update is called once per frame
	void Awake () {
		StartCoroutine ("destroyDelay");
	}

	void Update(){
		transform.Translate (Vector2.right * speed);
	}

	void OnCollisionEnter2D (Collision2D other){
		if (other.gameObject.tag == "Guest") {
			Destroy (other.gameObject);
			Destroy (gameObject);
			print (shotGuestName);

			if (other.gameObject == script.murderer) {
				print ("you killed the murderer, you win.");
			} else {
				print("that wasn't the killer, you lose,");
			}
		}else{
			Destroy (gameObject);
		}
	}

	IEnumerator destroyDelay(){
		yield return new WaitForSeconds (timeTillDestroy);
		Destroy (gameObject);
	}
}
