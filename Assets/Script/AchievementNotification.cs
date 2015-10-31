using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementNotification : MonoBehaviour {
    
    public float fadeSpeed = 5.0f;
    private bool fadeIn = true;
    private float screenTime = 2.0f;

    void Start()
    {
        Color color = transform.GetComponent<Text>().material.color;
        color.a = 0;
        transform.GetComponent<Text>().material.color = color;
    }

    void Update()
    {
        Debug.Log(transform.GetComponent<Text>().material.color.a);
        if (transform.GetComponent<Text>().material.color.a >= 0.95)
        {
            StartCoroutine(onScreenTime());
        }

        if (!fadeIn)
        {
            transform.GetComponent<Text>().material.color = Color.Lerp(transform.GetComponent<Text>().material.color, Color.clear, fadeSpeed * Time.deltaTime);
            if (transform.GetComponent<Text>().material.color.a <= 0.01)
            {
                Destroy(gameObject);
            }
        } else
        {
            transform.GetComponent<Text>().material.color = Color.Lerp(transform.GetComponent<Text>().material.color, Color.white, fadeSpeed * Time.deltaTime);
        }
    }

    private IEnumerator onScreenTime()
    {
        yield return new WaitForSeconds(screenTime);
        fadeIn = false;
    }

    public void setAchievementText(string achievementName)
    {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Achievement Unlocked: " + achievementName;
    }
}
