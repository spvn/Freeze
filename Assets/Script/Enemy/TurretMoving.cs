using UnityEngine;
using System.Collections;

public class TurretMoving : MonoBehaviour {

	// Stopping distance must be > 3
	public float speed;

	private GameManager gameManager;
	private GameObject player;
	private NavMeshAgent enemy;
	private Vector3 side;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		player = GameObject.Find ("OVRCameraRig");
		enemy = GetComponent<NavMeshAgent> ();
		side = new Vector3 (5, 0, 5);
	}
	
	// Update is called once per frame
	void Update () {
		if (!gameManager.isFrozen) {
			enemy.speed = speed;
			enemy.SetDestination (player.transform.position + side);
			Debug.Log ("Moving to player");
		}
	}
}
