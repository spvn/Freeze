using UnityEngine;
using System.Collections;

public class FadingScript : MonoBehaviour {
	
	public float countdownTime = 10.0f;
	Color fadeEffect;

	void Start() {
		fadeEffect = GetComponent<Renderer>().material.color;
		fadeEffect.a = 1.0f;
	
	}
	// Update is called once per frame
	void Update () {
		if (countdownTime >= 0.0){
			countdownTime -= Time.deltaTime;
			fadeEffect.a  -=  0.3f * Time.deltaTime;
			GetComponent<Renderer>().material.color = fadeEffect;
		} else {
			fadeEffect.a  = 0.0f;	
			GetComponent<Renderer>().material.color = fadeEffect;
			countdownTime = -1.0f;
		}

	}
}
