﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	public Button startGameButt;
	public Button achievementButt;
	public Button exitGameButt;

	private int currSelectionIndex = 0;

	// Use this for initialization
	void Start () {
		startGameButt = startGameButt.GetComponent<Button> ();
		achievementButt = achievementButt.GetComponent<Button> ();
		exitGameButt = exitGameButt.GetComponent<Button> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.DownArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/){
			incrementCurrSelectionIndex ();
			highlightButton(currSelectionIndex);
		} 

		if (Input.GetKeyDown(KeyCode.UpArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/){
			decrementCurrSelectionIndex ();
			highlightButton(currSelectionIndex);
		}

		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.JoystickButton0)) {
			Debug.Log ("selected ENTER");
			selectButton (currSelectionIndex);
		}
	}

	private void incrementCurrSelectionIndex () {
		if (currSelectionIndex == 0 || currSelectionIndex == 3)
			currSelectionIndex = 1;
		else
			currSelectionIndex++;
	}

	private void decrementCurrSelectionIndex () {
		if (currSelectionIndex == 0 || currSelectionIndex == 1)
			currSelectionIndex = 3;
		else
			currSelectionIndex--;
	}

	private void highlightButton (int index) {

		switch (index) {
		case 1:
			startGameButt.Select();
			break;
		case 2:
			achievementButt.Select();
			break;
		case 3:
			exitGameButt.Select();
			break;
		}
	}

	private void selectButton (int index) {

		switch (index) {
		case 1:
			Debug.Log ("trying to load level 1");
			Application.LoadLevel(1);
			break;
		case 2:
		//	for achievement scene
			break;
		case 3:
			Debug.Log ("quiting...");
			Application.Quit();
			break;
		}
	}
}
