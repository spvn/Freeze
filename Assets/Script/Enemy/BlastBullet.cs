using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	public GameObject blastRadius;
	public GameObject rocket;

	private LevelManager levelManager;
	
	private bool hasHitSomething;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();


		hasHitSomething = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame && !levelManager.isFrozen) {
			if (!hasHitSomething) {
				Move();
			} else {
				
			}
		}
	}

	void OnTriggerEnter(Collider other){
		hasHitSomething = true;
		ActivateBlast ();
		Destroy (gameObject);
	}

	private void ActivateBlast(){
		blastRadius.SetActive (true);
	}

	private void Move(){
		rocket.transform.Translate(Vector3.down * Time.deltaTime * 0.1f, Space.World);
	}
}
