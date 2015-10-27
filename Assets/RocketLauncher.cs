using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	public GameObject rocketPrefab;

	private GameObject player;
	private LevelManager levelManager;
	private Transform rocketShootingPt;
	private bool isShooting;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		rocketShootingPt = transform.Find ("RocketShootingPoint");

		isShooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!levelManager.isFrozen) {
			LookAtPlayer();
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

	private void LookAtPlayer()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, 
		                                      Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime);
	}
}
