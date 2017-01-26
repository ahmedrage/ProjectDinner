using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class audioManager : MonoBehaviour {

	public List<AudioSource> soundEffects;
	public AudioSource music;
	public Slider masterVolume;
	public Slider sfxVolume;
	public Slider musicVolume;

	[Range(0,1)]public float sfxLevel = 0.3f;
	[Range(0,1)]public float musicLevel = 0.3f; 
	[Range(0,1)]public float masterLevel = 1;

	// Use this for initialization
	void Start () {
		//DontDestroyOnLoad (this.gameObject);

		/*if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.volume = masterLevel;

		foreach (AudioSource sfx in soundEffects) {
			sfx.volume = sfxLevel;
		}

		music.volume = musicLevel;
		masterVolume.value = masterLevel;
		sfxVolume.value = sfxLevel;
		musicVolume.value = musicLevel;
	}

	public void masterSlider(float masterVolume){
		masterLevel = masterVolume;
	}

	public void sfxSlider(float sfxVolume){
		sfxLevel = sfxVolume;
	}

	public void musicSlider(float musicVolume){
		musicLevel = musicVolume;
	}
}
