using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed;
	public float aimingSpeed;
	public GameObject shot;
	public GameObject controllerCrosshair;
	public GameObject muzzleFlash;
	public GameObject hintPannel;
	public Texture2D cursor;
	public Texture2D normalCursor;
	public Transform shotSpawn;
	public Rigidbody2D rb;
	public Sprite gun;
	public Sprite controllerNormalCursor;
	public Sprite controllerCursor;
	public SpriteRenderer gunSprite;
	public int ammo = 1;
	public bool canShoot;
	public AudioSource ShootSound;
	public AudioSource Gasp;
	public Animator myAnimator;

	bool playing = false;
	bool usingController;
	float rotZ = 90;
	Sprite cursorSprite;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		gunSprite = GameObject.Find("gun").GetComponent<SpriteRenderer> ();
		gunSprite.sprite = null;
		Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
	}
	
	// Update is called once per frame
	void Update () {
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

		if (ammo > 0) {
			if (Input.GetButtonDown ("Fire2") || leftTrigger == 1 && usingController) {
				if (playing == false) {
					Gasp.Play ();
				}
				playing = true;
				canShoot = true;
				gunSprite.sprite = gun;

				if (usingController) {
					controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = controllerCursor;
				}
				Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
				speed = aimingSpeed;
				Gasp.Play ();
			} 

			if(Input.GetButtonUp("Fire2") || leftTrigger == 0 && usingController){
				Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
				playing = false;
				gunSprite.sprite = null;

				if (usingController) {
					controllerCrosshair.GetComponent<SpriteRenderer> ().sprite = controllerNormalCursor;
				}
				canShoot = false;
				speed = 6;
			}

			if (canShoot && (Input.GetButtonDown ("Fire1") || rightTrigger == 1 && usingController)) {
				Shoot ();
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
	}
		
	void Shoot() {
		ShootSound.Play ();
		print ("shot fired");
		ammo--;
		if (ammo <= 0) {
			canShoot = false;
		}
		gunSprite.sprite = null;
		speed = 6;
		//gunSound.Play ();
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		Instantiate (muzzleFlash, shotSpawn.position, shotSpawn.rotation);
		GamePad.SetVibration (PlayerIndex.One, 10, 10);
		StartCoroutine ("vibrationTime");
		StartCoroutine ("flashDestroy");
	}

	IEnumerator flashDestroy(){
		yield return new WaitForSeconds (0.05f);
		Destroy (GameObject.Find("muzzleFlash(Clone)"));
	}

	IEnumerator vibrationTime()
	{
		yield return new WaitForSeconds (0.1f);
		GamePad.SetVibration (PlayerIndex.One, 0, 0);
	}
}