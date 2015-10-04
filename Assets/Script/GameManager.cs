using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	GameObject timerGUIText;
	public GameObject player;
	public bool isFrozen;
	public bool isGameOver = false;
    public bool isChoosingPath = false;
	public GameObject canvas;
	public Highscore hs;

	const int NUM_LEVELS = 1;

	public bool startedGame = false;

	public static float timeElapsed;

	// Use this for initialization
	void Start () {	
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
			timeElapsed += Time.deltaTime;
			timerGUIText.GetComponent<Text> ().text = timeElapsed.ToString ("F2") + "s";
		}

		if (!isGameOver && (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.JoystickButton0))) {
			//Debug.Log("Pressed Freeze " + isFrozen );
			
			isFrozen = !isFrozen;
			
			if (isFrozen) {
				this.GetComponent<scriptAudio>().playFreezeAudio();
			}
			else {
				this.GetComponent<scriptAudio>().playUnfreezeAudio();
			}
			
			if(canvas.gameObject.transform.Find("StartingScreen").gameObject.activeSelf)
			{
				canvas.gameObject.transform.Find("StartingScreen").gameObject.SetActive(false);
			}
		}

		if (canvas.gameObject.transform.Find ("GameOverScreen").gameObject.activeSelf && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton5))) {
			RestartLevel();
		}

		if (canvas.gameObject.transform.Find ("WinScreen").gameObject.activeSelf && (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton5))) {
			LoadNextLevel();
		}

	}

	public void GameOver(){
		ScoreManager.updateHighscore ();
		canvas.gameObject.transform.Find("GameOverScreen").gameObject.SetActive (true);
		timeElapsed = 0.0f;
		isFrozen = true;
		isGameOver = true;
	}
	
	public void RestartLevel()
	{
		Debug.Log ("Restarting");
		Application.LoadLevel (Application.loadedLevel);
	}

	public void LoadNextLevel()
	{
		if (Application.loadedLevel < NUM_LEVELS) {
			Application.LoadLevel (Application.loadedLevel + 1);
		} else {
			RestartLevel();
		}
	}
}
