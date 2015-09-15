using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class script2ORMovement : MonoBehaviour {
	
	public float playerSpeed = 1.0f;
	public float lateralSpeed = 5.0f;
	public bool hasCollisionInFront;
	public float collisionDist = 0.51f;
	public float rotateSpeed = 2.0f;
	public Transform[] path;
	public GameObject canvas;
	public GameManager gameManager;
	
	private CharacterController controller;
	private Vector3 sideDirection;
	private float playerWidth;

	private int currNode = 0;
	private Vector3 forwardDirection;
	private ColorCorrectionCurves[] cccObjects;
	private NoiseAndScratches[] nsObjects;
	private Vector3 lastFramePos;
	private Vector3 currFramePos;
	Vector3 oculusMovement;
    //private Animator playerAnimator;

    // For testing of jump
    public float jumpSpeed = 10.0f;
    public float gravity = 9.81f;
    private float verticalVelocity = 0f;
    private Vector3 moveDirection;

    // Use this for initialization
    void Start () {
		controller = GetComponent<CharacterController>();
		playerWidth = controller.radius * 2;
		forwardDirection = Vector3.Normalize (path[0].position - transform.position);
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
		cccObjects = GetComponentsInChildren<ColorCorrectionCurves>();
		nsObjects = GetComponentsInChildren<NoiseAndScratches>();
		//playerAnimator = this.transform.Find ("playerAnimator").GetComponent<Animator> ();
		
		lastFramePos = transform.GetChild(1).transform.localPosition;
		
		
	}

	// Update is called once per frame
	void Update () {

        // Jump
        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                verticalVelocity = jumpSpeed;
                //transform.Translate(Vector3.up * jumpSpeed * 0.5f * Time.smoothDeltaTime); // This one will teleport to highest point then drop down
            }
        }
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= playerSpeed;
        // if player jumps
        verticalVelocity -= gravity * Time.deltaTime; // vertical speeds decrease over time due to gravity
        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);

        if ((Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.JoystickButton5))) {
			OVRManager.display.RecenterPose();  
		} 

        // Get position of player's hit area
		currFramePos = transform.GetChild(1).transform.localPosition;
		

		if( Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown (KeyCode.JoystickButton1)) {
			meleeAttack();
		}
		
		if (!gameManager.isGameOver && (Input.GetKeyDown (KeyCode.J) || Input.GetKeyDown (KeyCode.JoystickButton0))) {
			//Debug.Log("Pressed Freeze " + isFrozen );
			
			StartCoroutine (glitchEffect ());
		}

		if (!gameManager.isFrozen) {
			//Time.timeScale = 1.0f; FOR SLOWMO PURPOSES.
			
			foreach (ColorCorrectionCurves obj in cccObjects) {
				obj.enabled = false;
			}
			
			float horizontal = Input.GetAxis ("Horizontal");
			
			sideDirection = new Vector3 (horizontal, 0, 0);
			sideDirection = transform.rotation * sideDirection;
			//Debug.Log ("BEFORE: " + direction);
			
			if (!hasCollisionInFront) {
				controller.SimpleMove ((forwardDirection * playerSpeed) + (sideDirection * lateralSpeed));
				
			}
			else {
				controller.SimpleMove ((sideDirection * lateralSpeed));
			}

            //direction = Vector3.Normalize(direction);
            //Debug.Log ("AFTER: " + direction);
            //oculusMovement = lastFramePos - currFramePos;

            //Debug.Log ((direction * playerSpeed));

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
			oculusMovement = lastFramePos - currFramePos;
			
			
			
			foreach (ColorCorrectionCurves obj in cccObjects) {
				obj.enabled = true;
			}
		}
		

		lastFramePos = currFramePos;
	}
	
	void OnTriggerEnter( Collider col )
	{	
		if (col.gameObject.layer == 10) {
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
		//playerAnimator.Play ("R punch");
		if(Physics.Raycast(transform.position, transform.forward,  out meleeHit, 3.0f))
		{
			if(meleeHit.transform.gameObject.GetComponent<ScriptEnemy>() != null)
			{
				meleeHit.transform.gameObject.GetComponent<ScriptEnemy>().Die();
			}
		}
	}

}
