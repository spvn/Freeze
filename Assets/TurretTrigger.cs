using UnityEngine;
using System.Collections;

public enum AppearingDirection{
	FROM_ABOVE,
	FROM_LEFT,
	FROM_RIGHT
}

public class TurretTrigger : MonoBehaviour {

	public GameObject hoveringTurret;
	public AppearingDirection direction;
	public float speed = 5f;

	// Use this for initialization
	void Start () {
		hoveringTurret.SetActive (false);
		ActivateTurret ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void onTriggerEnter(Collider other){
	//	if (other.gameObject.name == "OVRCameraRig") {
			Debug.Log ("Player triggered turret.");
			ActivateTurret();
	//	}
	}

	void ActivateTurret(){
		hoveringTurret.SetActive (true);
		hoveringTurret.GetComponent<ScriptTurret> ().isHostile = false;

		if (direction == AppearingDirection.FROM_ABOVE) {
			StartCoroutine(moveTurretDown());
		} else if (direction == AppearingDirection.FROM_LEFT) {
			StartCoroutine(moveTurretRight());
		} else {
			StartCoroutine(moveTurretLeft());
		}
	}

	IEnumerator moveTurretDown(){
		yield return null;
	}

	IEnumerator moveTurretRight(){
		yield return null;
	}

	IEnumerator moveTurretLeft(){
		yield return null;
	}
}
