using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Highscore : MonoBehaviour {
	
	public static Text displayHighscore;
	public GameManager gm;
	// Use this for initialization
	void Start () {
		displayHighscore = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (ScoreManager.score < ScoreManager.highscore) {
			displayHighscore.text = "High Score to Beat: " + ScoreManager.highscore;
		}
		if (ScoreManager.score > ScoreManager.highscore) {
			displayHighscore.text = "New High Score: " + ScoreManager.score;
			displayHighscore.color = Color.yellow;
			displayHighscore.color = Color.red;
		}
	}

}
