﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class scriptMovement : MonoBehaviour {
	
	public float playerSpeed = 1.0f;
	public bool hasCollisionInFront;
	public float collisionDist = 0.51f;
	public float rotateSpeed = 2.0f;
	public Transform[] path;
	public GameObject canvas;
	public GameManager gameManager;
	
	private CharacterController controller;
	private Vector3 direction;
	private float playerWidth;

	public GameObject mainCamera;
	private int currNode = 0;
	private Vector3 forwardDirection;
	private ColorCorrectionCurves[] cccObjects;
	private NoiseAndScratches[] nsObjects;
	
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		playerWidth = GetComponent<MeshRenderer>().bounds.size.x;
		forwardDirection = Vector3.Normalize (path[0].position - transform.position);
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		cccObjects = mainCamera.GetComponentsInChildren<ColorCorrectionCurves>();
		nsObjects = mainCamera.GetComponentsInChildren<NoiseAndScratches>();
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetMouseButtonDown(0))
		{
			meleeAttack();
		}
	
		if (Input.GetKeyDown (KeyCode.T))
		{
			Debug.Log (mainCamera.transform.GetChild(0).transform.position);
			Debug.Log (mainCamera.transform.GetChild(0).transform.rotation);
		}
		
		if (!gameManager.isGameOver && Input.GetKeyDown (KeyCode.W)) {
			//Debug.Log("Pressed Freeze " + isFrozen );
			
			StartCoroutine (glitchEffect ());
		}

		if (!gameManager.isFrozen) {
			//Time.timeScale = 1.0f; FOR SLOWMO PURPOSES.
			
			foreach (ColorCorrectionCurves obj in cccObjects) {
				obj.enabled = false;
			}
			
			float horizontal = Input.GetAxis ("Horizontal");
			
			direction = new Vector3 (horizontal, 0, 0);
			direction = transform.rotation * direction;
			//Debug.Log ("BEFORE: " + direction);
			
			if (!hasCollisionInFront)
				
				direction += forwardDirection;
			
			//direction = Vector3.Normalize(direction);
			//Debug.Log ("AFTER: " + direction);
			
			controller.SimpleMove (direction * playerSpeed);
			
			
			RaycastHit hit;
			Ray checkCollisionRayLeft = new Ray (transform.position + (-transform.right * (playerWidth / 2)) + transform.forward*playerWidth/2, transform.forward);
			Ray checkCollisionRayRight = new Ray (transform.position + (transform.right * (playerWidth / 2)) + transform.forward*playerWidth/2, transform.forward);
			Ray checkCollisionRayCenter = new Ray (transform.position + transform.forward*playerWidth/2 , transform.forward);
			
			if (Physics.Raycast (checkCollisionRayLeft, collisionDist, (1<<0)) 
			    || Physics.Raycast (checkCollisionRayRight, collisionDist, (1<<0))
			    || Physics.Raycast (checkCollisionRayCenter, collisionDist, (1<<0))) {
			    
					hasCollisionInFront = true;
				//				Debug.Log (hit.collider.gameObject.name);
			} else {
				hasCollisionInFront = false;
			}
			
			
		} 
		
		else {
			//Time.timeScale = 0.5f; FOR SLOWMO PURPOSES
			
			foreach (ColorCorrectionCurves obj in cccObjects) {
				obj.enabled = true;
			}
		}
		

		
	}
	
	void OnTriggerEnter( Collider col )
	{	
		GameObject.Destroy(col.gameObject);
		Debug.Log("working");
		currNode++;

		if (currNode == path.Length) {
			gameManager.isFrozen = true;
			gameManager.isGameOver = true;
			canvas.gameObject.transform.Find("WinScreen").gameObject.SetActive (true);
		}

		forwardDirection = Vector3.Normalize (path[currNode].position - path[currNode-1].position);
		Quaternion targetRotation = Quaternion.LookRotation (path[currNode].position - transform.position);
		StartCoroutine(RotateTowards(targetRotation));
		
	}

	IEnumerator glitchEffect() {
		
		foreach (NoiseAndScratches obj in nsObjects) {
			obj.enabled = true;
		}
		
		
		yield return new WaitForSeconds(0.5f);
		
		foreach (NoiseAndScratches obj in nsObjects) {
			obj.enabled = false;
		}
		
	}

	IEnumerator RotateTowards(Quaternion targetRotation) {
		
		float t;
		for (t = 0f; t<rotateSpeed; t+= Time.deltaTime ) {		
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, t/rotateSpeed);
			yield return null;
		}
		
	}

	void meleeAttack()
	{
		RaycastHit meleeHit;
		Debug.DrawRay (transform.position, mainCamera.transform.forward);
		if(Physics.Raycast(transform.position, mainCamera.transform.forward,  out meleeHit, 3.0f))
		{
			if(meleeHit.transform.gameObject.GetComponent<ScriptEnemy>() != null)
			{
				meleeHit.transform.gameObject.GetComponent<ScriptEnemy>().Die();
			}
		}
	}

}
