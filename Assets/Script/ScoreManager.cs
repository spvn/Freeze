using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    Text currentScore;

    void Start ()
    {
		currentScore = GetComponent <Text> ();
        score = 0;
    }


    void Update ()
    {
        currentScore.text = "Score: " + score;
    }
	
}
