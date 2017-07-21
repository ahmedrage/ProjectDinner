using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicPlayer : MonoBehaviour {
	public int _tracknum;
	audioManager _audio;

	// Use this for initialization
	void Start () {
		_audio = GameObject.Find ("audioManager").GetComponent<audioManager> ();
		playTrack (_tracknum);
	}
	
	void playTrack(int trackNum){
		audioManager.instance.soundTracks [trackNum].Play ();
		if (trackNum - 1 >= 0) {
			audioManager.instance.soundTracks [trackNum - 1].Stop ();
		}
	}
}
