using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour {

	public AudioClip[] buttonSounds; //0 is button highlight, 1 is selected

	private Button button{get {return GetComponent<Button>();}}
	private AudioSource audio{get {return GetComponent<AudioSource>();}}
	

	// Use this for initialization
	void Start () {
//		audio = GetComponent<AudioSource>();
	}

	public void playButtonHighlightAudio() {
//		audio.PlayOneShot (buttonSounds[0]);audio.pitch = 1.0f;
	}
	
	public void playButtonSelectedAudio() {
//		audio.PlayOneShot (buttonSounds[1]);audio.pitch = 1.0f;
	}
}
