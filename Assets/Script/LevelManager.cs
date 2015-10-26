using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	private static LevelManager _instance;

	GameObject timerGUIText;
	public GameObject player;
	public bool isFrozen;
	public bool isGameOver = false;
    public bool isChoosingPath = false;
	public bool isFreezeBarActivated = false;
	public bool isAttacking = false;
	public GameObject canvas;
	public GameObject cameraOVR;
	public Highscore hs;
	public bool isPause = false;
	const int NUM_LEVELS = 4;
	public bool startedGame = false;
	public float timeElapsed;

    private GameManager gm;
	private bool isFrozenWhenPaused = false;

	void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
	//		DontDestroyOnLoad(this);
		}
		else
		{
			if (this != _instance)
			{
				Destroy(this.gameObject);
			}
		}
	}

	// Use this for initialization
	void Start () {
        gm = GameManager.getManager();
        gm.updateCurrentLevel();
        isFrozen = true;
		timerGUIText = canvas.transform.Find ("Panel").transform.Find ("TimerText").gameObject;
		timerGUIText.GetComponent<Text> ().text = "0.00s";
	}
	
	// Update is called once per frame
	void Update () {
		if (!startedGame && !isFrozen) {
			startedGame = true;
		}

		if (startedGame && !isGameOver) {
			if (!isPause) {
				timeElapsed += Time.deltaTime;
				timerGUIText.GetComponent<Text> ().text = timeElapsed.ToString ("F2") + "s";
			}
		}

		if (!isGameOver && (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.JoystickButton0))) {

			if (!isPause) {
				//Debug.Log("Pressed Freeze " + isFrozen );
				if(isChoosingPath)
				{
					return; // player cannot un-freeze when choosing a path to go
				}
				invertFreezeStatus ();
				setFreezeBar ();
				
				if (isFrozen) {
					this.GetComponent<scriptAudio>().playFreezeAudio();
				}
				else {
					this.GetComponent<scriptAudio>().playUnfreezeAudio();
				}
			}

			if(canvas.gameObject.transform.Find("StartingScreen").gameObject.activeSelf)
			{
				canvas.gameObject.transform.Find("StartingScreen").gameObject.SetActive(false);
			}

		}

		//Pause State
		if (!isGameOver && (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.JoystickButton7)/*start button on Xbox Controller*/)) {

            if (!isPause)
               isFrozenWhenPaused = isFrozen;
			PauseGame();
        }

		if (canvas.gameObject.transform.Find ("GameOverScreen").gameObject.activeSelf && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton5))) {
			RestartLevel();
		}

		if (canvas.gameObject.transform.Find ("WinScreen").gameObject.activeSelf && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton5))) {
			LoadNextLevel();
		}
        


	}

    public void PauseGame() {
        invertPauseStatus();

        if (isPause)
        {
            isFrozen = true;
            canvas.transform.Find("Panel").gameObject.SetActive(false);
            canvas.transform.Find("Score Panel").gameObject.SetActive(false);
            canvas.transform.Find("Freeze Bar").gameObject.SetActive(false);
            canvas.transform.Find("Action Bar").gameObject.SetActive(false);
            canvas.transform.Find("EventScreen").gameObject.SetActive(false);
			canvas.transform.Find("ExitScreen").gameObject.SetActive(false);
            canvas.transform.Find("PauseScreen").gameObject.SetActive(true);
            cameraOVR.transform.Find("Fade Box").gameObject.SetActive(true);
        }
        else
        {
			isFrozen = isFrozenWhenPaused;
			canvas.transform.Find("Panel").gameObject.SetActive(true);
            canvas.transform.Find("Score Panel").gameObject.SetActive(true);
            canvas.transform.Find("Freeze Bar").gameObject.SetActive(true);
            canvas.transform.Find("Action Bar").gameObject.SetActive(true);
            canvas.transform.Find("EventScreen").gameObject.SetActive(true);
			canvas.transform.Find("ExitScreen").gameObject.SetActive(false);
            canvas.transform.Find("PauseScreen").gameObject.SetActive(false);
            cameraOVR.transform.Find("Fade Box").gameObject.SetActive(false);
        }
    }

	public void GameOver(){
        gm.playerGameOver();
		ScoreManager.updateHighscore ();
		isFrozen = true;
		isGameOver = true;
	}
	
	public void RestartLevel()
	{
        gm.restartCurrentLevel();
	}

	public void LoadNextLevel()
	{
        gm.currentLevelCleared();
        gm.loadNextLevel();
	}

	public void invertFreezeStatus () {
		isFrozen = !isFrozen;
	}

	public void invertPauseStatus () {
		isPause = !isPause;
	}

	public void setFreezeBar () {
		isFreezeBarActivated = isFrozen;
	}

	public bool getFreezeBarStatus () {
		return isFreezeBarActivated;
	}

	public bool getIsAttackingStatus () {
		return isAttacking;
	}

	public void setActionBar () {
		Debug.Log ("Setting Action Bar");
		isAttacking = !isAttacking;
	}

	public static LevelManager getLevelManager()
	{
		if (_instance == null)
		{
			Debug.LogError("Level Manager not instantiated yet");
			return null;
		}
		else
		{
			return _instance;
		}
	}
}
