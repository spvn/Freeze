using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscore : MonoBehaviour {
	
	public static Text displayHighscore;
	// Use this for initialization
	void Start () {
		displayHighscore = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ScoreManager.score > ScoreManager.highscore) {
			displayHighscore.text = "New High Score: " + ScoreManager.score;
		}
		if (ScoreManager.score < ScoreManager.highscore) {
			displayHighscore.text = "High Score to Beat: " + ScoreManager.highscore;
		}
	}

	public static void forceHighScoreDisplay () {
		displayHighscore.text = "New High Score: " + ScoreManager.score;

	}

}
