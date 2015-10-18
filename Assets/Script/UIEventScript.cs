﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEventScript : MonoBehaviour {
	public GameObject canvasEventScreen;
	public GameObject canvasEventText;
	public string eventType;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == 9) {
			switch(eventType)
			{
			case "jump":
				Debug.Log("Entered");
				canvasEventText.GetComponent<Text>().text = "JUMP: L-ALT";
				break;
			case "move":
				Debug.Log("Moved");
				canvasEventText.GetComponent<Text>().text = "MOVE YOUR HEAD";
				break;
			default:
				break;
			}
			canvasEventScreen.SetActive (true);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.layer == 9) {
			canvasEventScreen.SetActive (false);
		}
	}
}
