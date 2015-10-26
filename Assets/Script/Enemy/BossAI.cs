using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	private GameObject player;
	private LevelManager levelManager;

	private bool bossActivated;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();

		bossActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen && !levelManager.isPause) {
			if (bossActivated){
				LookAtPlayer();
			}
		}
	}

	private void LookAtPlayer(){
		transform.LookAt(player.transform.position);
	}

	public void ActivateBoss(){
		bossActivated = true;
	}
}
