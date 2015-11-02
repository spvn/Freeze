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

    public Transform currentNode;
    public Transform destinationNode;

    private Vector3 sideDirection;
    private Vector3 forwardDirection;
    private Vector3 downDirection;

    // Essential GameObjects 
    private LevelManager levelManager;
    private CharacterController controller;
    //private ColorCorrectionCurves[] cccObjects;
    private NoiseAndScratches[] nsObjects;
    public GameObject canvas;

    void Start () {
		controller = GetComponent<CharacterController>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
        //cccObjects = GetComponentsInChildren<ColorCorrectionCurves>();
        nsObjects = GetComponentsInChildren<NoiseAndScratches>();

        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);
        currentNode = transform;
        destinationNode = null;
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
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.LeftAlt))
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


            if (node.isEndNode)
            {
                levelManager.isFrozen = true;
                levelManager.isGameOver = true;
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

    public void moveToNextNode()
    {
        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(destinationNode.position - transform.position);
        StartCoroutine(RotateTowards(targetRotation));
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
        downDirection.y -= gravity * Time.deltaTime;

        // Forward and lateral movement
        //Time.timeScale = 1.0f; FOR SLOWMO PURPOSES.
        float horizontal = Input.GetAxis("Horizontal");
        if (canStrafe == false)
        {
            horizontal = 0; // strafe disabled
        }
        sideDirection = new Vector3(horizontal, 0, 0);
        sideDirection = transform.rotation * sideDirection;

        controller.Move(downDirection * Time.deltaTime);
        controller.SimpleMove((forwardDirection * playerSpeed) + (sideDirection * lateralSpeed));
    }

    void pulseAttack()
    {
        PulseAttack attack = gameObject.GetComponent<PulseAttack>();
		attack.Pulse ();
    }
}
