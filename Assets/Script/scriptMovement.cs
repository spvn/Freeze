using UnityEngine;
using System.Collections;

public class scriptMovement : MonoBehaviour {
	
	public float playerSpeed = 1.0f;
	
	
	private CharacterController controller;
	private Vector3 direction;
	public bool hasCollisionInFront;
	public float collisionDist = 0.5f;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
		float horizontal = Input.GetAxis("Horizontal")/2;
		
		direction = new Vector3(horizontal, 0, 0.0f);
		//transform.position += direction;
		controller.SimpleMove(direction * playerSpeed);
		
		
		RaycastHit hit;
		Ray checkCollisionRay = new Ray(transform.position, transform.forward);
		
		if (Physics.Raycast(checkCollisionRay, out hit, collisionDist)) {
			hasCollisionInFront = true;
		}
		else {
			hasCollisionInFront = false;
		}
		
	}

	/*void OnTriggerEnter( Collider col )
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
	}*/
	
	void OnCollisionEnter(Collision col) {
		Debug.Log (gameObject.name + " has collided with " + col.gameObject.name);
		
	}
}
