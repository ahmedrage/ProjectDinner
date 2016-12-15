using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed;
	public float aimingSpeed;
	public GameObject shot;
	public GameObject muzzleFlash;
	public GameObject hintPannel;
	public Texture2D cursor;
	public Texture2D normalCursor;
	public Transform shotSpawn;
	public Rigidbody2D rb;
	public Sprite gun;
	public SpriteRenderer gunSprite;

	public int ammo = 1;
	public bool canShoot;
	bool playing = false;
	public AudioSource ShootSound;
	public AudioSource Gasp;
	public Animator myAnimator;
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

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ + -90);
		rb.velocity = (movement * speed);

		if (ammo > 0) {
			if (Input.GetButtonDown ("Fire2")) {
				if (playing == false) {
					Gasp.Play ();
				}
				playing = true;
				canShoot = true;
				gunSprite.sprite = gun;
				Cursor.SetCursor (cursor, Vector2.zero, CursorMode.Auto);
				speed = aimingSpeed;
				Gasp.Play ();
			} if(Input.GetButtonUp("Fire2")){
				Cursor.SetCursor (normalCursor,Vector2.zero,CursorMode.Auto);
				playing = false;
				gunSprite.sprite = null;
				canShoot = false;
				speed = 6;
			}

			if (canShoot && Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		}
		if (Input.GetAxis ("Horizontal") == 0 && Input.GetAxis ("Vertical") == 0) {
			myAnimator.SetBool ("Moving", false);
		} else {
			myAnimator.SetBool ("Moving", true);
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
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
		StartCoroutine ("flashDestroy");
	}

	IEnumerator flashDestroy(){
		yield return new WaitForSeconds (0.05f);
		Destroy (GameObject.Find("muzzleFlash(Clone)"));
	}
}