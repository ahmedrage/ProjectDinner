using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	public float speed;
	public float aimingSpeed;
	public GameObject shot;
	public Transform shotSpawn;
	public Rigidbody2D rb;
	//public AudioSource gasp;
	//public AudioSource gunSound;
	public Sprite gun;
	public SpriteRenderer gunSprite;

	int ammo = 1;
	bool canShoot;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		gunSprite = GameObject.Find("gun").GetComponent<SpriteRenderer> ();
		gunSprite.sprite = null;
	}
	
	// Update is called once per frame
	void Update () {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();
		float rotZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
		rb.velocity = (movement * speed);

		if (ammo == 1) {
			if (Input.GetButton ("Fire2")) {
				canShoot = true;
				gunSprite.sprite = gun;
				speed = aimingSpeed;
				//gasp.Play ();
			} if(Input.GetButtonUp("Fire2")){
				gunSprite.sprite = null;
				canShoot = false;
				speed = 6;
			}

			if (canShoot && Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		}
	}

	void Shoot() {
		print ("shot fired");
		canShoot = false;
		ammo = 0;
		gunSprite.sprite = null;
		speed = 6;
		//gunSound.Play ();
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
	}
}