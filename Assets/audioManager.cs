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
		//sfxLevel = PlayerPrefs.GetFloat ("sfx volume");
		//musicLevel = PlayerPrefs.GetFloat ("music volume");
		AudioListener.volume = PlayerPrefs.GetFloat ("masterVol");

		masterVolume.value = AudioListener.volume;
		//sfxVolume.value = sfxLevel;
		//musicVolume.value = musicLevel;
	}
	
	// Update is called once per frame
	void Update () {

		/*foreach (AudioSource sfx in soundEffects) {
			sfx.volume = sfxLevel;
		}
		music.volume = musicLevel;*/


	}

	public void masterSlider(float masterVolume){
		AudioListener.volume = masterVolume;
		PlayerPrefs.SetFloat ("masterVol", AudioListener.volume);
		PlayerPrefs.Save ();
		//masterLevel = masterVolume;
		//saveSound ("master volume", AudioListener.volume);
	}

	public void sfxSlider(float sfxVolume){
		//sfxLevel = sfxVolume;
		//saveSound ("sfx volume", sfxLevel);
	}

	public void musicSlider(float musicVolume){
		//musicLevel = musicVolume;
		//saveSound ("music volume", musicLevel);
	}

	void saveSound(string id,float value){
		//PlayerPrefs.SetFloat (id, value);
		//PlayerPrefs.Save();
	}
}
