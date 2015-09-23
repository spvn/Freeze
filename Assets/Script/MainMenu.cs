using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GameObject mainMenuStartScreen;
	public GameObject levelSelectionScreen;
	public GameObject blackScreen;
	
	private CanvasRenderer[] canvasRenderers;
	private bool startGame;
	private bool coroutineIsRunning;
	private string levelToLoad;

	void Start() {
		canvasRenderers = mainMenuStartScreen.GetComponentsInChildren<CanvasRenderer>();

		foreach (CanvasRenderer i in canvasRenderers) {
			print (i);
		}
		mainMenuStartScreen.SetActive (true);
		levelSelectionScreen.SetActive(false);
	}

	void Update() {
		if (startGame && !coroutineIsRunning) {
			StartCoroutine("SetupStartGame");
		}
	}
	
	IEnumerator SetupStartGame()
	{
		coroutineIsRunning = true;

		// Fade out main menu
		for (float i = 1f; i >= 0; i -= 0.01f) {
			foreach (CanvasRenderer cr in canvasRenderers) {
				cr.SetAlpha(i);
			}
			yield return null;
		}
		mainMenuStartScreen.SetActive(false);
		yield return null;

		// Fade out black screen
		for (float i = 1f; i >= 0; i -= 0.01f) {
			blackScreen.GetComponent<CanvasRenderer> ().SetAlpha(i);
			yield return null;
		}
		blackScreen.SetActive(false);
		yield return null;

		// Wait for 2 seconds
		yield return new WaitForSeconds (2f);

		Application.LoadLevel (levelToLoad);

		yield return null;
	}

	public void StartGame()
	{
		//print ("clicked on start game button");
		startGame = true;
		levelToLoad = "4350Level1";
	}

	public void StartLevelOne()
	{
		startGame = true;
		levelToLoad = "4350Level1";
	}

	public void ShowLevelSelection()
	{
		mainMenuStartScreen.SetActive (false);
		levelSelectionScreen.SetActive(true);
	}

	public void ShowStartScreen()
	{
		mainMenuStartScreen.SetActive (true);
		levelSelectionScreen.SetActive(false);
	}
}
