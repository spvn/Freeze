using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DoorControl : MonoBehaviour {

	public float angleOpened;
	public float openingSpeed;
	public bool rotateClockwiseToOpen;
	//public AudioSource doorOpeningSound;
	public bool triggerDoorOpen;

	private float angleClosed;
	private bool doorIsOpened;

	void Start () {
		doorIsOpened = false;
		triggerDoorOpen = true;
		angleClosed = transform.localEulerAngles.y;
	}

	void Update () {
		if (triggerDoorOpen && !doorIsOpened){
			openDoor();
		}
	}

	private void openDoor()
	{
		if (rotateClockwiseToOpen) {
			if (transform.localEulerAngles.y < angleOpened) { 
				transform.RotateAround (transform.position, Vector3.up, openingSpeed * Time.deltaTime);
			} else {
				doorIsOpened = true;
			}
		} else {
			if (transform.localEulerAngles.y == 0) {
				transform.RotateAround (transform.position, Vector3.up, -1 * openingSpeed * Time.deltaTime);
			}

			if (transform.localEulerAngles.y > angleOpened) { 
				//Debug.Log (transform.localEulerAngles.y);
				transform.RotateAround (transform.position, Vector3.up, -1 * openingSpeed * Time.deltaTime);
			} else {
				doorIsOpened = true;
			}
		}
	}
}
