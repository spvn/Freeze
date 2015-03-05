using UnityEngine;
using System.Collections;

public class scriptMovement : MonoBehaviour {
	
	public float playerSpeed = 1.0f;
	public bool hasCollisionInFront;
	public float collisionDist = 0.51f;
	
	
	private CharacterController controller;
	private Vector3 direction;
	private float playerWidth;
	
	
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		playerWidth = GetComponent<MeshRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
	
		float horizontal = Input.GetAxis("Horizontal")/2;
		
		direction = new Vector3(horizontal, 0, 0.0f);
		//transform.position += direction;
		controller.SimpleMove(direction * playerSpeed);
		
		
		RaycastHit hit;
		Ray checkCollisionRayLeft = new Ray(transform.position - new Vector3(playerWidth/2,0,0), transform.forward);
		Ray checkCollisionRayRight = new Ray(transform.position + new Vector3(playerWidth/2,0,0), transform.forward);
		Ray checkCollisionRayCenter = new Ray(transform.position, transform.forward);
		
		if (Physics.Raycast(checkCollisionRayLeft, out hit, collisionDist) 
			|| Physics.Raycast(checkCollisionRayRight, out hit, collisionDist)
		    || Physics.Raycast(checkCollisionRayCenter, out hit, collisionDist)) {
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
