using UnityEngine;
using System.Collections;

public class ResetPivot : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Slerp(transform.rotation, 
		                                      Quaternion.LookRotation(player.transform.position - transform.position), Time.deltaTime);
	}
}
