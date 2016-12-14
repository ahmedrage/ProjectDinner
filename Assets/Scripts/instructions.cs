using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class instructions : MonoBehaviour {

	public Text instructText;
	public menu menuScript;
	public float fadeInSpeed;

	// Use this for initialization
	void Start () {

		menuScript = GameObject.Find ("menu").GetComponent<menu> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
