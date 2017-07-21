using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour {
	//public static cameraShake Instance;
	public float _amplitude = 1f;
	public float _duration = 1;
	public Vector3 initialPosition;
	private Vector3 currentPosition;
	private bool isShaking = false;
	public playerController playerScript;
	// Use this for initialization
	void Start () 
	{	
		if (GameObject.Find ("player") != null) {
			playerScript = GameObject.Find ("player").GetComponent<playerController> ();
		}
		//Instance = this;

		Shake (_amplitude, _duration);
	}

	public void Shake (float amplitude, float duration)
	{	
		initialPosition = transform.localPosition;
		_amplitude = amplitude;
		isShaking = true;
		CancelInvoke ();
		Invoke ("StopShaking", duration);
		transform.position = initialPosition;
	}

	public void StopShaking()
	{
		isShaking = false;
	}


	// Update is called once per frame
	void Update ()
	{
		if (playerScript != null && playerScript.canGuess == true && Input.GetButtonDown("Fire1")) {
			Shake(_amplitude,_duration);
			print ("shake shake");
		}

		if (isShaking) {
			currentPosition = transform.localPosition; 
			transform.localPosition = initialPosition + Random.insideUnitSphere * _amplitude;
		} else {
			transform.localPosition = initialPosition;
		}
	}
}
