using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	public GameObject blastRadius;
	public GameObject rocket;

	private LevelManager levelManager;
	private GameObject player;
	//private GameObject boss;
	private GameObject explosionEffect;

	private bool isDeflected;
	private bool hasHitSomething;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		player = GameObject.Find ("OVRCameraRig");
		//boss = GameObject.Find ("Boss");
		explosionEffect = transform.Find ("BlastEffect").gameObject;

		hasHitSomething = false;
		isDeflected = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame && !levelManager.isFrozen) {
			if (!hasHitSomething) {
				if (isDeflected){
					MoveToBoss();
				} else {
					MoveToPlayer();
				}
			} else {
				if (explosionEffect.GetComponent<ParticleSystem>().IsAlive()){
					gameObject.GetComponent<MeshRenderer>().enabled = false;
				} else {
					Destroy(gameObject);
				}
			}
		}
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("triggered");
		hasHitSomething = true;

		ActivateBlast ();
	}

	private void ActivateBlast(){
		blastRadius.SetActive (true);
		explosionEffect.transform.position = transform.position;
		explosionEffect.GetComponent<ParticleSystem>().Play();
	}

	private void MoveToPlayer(){
		//Stub
		rocket.transform.Translate(Vector3.down * Time.deltaTime * 0.1f, Space.World);
	}

	private void MoveToBoss(){
		// Stub
		rocket.transform.Translate(Vector3.up * Time.deltaTime * 0.1f, Space.World);
	}

	public void DeflectRocket(){
		isDeflected = true;
	}
}
