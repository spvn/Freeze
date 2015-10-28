using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {

	public bool bossActivated;

	public GameObject[] bossAttackGameObjects;

	private GameObject player;
	private LevelManager levelManager;
	private BossHealth bossHealth;
	private Animation bossAnimations;

	private bool openingAnimationPlayed;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		bossHealth = GetComponent<BossHealth> ();
		bossAnimations = GetComponent<Animation> ();

		bossActivated = false;
		openingAnimationPlayed = false;

		// Make the boss model take the pose of the first frame of opening animation
		bossAnimations.Play ("Opening", PlayMode.StopAll);
		bossAnimations ["Opening"].speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen && !levelManager.isPause) {
			if (bossActivated){
				HandleAnimationAndSetup();

				if (bossHealth.getCurrentHealth() > 0){
					LookAtPlayer();
						
				} else {
					Die ();
				}
			}
		}
	}

	private void LookAtPlayer(){
		transform.LookAt(player.transform.position);
	}

	private void HandleAnimationAndSetup(){
		if (!openingAnimationPlayed){
			PlayOpeningAnimation();
		} else {
			if (!bossAnimations.IsPlaying ("Opening")){
				PlayIdleAnimation();
			}
			ActivateAllAttacks();
		}
	}

	private void PlayOpeningAnimation(){
		bossAnimations ["Opening"].speed = 0.5f;
		bossAnimations.Play ("Opening", PlayMode.StopAll);
		openingAnimationPlayed = true;
	}

	private void PlayIdleAnimation(){
		if (!bossAnimations.IsPlaying ("Idle")) {
			bossAnimations ["Idle"].speed = 0.5f;
			bossAnimations.Play ("Idle", PlayMode.StopAll);
		}
	}

	private void ActivateAllAttacks(){
		for (int i = 0; i < bossAttackGameObjects.Length; i++) {
			if (bossAttackGameObjects[i].tag == "RocketLauncher"){
				bossAttackGameObjects[i].GetComponentInChildren<RocketLauncher>().MakeHostile();
			} else if (bossAttackGameObjects[i].tag == "BossTurret"){
				bossAttackGameObjects[i].GetComponent<ScriptTurret>().isHostile = true;
			}
		}
	}

	public void ActivateBoss(){
		bossActivated = true;
	}

	private void Die(){

	}
}
