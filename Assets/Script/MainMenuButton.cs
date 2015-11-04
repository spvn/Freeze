using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuButton : MonoBehaviour {

	public Button startGameButt;
	public Button achievementButt;
	public Button exitGameButt;

    private GameManager gm;
	private int currSelectionIndex = 0;
	private ButtonAudio ba;

	// Use this for initialization
	void Start () {
		startGameButt = startGameButt.GetComponent<Button> ();
		achievementButt = achievementButt.GetComponent<Button> ();
		exitGameButt = exitGameButt.GetComponent<Button> ();
        gm = GameManager.getManager();
		ba = GameObject.Find("OVRCameraRig").GetComponent<ButtonAudio> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.DownArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/){
			incrementCurrSelectionIndex ();
			ba.playButtonHighlightAudio();
			highlightButton(currSelectionIndex);
		} 

		if (Input.GetKeyDown(KeyCode.UpArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/){
			decrementCurrSelectionIndex ();
			ba.playButtonHighlightAudio();
			highlightButton(currSelectionIndex);
		}

		if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.JoystickButton0)) {
			ba.playButtonSelectedAudio();
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
            gm.loadLevelSelector();
			break;
		case 2:
            gm.showAchievements();
			break;
		case 3:
            gm.quitGame();
			break;
		}
	}
}
