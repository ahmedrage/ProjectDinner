using UnityEngine;
using System.Collections;

public class shotMover : MonoBehaviour {

	public float speed;
	public float timeTillDestroy;


	// Use this for initialization
	void Start () {
	
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
			//change sprite to dead guest
		}else{
			Destroy (gameObject);
		}
	}

	IEnumerator destroyDelay(){
		yield return new WaitForSeconds (timeTillDestroy);
		Destroy (gameObject);
	}
}
