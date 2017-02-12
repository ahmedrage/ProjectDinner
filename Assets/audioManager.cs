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

	[Range(0,1)]public float sfxLevel;
	[Range(0,1)]public float musicLevel; 
	[Range(0,1)]public float masterLevel;

	// Use this for initialization
	void Start () {
		sfxLevel = PlayerPrefs.GetFloat ("sfx volume");
		musicLevel = PlayerPrefs.GetFloat ("music volume");
		masterLevel = PlayerPrefs.GetFloat ("master volume");
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
		saveSound ("master volume", masterLevel);
	}

	public void sfxSlider(float sfxVolume){
		sfxLevel = sfxVolume;
		saveSound ("sfx volume", sfxLevel);
	}

	public void musicSlider(float musicVolume){
		musicLevel = musicVolume;
		saveSound ("music volume", musicLevel);
	}

	void saveSound(string id,float value){
		PlayerPrefs.SetFloat (id, value);
		PlayerPrefs.Save();
	}
}
