using UnityEngine;
using System.Collections;

public class scriptAudio : MonoBehaviour {

	public AudioClip[] freezeSounds; //0 is freeze, 1 is unfreeze
	public AudioClip bgm;
	
	AudioSource audio;
	
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (audio.isPlaying == false) {
			//audio.clip = bgm;
			//audio.Play ();
		}
	}
	
	public void playFreezeAudio() {
		audio.PlayOneShot (freezeSounds[0]);
		audio.pitch = 0.5f;
	
	}
	
	public void playUnfreezeAudio() {
		audio.PlayOneShot (freezeSounds[1]);
		audio.pitch = 1.0f;
	}
}
