using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	public GameObject blastRadius;
	public GameObject rocket;
	public float speed = 15f;

	private LevelManager levelManager;
	private GameObject player;
	private GameObject boss;
	private GameObject explosionEffect;
	private AudioSource blastSound;

	private bool isDeflected;
	private bool bulletIsFlipped;
	private bool hasHitSomething;

	private Vector3 bossPosition;
	private Vector3 playerTargetDirection;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		player = GameObject.Find ("OVRCameraRig");
		boss = GameObject.Find ("Boss");
		explosionEffect = transform.Find ("BlastEffect").gameObject;
		blastSound = GetComponent<AudioSource> ();

		hasHitSomething = false;
		isDeflected = false;
		bulletIsFlipped = false;

		bossPosition = boss.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame && !levelManager.isFrozen) {
			if (!hasHitSomething) {
				if (isDeflected){
					if (!bulletIsFlipped){
						FlipBullet();
					}
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

		if (!blastSound.isPlaying) {
			blastSound.Play ();
		}
	}

	private void MoveToPlayer(){
		rocket.transform.LookAt (player.transform.position);
		rocket.transform.localPosition += playerTargetDirection * speed * Time.deltaTime;
	}

	private void MoveToBoss(){
		//Vector3 moveDirection = bossPosition - rocket.transform.position;
		rocket.transform.Translate(Vector3.up * Time.deltaTime * 0.1f, Space.World);
	}

	private void FlipBullet(){
		bulletIsFlipped = true;
	}

	public void setBulletDirection(Vector3 target){
		playerTargetDirection = target - transform.position;
		//playerTargetDirection = transform.rotation * playerTargetDirection;
		playerTargetDirection = Vector3.Normalize (playerTargetDirection);
	}

	public void DeflectBullet(){
		Debug.Log ("Rocket deflected");
		isDeflected = true;
	}
}
