using UnityEngine;
using System.Collections;

public class cameraShake : MonoBehaviour {
	//public static cameraShake Instance;
	public float _amplitude = 1f;
	public float _duration = 1;
	private Vector3 initialPosition;
	private bool isShaking = false;
	public playerController playerScript;

	// Use this for initialization
	void Start () 
	{	
		playerScript = GameObject.Find ("player").GetComponent<playerController>();
		initialPosition = transform.localPosition;
	}

	public void Shake (float amplitude, float duration)
	{	
		_amplitude = amplitude;
		isShaking = true;
		CancelInvoke ();
		Invoke ("StopShaking", duration);
		playerScript._isShaking = false;
	}

	public void StopShaking()
	{
		isShaking = false;
	}


	// Update is called once per frame
	void Update ()
	{
		if (playerScript._isShaking) {
			Shake(_amplitude,_duration);
		}

		if (isShaking) 
		{
			initialPosition = transform.localPosition; 
			transform.localPosition = initialPosition + Random.insideUnitSphere* _amplitude;
		}
	}
}
