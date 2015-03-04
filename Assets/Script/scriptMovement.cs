using UnityEngine;
using System.Collections;

public class scriptMovement : MonoBehaviour {
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		float horizontal = Input.GetAxis("Horizontal")/2;
		
		Vector3 direction = new Vector3(horizontal, 0, 0.0f);
		transform.position += direction;
	}

	void OnTriggerEnter( Collider col )
	{
		Debug.Log ("TRIGGERED");
		pausePath ();
	}

	void pausePath()
	{
		iTween.Pause ();
	}

	void onTriggerExit( Collider col )
	{
		Debug.Log ("Exited");
		resumePath ();
	}
	
	void resumePath()
	{
		iTween.Resume ();
	}
	
	void OnCollisionEnter(Collision col) {
		Debug.Log (gameObject.name + " has collided with " + col.gameObject.name);
		
	}
}
