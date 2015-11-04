using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	public Button restartGameButt;
	public Button checkpointGameButt;
	public Button exitGameButt;
	
	private LevelManager lm;
	private GameManager gm;
	private int currSelectionIndex = 0;
	private ButtonAudio ba;
	
	// Use this for initialization
	void Start()
	{
		restartGameButt = restartGameButt.GetComponent<Button>();
		checkpointGameButt = checkpointGameButt.GetComponent<Button>();
		exitGameButt = exitGameButt.GetComponent<Button>();
		lm = LevelManager.getLevelManager();
		gm = GameManager.getManager ();
		ba = GameObject.Find("ORCanvas").GetComponent<ButtonAudio> ();
	}
	
	// Update is called once per frame
	void Update()
	{
		if (lm.isGameOver) {
			if (Input.GetKeyDown(KeyCode.DownArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/)
			{
				incrementCurrSelectionIndex();
				ba.playButtonHighlightAudio();
				highlightButton(currSelectionIndex);
			}
			
			if (Input.GetKeyDown(KeyCode.UpArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/)
			{
				decrementCurrSelectionIndex();
				ba.playButtonHighlightAudio();
				highlightButton(currSelectionIndex);
			}
			
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0))
			{
				Debug.Log("selected ENTER");
				ba.playButtonSelectedAudio();
				selectButton(currSelectionIndex);
			}
		}
	}
	
	private void incrementCurrSelectionIndex()
	{
		if (currSelectionIndex == 0 || currSelectionIndex == 3)
			currSelectionIndex = 1;
		else
			currSelectionIndex++;
	}
	
	private void decrementCurrSelectionIndex()
	{
		if (currSelectionIndex == 0 || currSelectionIndex == 1)
			currSelectionIndex = 3;
		else
			currSelectionIndex--;
	}
	
	private void highlightButton(int index)
	{
		
		switch (index)
		{
		case 1:
			checkpointGameButt.Select();
			break;
		case 2:
			restartGameButt.Select();
			break;
		case 3:
			exitGameButt.Select();
			break;
		}
	}
	
	private void selectButton(int index)
	{
		
		switch (index)
		{
		case 1:
			lm.restartFromCheckpoint();
			break;
		case 2:
			lm.RestartLevel();
			break;
		case 3:
			Application.LoadLevel (0);
			break;
		}
	}
}
