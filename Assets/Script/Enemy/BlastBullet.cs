using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	private LevelManager levelManager;

	private GameObject blastRadius;
	private bool hasHitSomething;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();

		blastRadius = GameObject.Find ("BlastRadius");
		blastRadius.SetActive (false);

		hasHitSomething = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame) {
			if (!hasHitSomething) {
				Move();
			} else {
				
			}
		}
	}

	void OnTriggerEnter(Collider other){
		hasHitSomething = true;
		ActivateBlast ();
	}

	private void ActivateBlast(){
		blastRadius.SetActive (true);
	}

	private void Move(){
		transform.Translate(Vector3.down * Time.deltaTime, Space.World);
	}
}
