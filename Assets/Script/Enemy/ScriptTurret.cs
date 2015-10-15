using UnityEngine;
using System.Collections;

public class ScriptTurret : MonoBehaviour {
	public GameObject player;
	GameManager gameManager;
	bool shooting = false;
	public GameObject bulletPrefab;
	public Animation shootingAnimation;
	public float angleOfShooting = 90f;
	public float inaccuracy = 0.2f;
	public float inaccuracyY = 0.5f; 
	public GameObject deathEffect;

	public bool isHostile = true;

	// Shooting variables
	private Transform turretHead;
	private Transform bulletShootingPt;
	float timer = 0.0f;
	public float intervalShootTime;
	Quaternion initialAngle;
	Vector3 playerOffset = new Vector3(0, 0.0f, 0);
	Vector3 randomOffset;
	AudioSource shotSound;
	float playerSpeed;
	GameObject muzzleFlash;

	private Vector3 turretFront;
	bool isAiming = false;

	// Use this for initialization
	void Start () {
		turretHead = transform.Find ("TurretHead");
		bulletShootingPt = turretHead.Find ("BulletShootingPoint");

		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		initialAngle = this.transform.localRotation;
		playerSpeed = player.GetComponent<script2ORMovement>().playerSpeed;
		muzzleFlash = transform.Find ("muzzleFlashParticle").gameObject;
		shotSound = GetComponent<AudioSource>();

		turretFront = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isFrozen) {
			if (isHostile && playerIsVisibleToEnemey () && withinRotationRange ()) {				
				lookAtPlayer ();

				timer += Time.deltaTime;
				if (timer > Random.Range (intervalShootTime * 0.7f, intervalShootTime * 1.4f) && !withinMeleeRange ()) {
					timer = 0.0f;
					shootBullet ();
				}
			}
		} else {
			shootingAnimation.Stop ();
		}
	}

	bool withinRotationRange()
	{
		Vector3 playerDirection = player.transform.position - transform.position;

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
	
		if (!Physics.Raycast(turretHead.transform.position, direction, distance, (1<<9 | 1 <<0))) {
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
		if (!shootingAnimation.isPlaying) {
			shootingAnimation.Play ();
		}
		
		GameObject bullet = (GameObject)Instantiate (bulletPrefab);

		bullet.transform.position = bulletShootingPt.transform.position;
		muzzleFlash.transform.position = bulletShootingPt.transform.position;
		muzzleFlash.GetComponent<ParticleSystem>().Play();
		shotSound.Play();
		
		//Time of flight to reach player
		float bulletTimeToCurrPos = (player.transform.position - bulletShootingPt.transform.position).magnitude / bullet.GetComponent<scriptBullet>().speed;
		
		//playerOffset += player.transform.forward * playerSpeed;
		//randomOffset = new Vector3(Random.Range (inaccuracy*-1, inaccuracy), Random.Range(inaccuracyY/2 * -1, 0f) + 0.5f, Random.Range (inaccuracy*-1, inaccuracy));
		randomOffset = new Vector3(Random.Range (inaccuracy*-1, inaccuracy), Random.Range(inaccuracyY * -1, 0f), Random.Range (inaccuracy*-1, inaccuracy));
		
		bulletTargetPoint = player.transform.position + playerOffset + ((player.transform.forward * playerSpeed) * bulletTimeToCurrPos /2) + randomOffset;
		
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
		Destroy (gameObject);
	}

}
