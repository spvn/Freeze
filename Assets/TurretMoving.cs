using UnityEngine;
using System.Collections;

public class TurretMoving : MonoBehaviour {

	public float speed = 4f;

	private GameObject player;
	private NavMeshAgent enemy;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		enemy = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		enemy.speed = speed;
		enemy.SetDestination (player.transform.position);
		Debug.Log ("Moving to player");
	}
}
