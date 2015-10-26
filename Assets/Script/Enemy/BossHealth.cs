using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	private GameObject player;
	private LevelManager levelManager;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
