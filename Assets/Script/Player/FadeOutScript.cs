using UnityEngine;
using System.Collections;

public class FadeOutScript : MonoBehaviour {
	
	public float countdownTime = 10.0f;
	public GameObject cameraOVR;

	private bool hasFadedOut = false;
	Color fadeEffect;
	
	void Start() {
		fadeEffect = GetComponent<Renderer>().material.color;
		fadeEffect.a = 0.0f;
		
	}
	// Update is called once per frame
	void Update () {
		if (countdownTime >= 0.0){
			countdownTime -= Time.deltaTime;
			fadeEffect.a  +=  0.4f * Time.deltaTime;
			GetComponent<Renderer>().material.color = fadeEffect;
		} else if (!hasFadedOut) {
			hasFadedOut = true;
			//this.gameObject.SetActive(false);
			setFadeBoxOpaque ();
			countdownTime = -1.0f;
		}
	}
	
	public void setFadeBoxOpaque () {
		fadeEffect.a = 1.0f;
		GetComponent<Renderer>().material.color = fadeEffect;
	}
	
	public void setFadeBoxTransparent () {
		fadeEffect.a = 0.0f;
		GetComponent<Renderer>().material.color = fadeEffect;
	}
}
