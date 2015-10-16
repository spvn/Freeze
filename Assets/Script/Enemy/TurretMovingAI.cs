using UnityEngine;
using System.Collections;

public class TurretMovingAI : MonoBehaviour {
	public GameObject player;

	bool shooting = false;
	public GameObject bulletPrefab;
	public Animation turretAnimation;
	public float inaccuracy = 0.2f;
	public float inaccuracyY = 0.5f; 
	public GameObject deathEffect;

	public float intervalShootTime;
	public bool isHostile = true;

	private LevelManager levelManager;
	GameObject muzzleFlash;
	AudioSource shotSound;
	private bool withinRange;
	private Transform turretHead;
	private Transform bulletShootingPt;
	private float timer = 0.0f;
	Quaternion initialAngle;
	Vector3 randomOffset;
	Vector3 rotationVector;
	float playerSpeed;

	
	bool isAiming = false;
	
	// Use this for initialization
	void Start () {
		turretHead = transform.Find ("TurretHead");
		bulletShootingPt = turretHead.Find ("BulletShootingPoint");
		
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		initialAngle = this.transform.localRotation;
		playerSpeed = player.GetComponent<script2ORMovement>().playerSpeed;
		muzzleFlash = transform.Find ("muzzleFlashParticle").gameObject;
		shotSound = GetComponent<AudioSource>();

		withinRange = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isHostile){
			//Debug.Log(gameObject.name + " " + isFacingPlayer() + " " + player.GetComponent<scriptMovement>().isFrozen + withinRotationRange());
			if (!levelManager.isFrozen){
				facePlayer();

				if (withinRange){
					timer += Time.deltaTime;
					if (timer > Random.Range (intervalShootTime*0.7f, intervalShootTime* 1.4f) && !withinMeleeRange()) {
						timer = 0.0f;
						shootBullet();
					}
				}
			} else{
				turretAnimation.Stop();
			}
		}
	}

	void facePlayer(){
		rotationVector = player.transform.position;
		rotationVector.y = 0f;
		
		turretHead.transform.LookAt(rotationVector);
	}
	
	void shootBullet(){
		Vector3 bulletTargetPoint;
		
		if (shooting) {
			return;
		}
		
		Debug.Log("Shooting");
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
		
		bulletTargetPoint = player.transform.position + ((player.transform.forward * playerSpeed) * bulletTimeToCurrPos /2) + randomOffset;
		
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

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.name == "OVRCameraRig") {
			withinRange = true;
			//Debug.Log ("Within range");
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name == "OVRCameraRig") {
			withinRange = false;
			//Debug.Log ("Outside range");
		}

	}
}
