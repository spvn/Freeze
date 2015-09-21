using UnityEngine;
using System.Collections;

public class ScriptTurret : MonoBehaviour {
	public GameObject player;
	GameManager gameManager;
	bool shooting = false;
	public GameObject bulletPrefab;
	public Animation turretAnimation;
	public float inaccuracy = 0.2f;
	public float inaccuracyY = 0.5f; 
	public GameObject deathEffect;

	private Transform turretHead;
	private Transform bulletShootingPt;
	float timer = 0.0f;
	public float intervalShootTime;
	public bool isHostile = true;
	Quaternion initialAngle;
	Vector3 bulletOffset = new Vector3(0, 0, 0);
	Vector3 playerOffset = new Vector3(0, 0.0f, 0);
	Vector3 randomOffset;
	Vector3 rotationVector;
	float playerSpeed;
	GameObject muzzleFlash;
	AudioSource shotSound;

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
	}
	
	// Update is called once per frame
	void Update () {
		if (isHostile){
			//Debug.Log(gameObject.name + " " + isFacingPlayer() + " " + player.GetComponent<scriptMovement>().isFrozen + withinRotationRange());
			
			if (isFacingPlayer() && !gameManager.isFrozen && withinRotationRange()) {
				if(!isAiming){
					isAiming = true;
					timer -= 1.0f;
				}
				rotationVector = player.transform.position;
				rotationVector.y = 0f;

				turretHead.transform.LookAt(rotationVector);

				timer += Time.deltaTime;
				if (timer > Random.Range (intervalShootTime*0.7f, intervalShootTime* 1.4f) && !withinMeleeRange()) {
					timer = 0.0f;
					shootBullet();
				}

			}

			if(gameManager.isFrozen){
				turretAnimation.Stop();
			}
		}
	}

	bool withinRotationRange()
	{
		float rotationAngle = Mathf.Abs ((turretHead.transform.rotation.y) - initialAngle.y) * Mathf.Rad2Deg;
		//Debug.Log (this.transform.rotation.y * Mathf.Rad2Deg + " " + initialAngle.y * Mathf.Rad2Deg);
	
		if (rotationAngle <= 90.0f && rotationAngle >= 0.0f) {
			return true;
		}
		Debug.Log ("Not turning " + rotationAngle);
		return false;
	}

	bool isFacingPlayer()
	{
		Vector3 playerPos = player.transform.position;
		float distance = Vector3.Distance (turretHead.transform.position, playerPos) - 1;
		Vector3 direction = playerPos - (turretHead.transform.position);
	
		if (!Physics.Raycast(turretHead.transform.position, direction, distance, (1<<9 | 1 <<0))) {
//			Debug.Log("Enemy is facing player");
			return true;
		}

		return false;
	}

	void shootBullet(){
		Vector3 bulletTargetPoint;

		if (shooting) {
			return;
		}

		shooting = true;
		if (!turretAnimation.isPlaying) {
			turretAnimation.Play ();
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
