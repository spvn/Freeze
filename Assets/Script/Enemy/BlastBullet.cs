using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	public GameObject blastRadius;
	public GameObject rocket;

	private LevelManager levelManager;
	private GameObject explosionEffect;
	
	private bool hasHitSomething;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		explosionEffect = transform.Find ("BlastEffect").gameObject;

		hasHitSomething = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame && !levelManager.isFrozen) {
			if (!hasHitSomething) {
				Move();
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
		//Destroy (gameObject);
	}

	private void ActivateBlast(){
		blastRadius.SetActive (true);
		explosionEffect.transform.position = transform.position;
		explosionEffect.GetComponent<ParticleSystem>().Play();
	}

	private void Move(){
		rocket.transform.Translate(Vector3.down * Time.deltaTime * 0.1f, Space.World);
	}
}
