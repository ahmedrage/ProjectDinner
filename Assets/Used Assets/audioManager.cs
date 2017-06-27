using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class audioManager : MonoBehaviour {

	public List<AudioSource> soundEffects;
	public AudioSource music;
	public enum AudioChannel {Master, Sfx, Music};

	public float sfxLevel { get; private set;}
	public float musicLevel { get; private set;}
	public float masterLevel { get; private set;}

	// Use this for initialization
	public static audioManager instance;

	void Awake(){
		if (instance != null) {
			Destroy (gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad (gameObject);
		}

		masterLevel = PlayerPrefs.GetFloat ("master vol");
		sfxLevel = PlayerPrefs.GetFloat ("sfx vol");
		musicLevel =PlayerPrefs.GetFloat ("music vol");
	}
	
	// Update is called once per frame
	void Update () {

		foreach (AudioSource sfx in soundEffects) {
			sfx.volume = sfxLevel;
		}
		music.volume = musicLevel;
		AudioListener.volume = masterLevel;

	}

	public void setVolume(float value, AudioChannel channel){
		switch (channel) {
		case AudioChannel.Master:
			masterLevel = value;
			break;
		case AudioChannel.Sfx:
			sfxLevel = value;
			break;
		case AudioChannel.Music:
			musicLevel = value;
			break;
		}

		PlayerPrefs.SetFloat ("master vol", masterLevel);
		PlayerPrefs.SetFloat ("sfx vol", sfxLevel);
		PlayerPrefs.SetFloat ("music vol", musicLevel);
		PlayerPrefs.Save ();
	}

	public void playSound(AudioClip clip, Vector2 pos){
		AudioSource.PlayClipAtPoint (clip, pos, sfxLevel);
	}
}
