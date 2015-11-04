using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour {

	public AudioClip[] buttonSounds; //0 is button highlight, 1 is selected
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}

	public void playButtonHighlightAudio() {
		audio.PlayOneShot (buttonSounds[0]);
	}
	
	public void playButtonSelectedAudio() {
		audio.PlayOneShot (buttonSounds[1]);
	}
}
