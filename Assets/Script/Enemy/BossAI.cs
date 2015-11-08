using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour {
	
	public GameObject[] bossAttackGameObjects;
	// 0: opening, 1: movement, 2,3: idle
	public AudioClip[]	bossAudio;
	//0: opening, 2: idle
	public GameObject playerFadeBox;
	private GameObject player;
	private LevelManager levelManager;
	private BossHealth bossHealth;
	private Animation bossAnimations;
	private AudioSource bossAudioPlayer;

	private bool bossActivated;
	private bool openingAnimationPlayed;
	private bool isDead;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		bossHealth = GetComponent<BossHealth> ();
		bossAnimations = GetComponent<Animation> ();
		bossAudioPlayer = GetComponent<AudioSource> ();

		bossActivated = false;
		openingAnimationPlayed = false;

		// Make the boss model take the pose of the first frame of opening animation
		bossAnimations.Play ("Opening", PlayMode.StopAll);
		bossAnimations ["Opening"].speed = 0f;
		isDead = false;
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
			if (!bossAnimations.IsPlaying ("Opening") && !isDead){
				PlayIdleAnimation();
				ActivateAllAttacks();
			}
		}
	}

	private void PlayOpeningAnimation(){
		bossAnimations ["Opening"].speed = 0.5f;
		bossAnimations.Play ("Opening", PlayMode.StopAll);
		openingAnimationPlayed = true;

		bossAudioPlayer.clip = bossAudio [0];
		bossAudioPlayer.PlayDelayed(0.1f);
	}

	private void PlayIdleAnimation(){
		if (!bossAnimations.IsPlaying ("Idle")) {
			bossAnimations ["Idle"].speed = 0.5f;
			bossAnimations.Play ("Idle", PlayMode.StopAll);
		}

		bossAudioPlayer.clip = bossAudio [1];
		if (!bossAudioPlayer.isPlaying) {
			bossAudioPlayer.PlayDelayed(0f);
			bossAudioPlayer.loop = true;
		}
	}

	private void ActivateAllAttacks(){
		for (int i = 0; i < bossAttackGameObjects.Length; i++) {
			if (bossAttackGameObjects[i].tag == "RocketLauncher"){
				bossAttackGameObjects[i].GetComponentInChildren<RocketLauncher>().MakeHostile();
			} else if (bossAttackGameObjects[i].tag == "BossTurret"){
				bossAttackGameObjects[i].SetActive(true);
				bossAttackGameObjects[i].GetComponent<ScriptBossTurret>().isHostile = true;
			}
		}
	}

	public void ActivateBoss(){
		bossActivated = true;
	}

	private void Die(){
		isDead = true;

		for (int i = 0; i < bossAttackGameObjects.Length; i++) {
			if (bossAttackGameObjects[i].tag == "RocketLauncher"){
				bossAttackGameObjects[i].GetComponentInChildren<RocketLauncher>().Die ();
			} else if (bossAttackGameObjects[i].tag == "BossTurret"){
				bossAttackGameObjects[i].GetComponent<ScriptBossTurret>().Die ();
			}
		}
		bossAnimations.Play ("Death", PlayMode.StopAll);
		StartCoroutine (LoadCredits());
	}

	private IEnumerator LoadCredits()
	{
		yield return new WaitForSeconds (2.875f);
		Debug.Log ("LoadCredits");
		bossAnimations.Stop ();
		bossAudioPlayer.Stop ();
		playerFadeBox.SetActive (true);
	}
}
