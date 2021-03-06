using UnityEngine;
using System.Collections;

public enum AppearSide{
	APPEAR_LEFT,
	APPEAR_RIGHT
}

public class TurretMoving : MonoBehaviour {
	public float speed;
	public AppearSide chaseSide;
	public float selfDestruct;

	private LevelManager levelManager;
	private GameObject player;
	private NavMeshAgent enemy;
	private Vector3 rightSide;
	private Vector3 leftSide;

	private Vector3 chasePosition;
	private float playerSpeed;
	private bool enemyStopped;
	private float aliveTime;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		player = GameObject.Find ("OVRCameraRig");
		enemy = GetComponent<NavMeshAgent> ();

		enemyStopped = false;
		enemy.speed = speed;
		playerSpeed = player.GetComponent<script2ORMovement> ().playerSpeed;
		aliveTime = 0;
		ChasePlayer ();
		StopChase ();
	}
	
	// Update is called once per frame
	void Update () {
		aliveTime += Time.deltaTime;
		if (aliveTime > selfDestruct) {
			this.gameObject.transform.GetComponent<ScriptTurret>().Die();
		}
		if (!levelManager.isFrozen) {
			ResumeChase();
			//if (isLockedOnPlayer()){
			//	LagChasePlayer();
			//} else {
				ChasePlayer ();
			//}
		} else {
			StopChase();
		}
	}

	void LagChasePlayer(){
		if (chasePosition == null) {
			return;
		}

//		Debug.Log ("Lag chasing");
		// TODO: check what if chasing while player rotating
		enemy.SetDestination (chasePosition + player.transform.forward*playerSpeed);
	}

	void ChasePlayer(){
		if (chaseSide == AppearSide.APPEAR_LEFT) {
			chasePosition = player.transform.position 
				+ 3*player.transform.forward - 5*player.transform.right;
			enemy.SetDestination (chasePosition);
			//Debug.Log ("Chasing on player's left");
		} else {
			chasePosition = player.transform.position 
				+ 3*player.transform.forward + 5*player.transform.right;
			enemy.SetDestination (chasePosition);
			//Debug.Log ("Chasing on player's right");
		}
	}

	bool isLockedOnPlayer(){
		if (enemy.remainingDistance <= 1) {
			return true;
		}
		return false;
	}

	void StopChase(){
		//enemy.Stop ();
		//enemy.SetDestination (transform.position);
		enemy.enabled = false;
		enemyStopped = true;
		//Debug.Log ("Enemy stopped chasing.");
	}

	void ResumeChase(){
		if (enemyStopped) {

			enemy.enabled = true;
			enemy.Resume ();

			enemyStopped = false;
		}
	}

	// TODO: Need dumber AI to trigger this
	void OnTriggerEnter(Collider other){
		Debug.Log ("Moving turret got hit by something.");
		GetComponent<ScriptTurret> ().Die();
	}
}
