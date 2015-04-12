using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject timerGUIText;
	public GameObject player;

	bool startedGame = false;

	float timeElapsed;
	// Use this for initialization
	void Start () {
		timerGUIText.GetComponent<Text> ().text = "0.00s";
	}
	
	// Update is called once per frame
	void Update () {
		if (!startedGame && !player.GetComponent<scriptMovement>().isFrozen) {
			startedGame = true;
		}

		if (startedGame) {
			timeElapsed += Time.deltaTime;
			timerGUIText.GetComponent<Text> ().text = timeElapsed.ToString ("F2") + "s";
		}
	}
	
}
