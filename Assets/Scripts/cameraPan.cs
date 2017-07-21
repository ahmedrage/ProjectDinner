using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class cameraPan : MonoBehaviour {
	public Vector2[] coords;
	public List<Image> murderPortraitImages;
	public List<Text> _murdererNames;
	public float _showTime;
	public bool panning;
	public float panSpeed;
	public int point;
	public Image blackScreen;
	public Text title;
	public Text credits;
	public statManager _statManager;
	Vector2 newPosition;

	void Awake(){
		point = -1;
		newPosition = transform.position;
		panning = true;
		blackScreen.enabled = false;
		title.enabled = false;
		_statManager = GameObject.Find ("dataManager").GetComponent<statManager> ();
	}
		
	// Update is called once per frame
	void Update () {
		if (panning) {
			Pan ();
		}

		if (point == 2) {
			cutOut ();
		}

		for (int i = 0; i < _statManager.murderers.Count; i++) {
			murderPortraitImages [i].sprite = _statManager.murderers [i];
		}

		for (int x = 0; x < _statManager.murdererNames.Count; x++) {
			_murdererNames [x].text = _statManager.murdererNames [x];
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
		title.enabled = false;
		credits.enabled = true;
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene ("StatScene");
	}
}
