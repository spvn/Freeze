using UnityEngine;
using System.Collections;

public class BlastRadius : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "OVRCameraRig") {
			Debug.Log ("Player within Blast Bullet radius.");
			//LevelManager.GameOver ();
		} 
		if (other.gameObject.name == "Boss") {
			Debug.Log ("Boss within Blast Bullet radius.");
		}
	}
}
