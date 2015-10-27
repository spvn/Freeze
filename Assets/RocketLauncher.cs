using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	public GameObject rocketPrefab;
	public float shootCooldown = 2f;

	private GameObject player;
	private LevelManager levelManager;

	// Shooting
	private Transform rocketShootingPt;
	private bool isShooting;
	private float shootingTimer;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		rocketShootingPt = transform.Find ("RocketShootingPoint");

		isShooting = false;
		shootingTimer = 0;
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
