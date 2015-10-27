using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	public GameObject rocketPrefab;
	
	private Transform rocketShootingPt;
	private bool isShooting;

	// Use this for initialization
	void Start () {
		rocketShootingPt = gameObject.Find ("RocketShootingPoint");

		isShooting = false;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
