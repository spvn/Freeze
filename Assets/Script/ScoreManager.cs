using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
	public static int highscore = 0;
   	public static Text currentScore;

    void Start ()
    {
		currentScore = GetComponent <Text> ();
        score = 0;
    }

    void Update ()
    {
        currentScore.text = "Score: " + score;
    }

	public static void updateHighscore () {
		if (score > highscore) {
			highscore = score;
		}
	}
}
