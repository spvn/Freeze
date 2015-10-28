using UnityEngine;
using System.Collections;

public enum ShootTarget{
	AT_PLAYER,
	RIGHT_OF_PLAYER,
	LEFT_OF_PLAYER
}

public class RocketLauncher : MonoBehaviour {

	public GameObject rocketPrefab;
	public ShootTarget shootTarget;
	public float shootCooldown = 2f;
	public float inaccuracy = 0.2f;
	public float inaccuracyY = 0.5f; 

	private GameObject player;
	private LevelManager levelManager;

	// Shooting
	private Transform rocketShootingPt;
	private Vector3 playerOffset;
	private bool isShooting;
	private float shootingTimer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		rocketShootingPt = transform.Find ("RocketShootingPoint");

		isShooting = false;
		shootingTimer = 0;

		if (shootTarget == ShootTarget.AT_PLAYER) {
			playerOffset = Vector3.zero;
		} else if (shootTarget == ShootTarget.LEFT_OF_PLAYER) {
			playerOffset = Vector3.left * 5;
		} else {
			playerOffset = Vector3.right * 5;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen) {
			if (canShoot()){
				ShootRocket();
			}
		}
	}

	private void ShootRocket(){
		if (isShooting) {
			return;
		} else {
			isShooting = true;
		}

		GameObject rocket = (GameObject)Instantiate (rocketPrefab);
		rocket.transform.position = rocketShootingPt.transform.position;

		Vector3 randomOffset = new Vector3(Random.Range (inaccuracy*-1, inaccuracy), 
		    Random.Range(inaccuracyY * -1, 0f), Random.Range (inaccuracy*-1, inaccuracy));

		Vector3 targetPoint = player.transform.position + playerOffset + randomOffset;

		rocket.GetComponentInChildren<BlastBullet> ().setBulletDirection (targetPoint);

		isShooting = false;
	}

	private bool canShoot(){
		shootingTimer += Time.deltaTime;

		if (shootingTimer >= shootCooldown) {
			shootingTimer = 0;
			return true;
		} 
		return false;
	}
}
