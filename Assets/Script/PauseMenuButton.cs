using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenuButton : MonoBehaviour
{

    public Button resumeGameButt;
    public Button exitGameButt;
	public Button yesExitButt;
	public Button noExitButt;
	
	private LevelManager lm;
	private GameManager gm;
    private int currSelectionIndex = 0;
	private ButtonAudio ba;
	private bool h_isAxisInUse, v_isAxisInUse = false;

	// Use this for initialization
    void Start()
    {
        resumeGameButt = resumeGameButt.GetComponent<Button>();
        exitGameButt = exitGameButt.GetComponent<Button>();
        yesExitButt = resumeGameButt.GetComponent<Button>();
        noExitButt = exitGameButt.GetComponent<Button>();
        lm = LevelManager.getLevelManager();
		gm = GameManager.getManager ();
		ba = GameObject.Find("ORCanvas").GetComponent<ButtonAudio> ();
    }

    // Update is called once per frame
    void Update()
    {
		if (lm.isPause) {
			//if (Input.GetKeyDown(KeyCode.DownArrow) /* || Input.GetKeyDown(KeyCode.JoystickButton0)*/)
			if ((Input.GetAxis("Vertical") < 0 && !v_isAxisInUse))
			{
				incrementCurrSelectionIndex();
				ba.playButtonHighlightAudio();
				highlightButton(currSelectionIndex);
			}
			
			if ((Input.GetAxis("Vertical") > 0 && !v_isAxisInUse))
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
		checkAxisInUse ();

	}

    private void incrementCurrSelectionIndex()
    {
        if (currSelectionIndex == 0 || currSelectionIndex == 2)
			currSelectionIndex = 1;
		else if (currSelectionIndex == 6 || currSelectionIndex == 8)
			currSelectionIndex = 7;
        else
            currSelectionIndex++;
    }

    private void decrementCurrSelectionIndex()
    {
        if (currSelectionIndex == 0 || currSelectionIndex == 1)
			currSelectionIndex = 2;
		else if (currSelectionIndex == 6 || currSelectionIndex == 7)
			currSelectionIndex = 8;
        else
            currSelectionIndex--;
    }

    private void highlightButton(int index)
    {

        switch (index)
        {
            case 1:
                resumeGameButt.Select();
				
                break;
            case 2:
                exitGameButt.Select();
				
                break;
			case 7:
				yesExitButt.Select();
				break;
			case 8:
				noExitButt.Select();
				break;
		}
	}

    private void selectButton(int index)
    {

        switch (index)
        {
            case 1:
                lm.PauseGame();
                break;
            case 2:
				lm.canvas.transform.Find("PauseScreen").gameObject.SetActive(false);
				lm.canvas.transform.Find("ExitScreen").gameObject.SetActive(true);
				currSelectionIndex = 6;
				break;
			case 7: //Exit to main menu
                gm.exitToMainMenu();
				break;
			case 8: //resume game
				currSelectionIndex = 0;
				lm.PauseGame();
				break;
		}
	}

	private void checkAxisInUse() {
		if (Input.GetAxis ("Vertical") != 0) {
			if (v_isAxisInUse == false) {
				// Call your event function here.
				v_isAxisInUse = true;
			}
		}
		if (Input.GetAxis ("Vertical") == 0) {
			v_isAxisInUse = false;
		}
		
		if (Input.GetAxis ("Horizontal") != 0) {
			if (h_isAxisInUse == false) {
				// Call your event function here.
				h_isAxisInUse = true;
			}
		}
		if (Input.GetAxis ("Horizontal") == 0) {
			h_isAxisInUse = false;
		}    
	}
}
