using UnityEngine;
using System.Collections;

public class ParticleEmmitterController : MonoBehaviour {

    public GameObject[] gameObjects;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        LevelManager levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        if (levelManager.isFrozen || levelManager.isPause || levelManager.isGameOver)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                //gameObjects[i].SetActive(false);
				gameObjects[i].GetComponent<ParticleSystem>().Stop();
            }
        } else
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                //gameObjects[i].SetActive(true);
				gameObjects[i].GetComponent<ParticleSystem>().Play ();
            }
        }
    }
}
