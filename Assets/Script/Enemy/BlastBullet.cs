using UnityEngine;
using System.Collections;

public class BlastBullet : MonoBehaviour {

	public GameObject blastRadius;
	public GameObject rocket;
	public float speed = 5f;

	private LevelManager levelManager;
	private GameObject player;
	private GameObject boss;
	private GameObject explosionEffect;
	private AudioSource blastSound;

	private bool isDeflected;
	private bool hasHitSomething;

	private Vector3 bossPosition;
	private Vector3 deflectedPosition;
	private Vector3 playerTargetDirection;
	private Vector3 bossTargetDirection;

	public LineRenderer bulletLine;
	private Vector3 firstAppearancePosition;
	private Vector3 bulletLineEndVertex;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		player = GameObject.Find ("OVRCameraRig");
		boss = GameObject.Find ("Boss");
		explosionEffect = transform.Find ("BlastEffect").gameObject;
		blastSound = GetComponent<AudioSource> ();
		bulletLine = GetComponent<LineRenderer> ();

		hasHitSomething = false;
		isDeflected = false;

		bossPosition = boss.transform.Find("BossCenter").transform.position;
		firstAppearancePosition = rocket.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (levelManager.startedGame && !levelManager.isFrozen) {
			if (!hasHitSomething) {
				if (isDeflected){
					MoveToBoss();
				} else {
					DrawBulletLine ();
					CheckHitPlayer();
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
		if (!isDeflected && other.gameObject.name == "Boss") {
			// Ignore the boss collision box when it is first fired from boss
		} else {
			//Debug.Log ("Rocket triggered.");
			hasHitSomething = true;
			ActivateBlast ();
		}
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
		rocket.transform.LookAt (bossPosition);
		rocket.transform.localPosition += bossTargetDirection * speed * Time.deltaTime;
		//rocket.transform.Translate(Vector3.up * Time.deltaTime * 0.1f, Space.World);
	}

	private void DrawBulletLine(){
		bulletLine.enabled = true;
		bulletLine.SetWidth (0.04f, 0.04f);
		bulletLine.SetPosition (0, firstAppearancePosition);
		bulletLine.SetPosition(1, bulletLineEndVertex);
	}

	private void CheckHitPlayer(){
		RaycastHit objHit;
		// Check if aiming directly at player
		if (Physics.Raycast (firstAppearancePosition, bulletLineEndVertex-firstAppearancePosition, 
		                     out objHit, Mathf.Infinity, (1<<9))) {
			bulletLine.SetColors (Color.red, Color.red);
		} else {
			// Check if player is wihtin blast radius
			Collider[] hitColliders = Physics.OverlapSphere(bulletLineEndVertex, 
			                                                blastRadius.GetComponent<SphereCollider>().radius);
			for (int i = 0; i < hitColliders.Length; i++){
				if (hitColliders[i].gameObject.name == "OVRCameraRig"){
					bulletLine.SetColors (Color.red, Color.red);
					return;
				}
			}

			bulletLine.SetColors(Color.green, Color.green);
		}
	}

	public void setBulletDirection(Vector3 target){
		playerTargetDirection = target - transform.position;
		playerTargetDirection = Vector3.Normalize (playerTargetDirection);

		bulletLineEndVertex = target + playerTargetDirection*50;
	}

	public void DeflectBullet(){
		Debug.Log ("Rocket deflected");
		isDeflected = true;

		deflectedPosition = rocket.transform.position;
		bossTargetDirection = bossPosition - deflectedPosition;
		bossTargetDirection = Vector3.Normalize (bossTargetDirection);

		bulletLine.enabled = false;
	}
}
