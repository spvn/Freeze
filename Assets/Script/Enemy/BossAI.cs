using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	private GameObject player;
	private LevelManager levelManager;
	private BossHealth bossHealth;

	private bool bossActivated;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		bossHealth = GetComponent<BossHealth> ();

		bossActivated = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen && !levelManager.isPause) {
			if (bossHealth.getCurrentHealth() > 0){
				LookAtPlayer();
				if (bossActivated){

				}
			} else {
				Die ();
			}
		}
	}

	private void LookAtPlayer(){
		transform.LookAt(player.transform.position);
	}

	public void ActivateBoss(){
		bossActivated = true;
	}

	private void Die(){

	}
}
