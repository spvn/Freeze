using UnityEngine;
using System.Collections;

public class LightningField : MonoBehaviour {

    public float timeActive;
    public float timeInactive;

    private float timeRemaining;
    private bool isActive;
    private Transform leftPillar;

    // Use this for initialization
	void Start () {
        timeRemaining = timeInactive;
        isActive = false;

        int children = transform.childCount;
        for (int i = 0; i < children; ++i)
        {
            if (transform.GetChild(i).name == "Left Pillar")
            {
                leftPillar = transform.GetChild(i);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timeRemaining -= Time.deltaTime;
        // Swap between active and inactive
        if (timeRemaining <= 0)
        {
            isActive = !isActive;
            if (isActive)
            {
                print("Lightning Field is now active");
                timeRemaining = timeActive;
            }
            else
            {
                print("Lightning Field is now inactive");
                timeRemaining = timeInactive;
            }
            toggleLightning();
        }
	}

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && isActive)
        {
            print("Zap!!!");
        }
    }

    void toggleLightning()
    {
        int lightningCount = leftPillar.childCount;
        for (int i = 0; i < lightningCount; i++)
        {
            bool newState = !leftPillar.GetChild(i).gameObject.activeInHierarchy;
            leftPillar.GetChild(i).gameObject.SetActive(newState);
        }
        return;
    }
}