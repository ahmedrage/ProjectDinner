﻿using UnityEngine;
using XInputDotNetPure;
using System.Collections;
using System.Collections.Generic;
public class playerController : MonoBehaviour {

	public float speed;
	public float knockoutSpeed;
	public GameObject controllerCrosshair;
	public GameObject hintPannel;
	public Texture2D cursor;
	public Texture2D normalCursor;
	public Transform shotSpawn;
	public Rigidbody2D rb;
	public Sprite gun;
	public Sprite controllerNormalCursor;
	public Sprite controllerCursor;
	public murderSystem murderSys;
	public finishConditions finishScript;
	public controlSystem controlSys;
	public SpriteRenderer gunSprite;
	public Transform endPosition;
	public Transform startPosition;
	public int initialGuess = 2;
	public int guesses = 2;
	public bool canGuess; 
	public bool _isShaking;
	public AudioClip blugeSound;
	public Animator myAnimator;

	public List<Transform> nearbyGuests; // This array holds all the guests who are close enough to display details.

	public GameObject nearestGuest;
	//bool playing = false;
	bool usingController;
	float rotZ = 90;
	Sprite cursorSprite;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		finishScript = GameObject.Find ("Gm").GetComponent<finishConditions> ();
		controlSys = GameObject.Find ("Gm").GetComponent<controlSystem> ();
		murderSys = GameObject.Find ("Gm").GetComponent<murderSystem> ();
		gunSprite = GameObject.Find("gun").GetComponent<SpriteRenderer> ();
		gunSprite.sprite = null;
		Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
		_isShaking = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkGuests ();
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		float moveVertical = Input.GetAxisRaw ("Vertical");
		float rHoriz = Input.GetAxisRaw ("joyHoriz");
		float rVert = Input.GetAxisRaw ("joyVert");
		float rightTrigger = Input.GetAxisRaw ("rightTrigger");
		float leftTrigger = Input.GetAxisRaw ("leftTrigger");

		if (GamePad.GetState (PlayerIndex.One).IsConnected) {
			usingController = true;
		} else {
			usingController = false;
		}

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rb.velocity = (movement * speed);

		if(usingController){
			Vector2 playerDirection = Vector2.right * rHoriz + -Vector2.up * rVert;
			playerDirection.Normalize ();

			if (playerDirection.sqrMagnitude > 0.0f) {
				rotZ = Mathf.Atan2 (playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;	
				transform.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);
			} else {
				transform.rotation = Quaternion.Euler (0f, 0f, rotZ - 90);
			}

			controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = controllerNormalCursor;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

		} else {
			Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			difference.Normalize ();
			float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;	
			transform.rotation = Quaternion.Euler (0f, 0f, rotZ + -90);
			controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = null;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;		
		}

		if (guesses > 0) {
			if (Input.GetButtonDown ("Fire2") || leftTrigger == 1 && usingController) {
				/*if (playing == false) {
					//Gasp.Play ();
					//audioManager.instance.playSound(Gasp,Vector2.zero);
				}
				playing = true;*/
				canGuess = true;
				gunSprite.sprite = gun;

				if (usingController) {
					controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = controllerCursor;
				}
				Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
				speed = knockoutSpeed;
			} 

			if(Input.GetButtonUp("Fire2") || leftTrigger == 0 && usingController){
				Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
				//playing = false;
				gunSprite.sprite = null;

				if (usingController) {
					controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = controllerNormalCursor;
				}
				canGuess = false;
				speed = 6;
			}

			if (canGuess && (Input.GetButtonDown ("Fire1") || rightTrigger == 1 && usingController)) {
				knockOut ();
			}
		}
		if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0) {
			myAnimator.SetBool ("Moving", false);
		} else {
			myAnimator.SetBool ("Moving", true);
		}

		if (Input.GetKeyDown (KeyCode.LeftShift) || Input.GetButtonDown("controllerBack")) {
			hintPannel.SetActive (!(hintPannel.activeSelf));
		}

		if (controlSys.accused) {
			if (Input.GetKeyDown(KeyCode.Q)) {
				speed = 0;
				controlSys.response (controlSys.promises.Count, controlSys.promises, true);
			}

			if (Input.GetKeyDown (KeyCode.E)) {
				speed = 0;
				controlSys.response (controlSys.calming.Count, controlSys.calming, false);
			}
		}
	}
		
	void knockOut() {
		RaycastHit2D hit = Physics2D.Linecast(startPosition.position,endPosition.position);
		if (hit != null && hit.collider != null) {

			if (hit.collider.gameObject != murderSys.murderer) { // incorrect guess
				_isShaking = true;
				audioManager.instance.playSound(blugeSound,Vector2.zero);
				guesses--; // decrease calm
				controlSys.calmness -= (controlSys.initialCalmness/ initialGuess) * controlSys.negativePromiseMod;
			}

			if (hit.collider.gameObject == murderSys.murderer) {
				_isShaking = true;
				audioManager.instance.playSound(blugeSound,Vector2.zero);
				canGuess = false;
				finishScript.Win ();
			}
		}

		gunSprite.sprite = null;
		speed = 6;
		GamePad.SetVibration (PlayerIndex.One, 10, 10);
		StartCoroutine ("vibrationTime");
	}

	//This function chooses one guests details to present
	void checkGuests() {
		float closestDist = 10;

		foreach (Transform nearbyGuest in nearbyGuests) {
			if (Vector3.Distance(transform.position, nearbyGuest.position) < closestDist){
				nearestGuest = nearbyGuest.gameObject;
				closestDist = Vector3.Distance (transform.position, nearbyGuest.position);
			}
		}
		if (closestDist == 10) {
			nearestGuest = null;
		}
	}

	public void removeGuest (Transform guest) {
		if (nearbyGuests.Contains (guest)) {
			nearbyGuests.Remove (guest);
		} else {
			//print ("Guest to remove is not in list");
		}
	}

	public void addGuest (Transform guest) {
		if (guest != null) {
			nearbyGuests.Add (guest);
		} else {
			//print ("Guest to add does not exist!");
		}
	}

	IEnumerator vibrationTime()
	{
		yield return new WaitForSeconds (0.1f);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}
}