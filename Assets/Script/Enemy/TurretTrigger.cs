using UnityEngine;
using System.Collections;

// TODO: collider check when moving left/right

public enum AppearingDirection{
	FROM_ABOVE,
	FROM_LEFT,
	FROM_RIGHT
}

public class TurretTrigger : MonoBehaviour {

	public GameObject hoveringTurret;
	public AppearingDirection direction;
	public float speed = 10f;
	public float distance;
	private bool startMoving;

	private float distanceTravelled;
	private Vector3 directionVector;

	// Use this for initialization
	void Start () {
		hoveringTurret.SetActive (false);
		startMoving = false;
		distanceTravelled = 0;

		if (direction == AppearingDirection.FROM_ABOVE) {
			directionVector = Vector3.down;
		} else if (direction == AppearingDirection.FROM_LEFT) {
			directionVector = Vector3.left;
		} else {
			directionVector = Vector3.right;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (startMoving && distanceTravelled <= distance) {
			moveTurret ();
		}

		if (distanceTravelled >= distance) {
			hoveringTurret.GetComponent<ScriptTurret> ().isHostile = true;
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "OVRCameraRig") {
			Debug.Log ("Player triggered turret.");
			ActivateTurret();
		}
	}

	void ActivateTurret(){
		hoveringTurret.SetActive (true);
		hoveringTurret.GetComponent<ScriptTurret> ().isHostile = false;

		startMoving = true;
	}

	void moveTurret(){
		distanceTravelled += (directionVector * speed * Time.deltaTime).magnitude;
		hoveringTurret.transform.Translate(directionVector * speed * Time.deltaTime, Space.World);
		Debug.Log ("Distance travelled: " + distanceTravelled);
	}

}
