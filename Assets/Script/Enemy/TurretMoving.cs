using UnityEngine;
using System.Collections;

public enum AppearSide{
	APPEAR_LEFT,
	APPEAR_RIGHT
}

public class TurretMoving : MonoBehaviour {
	public float speed;
	public AppearSide chaseSide;

	private GameManager gameManager;
	private GameObject player;
	private NavMeshAgent enemy;
	private Vector3 rightSide;
	private Vector3 leftSide;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		player = GameObject.Find ("OVRCameraRig");
		enemy = GetComponent<NavMeshAgent> ();
		enemy.speed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isFrozen) {
			ChasePlayer ();
		} else {
			StopChase();
		}
	}

	void ChasePlayer(){
		enemy.Resume ();
		if (chaseSide == AppearSide.APPEAR_LEFT) {
			enemy.SetDestination (player.transform.position 
			                      + 3*player.transform.forward - 5*player.transform.right);
			Debug.Log ("Chasing on player's left");
		} else {
			enemy.SetDestination (player.transform.position 
			                      + 3*player.transform.forward + 5*player.transform.right);
			Debug.Log ("Chasing on player's right");
		}
	}

	void StopChase(){
		enemy.Stop ();
		Debug.Log ("Enemy stopped chasing.");
	}
}
