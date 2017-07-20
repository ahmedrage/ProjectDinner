using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cameraPan : MonoBehaviour {
	public Vector2[] coords;
	public float _showTime;
	public bool panning;
	public float panSpeed;
	public int point;
	public Image blackScreen;
	public Text title;
	Vector2 newPosition;

	void Awake(){
		point = -1;
		newPosition = transform.position;
		panning = true;
		blackScreen.enabled = false;
		title.enabled = false;
	}
		
	// Update is called once per frame
	void Update () {
		if (panning) {
			Pan ();
		}

		if (point == 2) {
			cutOut ();
		}
	}

	public void Pan()
	{
		if (transform.position.x < newPosition.x) {
			transform.localPosition = Vector2.MoveTowards (transform.position, newPosition,panSpeed);
		} 
		
		if(Mathf.Approximately(transform.position.x,newPosition.x)){
			StartCoroutine ("showTime");
			panning = false;
		}
	}

	IEnumerator showTime(){
		yield return new WaitForSeconds (_showTime);
		point++;
		if (point <= 1) {
			newPosition = coords [point];
		}
		panning = true;
	}

	public void cutOut(){
		blackScreen.enabled = true;
		title.enabled = true;
		StartCoroutine ("toStats");
	}

	IEnumerator toStats(){
		yield return new WaitForSeconds (_showTime);
		SceneManager.LoadScene ("StatScene");
	}
}
