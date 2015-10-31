using UnityEngine;
using System.Collections;

public class ActivateTurrets : MonoBehaviour {
	public GameObject[] turretsToActivate;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "OVRCameraRig") {
			for(int i = 0; i < turretsToActivate.Length; i++)
			{
				turretsToActivate[i].GetComponent<ScriptTurret>().isHostile = true;
			}
		}
	}
}
