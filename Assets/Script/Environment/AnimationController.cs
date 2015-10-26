using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    public Animator animator;
    private LevelManager levelManager;

	// Use this for initialization
	void Start () {
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (levelManager.isFrozen || levelManager.isPause || levelManager.isGameOver)
        {
            animator.speed = 0;
        } else
        {
            animator.speed = 1;
        }
	}
}
