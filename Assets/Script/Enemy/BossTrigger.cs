using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	private BossAI bossAI;

	// Use this for initialization
	void Start () {
		bossAI = GameObject.Find ("Boss").GetComponent<BossAI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col){
		if (col.gameObject.name == "OVRCameraRig"){
			bossAI.ActivateBoss();
			Destroy (gameObject);
		}
	}
}
