using UnityEngine;
using System.Collections;

public class ScriptBossTurret : MonoBehaviour {

	public float maxShootingDistance = 40f;
	public GameObject bulletPrefab;
	public Animation shootingAnimation;
	public float angleOfShooting = 90f;
	public float inaccuracy = 0.2f;
	public float inaccuracyY = 0.5f; 
	public GameObject deathEffect;
	private GameObject player;
	private LevelManager levelManager;
	private StatisticManager sm;

	public bool isHostile = true;

	// Sounds
	private AudioSource[] turretAudio;
	private int SHOOTING_SOUND = 0;
	private int DYING_SOUND = 1;

	// Shooting variables
	bool shooting = false;
	private Transform turretHead;
	private Transform bulletShootingPt;
	float timer = 0.0f;
	public float intervalShootTime;
	Vector3 randomOffset;
	GameObject muzzleFlash;

	private Vector3 turretFront;
	bool isAiming = false;

	// Use this for initialization
	void Start () {
		turretHead = transform.Find ("TurretHead");
		bulletShootingPt = turretHead.Find ("BulletShootingPoint");
		sm = GameObject.Find("StatisticsManager").transform.gameObject.GetComponent<StatisticManager>();
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		muzzleFlash = transform.Find ("muzzleFlashParticle").gameObject;
		turretAudio = GetComponents<AudioSource>();
		
		turretFront = transform.forward;
		turretFront.y = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen) {
			if (isHostile && playerIsVisibleToEnemey () && withinRotationRange ()) {				
				lookAtPlayer ();

				timer += Time.deltaTime;
				if (timer > Random.Range (intervalShootTime * 0.7f, intervalShootTime * 1.4f) && !withinMeleeRange()) {
					timer = 0.0f;
					shootBullet ();
				}
			}
		} else {
		//	shootingAnimation.Stop ();
		}
	}

	bool withinRotationRange()
	{
		Vector3 playerDirection = player.transform.position - transform.position;
		playerDirection.y = 0;

		float angle = Vector3.Angle (turretFront, playerDirection);
		//Debug.Log (angle);

		if (angle <= angleOfShooting / 2.0) {
			return true;
		} 

		return false;
	}

	bool playerIsVisibleToEnemey()
	{
		Vector3 playerPos = player.transform.position;
		float distance = Vector3.Distance (turretHead.transform.position, playerPos) - 1;
		Vector3 direction = playerPos - (turretHead.transform.position);
	
		// Raycast on layer 0 default and layer 9 player
		if (!Physics.Raycast(turretHead.transform.position, direction, distance, 
		                     (1<<9 | 1<<0)) && distance < maxShootingDistance) {
			//Debug.Log("Player visible to enemy");
			return true;
		}

		return false;
	}

	void shootBullet(){
		Vector3 bulletTargetPoint;

		if (shooting) {
			return;
		}

		//Debug.Log("Shooting");
		shooting = true;
		/*if (!shootingAnimation.isPlaying) {
			shootingAnimation.Play ();
		}*/

		GameObject bullet = (GameObject)Instantiate (bulletPrefab);
		bullet.transform.position = bulletShootingPt.transform.position;
		muzzleFlash.transform.position = bulletShootingPt.transform.position;
		muzzleFlash.GetComponent<ParticleSystem>().Play();
		turretAudio[SHOOTING_SOUND].Play();

		randomOffset = new Vector3(Random.Range (inaccuracy*-1, inaccuracy), Random.Range(inaccuracyY * -1, 0f), Random.Range (inaccuracy*-1, inaccuracy));
		
		bulletTargetPoint = player.transform.position + randomOffset;
		bullet.GetComponent<scriptBullet> ().setBulletDirection (bulletTargetPoint);

		shooting = false;
	}

	void lookAtPlayer()
	{
		turretHead.transform.LookAt(player.transform.position);
	}

	bool withinMeleeRange()
	{
		if (Vector3.Distance (player.transform.position, this.transform.position) < 3.0f) {
			return true;
		}
		return false;
	}

	public void Die()
	{
		Vector3 deathEffectPos = transform.position - (transform.forward/2) + new Vector3(0.0f,1.0f,0.0f);
		Instantiate (deathEffect, deathEffectPos, deathEffect.transform.rotation);
		ScoreManager.score += 50;
		sm.addProgressByStatisticName ("Enemies Killed", 1);
	//	turretAudio[DYING_SOUND].Play();
		Destroy (gameObject);
	}

}
