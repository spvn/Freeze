using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

//TODO: movement based on events

public class script2ORMovement : MonoBehaviour {

    // Player movement variables
    public float playerSpeed = 4.0f;
	public float lateralSpeed = 5.0f;
    public float rotateSpeed = 2.0f;
    public float jumpSpeed = 10.0f;
    public float gravity = 9.81f;
    public bool canStrafe = true;
	public bool isFreefall = false;

    public Transform currentNode;
    public Transform destinationNode;

    private Vector3 sideDirection;
    private Vector3 forwardDirection;
	private Vector3 downDirection = new Vector3(0, 0, 0);
	private float tempPlayerSpeed;
    private float previousFrameHorizontal;

    // Essential GameObjects 
    private LevelManager levelManager;
    private CharacterController controller;
    //private ColorCorrectionCurves[] cccObjects;
    private NoiseAndScratches[] nsObjects;
    public GameObject canvas;

	// Feedback to enemies
	public float slopeAngle;
	public int slopeStatus;	// -1: down slope, 0: flat, 1: up slope

    void Start () {
		controller = GetComponent<CharacterController>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        //cccObjects = GetComponentsInChildren<ColorCorrectionCurves>();
        nsObjects = GetComponentsInChildren<NoiseAndScratches>();

        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);
        currentNode = transform;
        destinationNode = null;
		tempPlayerSpeed = playerSpeed;
    }

    // Update is called once per frame
    void Update() {

        if (destinationNode != null)
        {
            moveToNextNode();
        }

        if (levelManager.isChoosingPath)
        {
            return;
        }

        if (!levelManager.isGameOver && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            if (!levelManager.isPause)
				StartCoroutine(glitchEffect());
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton5)))
        {
            OVRManager.display.RecenterPose();
        }   

        if (levelManager.isGameOver || levelManager.isFrozen)
        {
            return;
        }


        // Player can do these actions if game is not paused
		if (controller.isGrounded && (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            jump();
        }

        updateFrameMovement();

		if( Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown (KeyCode.JoystickButton1)) {
            //meleeAttack();
            pulseAttack();
		}
	}

	void OnTriggerEnter( Collider col )
	{	
        // if player collides with a node
		if (col.gameObject.layer == 10) {
			Debug.Log (col.name);
            // access node script
            NodePathing node = col.GetComponent<NodePathing>();

            // this node will disable strafing until it reaches a point where it is unable to go backwards through strafing
            if (node.disableStrafe == true)
            {
                canStrafe = false;
            }

            if (node.isCheckpoint == true)
            {
                levelManager.playerCheckpoint = col.gameObject;
            }

			if (node.isFreefall == true)
			{
				isFreefall = true;
				playerSpeed = 0;
			}

			else
			{
				isFreefall = false;
				playerSpeed = tempPlayerSpeed;
			}


            if (node.isEndNode)
            {
                levelManager.isFrozen = true;
                levelManager.isGameOver = true;
				levelManager.LevelCleared();
                canvas.gameObject.transform.Find("WinScreen").gameObject.SetActive(true);

            }
            else if (node.hasMultiplePath)
            {
                print("Player Script: Node has multiple paths");
                levelManager.isFrozen = true;
                levelManager.isChoosingPath = true;
            }
            // single path: get next node and continue travelling
            else
            {
                destinationNode = node.getNextDestinationSinglePath();
                moveToNextNode();
            }
		}	
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		slopeAngle = Vector3.Angle(Vector3.up, hit.normal);

		if (slopeAngle <= 1) {
			slopeStatus = 0;	// flat
		} else {
			float genAngle = Vector3.Angle(forwardDirection, hit.normal);
			if (genAngle < 90){
				slopeStatus = -1;	// going down slope
			} else {
				slopeStatus = 1;	// going up slope
			}
		}
		//Debug.Log("slope status: " + slopeStatus + " Slope Angle: " + slopeAngle);
	}

    public void moveToNextNode()
    {
        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);

		//if is freefall node, dont need to rotate
		if (!isFreefall) {
			Vector3 destinationNodePosition = destinationNode.position;
			destinationNodePosition.y = transform.position.y;
			Quaternion targetRotation = Quaternion.LookRotation (destinationNodePosition - transform.position);
			Debug.Log (targetRotation);
			StartCoroutine (RotateTowards (targetRotation));
		}
        currentNode = destinationNode;
        destinationNode = null;
        levelManager.isFrozen = false;
        levelManager.isChoosingPath = false;
    }

	IEnumerator glitchEffect()
    {
		foreach (NoiseAndScratches obj in nsObjects)
        {
			obj.enabled = true;
		}
		yield return new WaitForSeconds(0.5f);
		foreach (NoiseAndScratches obj in nsObjects)
        {
			obj.enabled = false;
		}		
	}

	IEnumerator RotateTowards(Quaternion targetRotation)
    {
		float t;
		for (t = 0f; t < rotateSpeed; t += Time.deltaTime)
        {		
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, t / rotateSpeed);
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

    void jump()
    {
        downDirection = new Vector3(0, 0, /*Input.GetAxis("Vertical")*/0);
        //downDirection = transform.TransformDirection(downDirection);
        //downDirection *= playerSpeed;
        downDirection.y = jumpSpeed;
    }

    void updateFrameMovement()
    {
        // Gravity for downward movement
		if (!controller.isGrounded)
			downDirection.y -= gravity * Time.deltaTime;
        // Forward and lateral movement
        //Time.timeScale = 1.0f; FOR SLOWMO PURPOSES.
        float horizontal = Input.GetAxis("Horizontal");
        // Track previous frame side direction
        if (controller.isGrounded)
        {
            previousFrameHorizontal = horizontal; // grounded: track horizontal
        }
        else
        {
            horizontal = previousFrameHorizontal; // in the air: use last frame's horizontal to prevent strafing in air
        }

        if (canStrafe == false)
        {
            horizontal = 0; // strafe disabled
        }
        sideDirection = new Vector3(horizontal, 0, 0);
        sideDirection = transform.rotation * sideDirection;

		controller.Move ((forwardDirection * playerSpeed * Time.deltaTime) + (sideDirection * lateralSpeed * Time.deltaTime) + (downDirection * Time.deltaTime));
    }

    void pulseAttack()
    {
        PulseAttack attack = gameObject.GetComponent<PulseAttack>();
		attack.Pulse ();
    }

	public Vector3 getForwardDirection(){
		return forwardDirection;
	}
}
