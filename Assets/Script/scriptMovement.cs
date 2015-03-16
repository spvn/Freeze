using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class scriptMovement : MonoBehaviour {
	
	public float playerSpeed = 1.0f;
	public bool hasCollisionInFront;
	public float collisionDist = 0.51f;
	public float rotateSpeed = 2.0f;
	public Transform[] path;
	
	
	private CharacterController controller;
	private Vector3 direction;
	private float playerWidth;

	public bool isFrozen;
	public GameObject mainCamera;
	private int currNode = 0;
	private Vector3 forwardDirection;
	
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		playerWidth = GetComponent<MeshRenderer>().bounds.size.x;
		isFrozen = true;
		forwardDirection = Vector3.Normalize (path[0].position - transform.position);
		Debug.Log ("forward: " + forwardDirection);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.W)) {
			Debug.Log("Pressed Freeze " + isFrozen );
			isFrozen = !isFrozen;
		}

		if (!isFrozen) {
			mainCamera.GetComponent<BlurOptimized>().enabled = false;
			float horizontal = Input.GetAxis ("Horizontal");
			
			direction = new Vector3 (horizontal, 0, 0);
			direction = transform.rotation * direction;
			//Debug.Log ("BEFORE: " + direction);
			
			direction += forwardDirection;
			
			//direction = Vector3.Normalize(direction);
			//Debug.Log ("AFTER: " + direction);
			controller.SimpleMove (direction * playerSpeed);
			
			
			RaycastHit hit;
			Ray checkCollisionRayLeft = new Ray (transform.position - new Vector3 (playerWidth / 2, 0, 0) + transform.forward*playerWidth, transform.forward);
			Ray checkCollisionRayRight = new Ray (transform.position + new Vector3 (playerWidth / 2, 0, 0) + transform.forward*playerWidth, transform.forward);
			Ray checkCollisionRayCenter = new Ray (transform.position + transform.forward*playerWidth , transform.forward);
			
			if (Physics.Raycast (checkCollisionRayLeft, out hit, collisionDist) 
				|| Physics.Raycast (checkCollisionRayRight, out hit, collisionDist)
				|| Physics.Raycast (checkCollisionRayCenter, out hit, collisionDist)) {
				hasCollisionInFront = true;
				Debug.Log (hit.collider.gameObject.name);
			} else {
				hasCollisionInFront = false;
			}
		} 
		
		else {
			mainCamera.GetComponent<BlurOptimized>().enabled = true;
		}
		
		
		
	}

	void OnTriggerEnter( Collider col )
	{	
		GameObject.Destroy(col.gameObject);
		Debug.Log("working");
		currNode++;
		forwardDirection = Vector3.Normalize (path[currNode].position - path[currNode-1].position);
		Quaternion targetRotation = Quaternion.LookRotation (path[currNode].position - transform.position);
		StartCoroutine(RotateTowards(targetRotation));
		
	}
	
	IEnumerator RotateTowards(Quaternion targetRotation) {
		
		float t;
		for (t = 0f; t<rotateSpeed; t+= Time.deltaTime ) {		
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, t/rotateSpeed);
			yield return null;
		}
		
	}
	
}
