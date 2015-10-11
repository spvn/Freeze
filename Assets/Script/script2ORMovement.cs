using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

//TODO: movement based on events

public class script2ORMovement : MonoBehaviour {

    // Player movement variables
    public float playerSpeed = 4.0f;
	public float lateralSpeed = 5.0f;
    public float rotateSpeed = 2.0f;
    private Vector3 sideDirection;
    private Vector3 forwardDirection;
    // For testing of jump
    public float jumpSpeed = 10.0f;
    public float gravity = 9.81f;
    private float verticalVelocity = 0f;
    private Vector3 moveDirection;

    // Essential GameObjects 
    private GameManager gameManager;
    private CharacterController controller;
    private ColorCorrectionCurves[] cccObjects;
    private NoiseAndScratches[] nsObjects;

    //public bool hasCollisionInFront;
    //public float collisionDist = 0.51f;

    public Transform[] path;
	public GameObject canvas;
    private int currNode = 0;
    public Transform currentNode;
    public Transform destinationNode;

    //private float playerWidth;
	//private Vector3 lastFramePos;
	//private Vector3 currFramePos;
	//Vector3 oculusMovement;
    //private Animator playerAnimator;

    void Start () {
		controller = GetComponent<CharacterController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        cccObjects = GetComponentsInChildren<ColorCorrectionCurves>();
        nsObjects = GetComponentsInChildren<NoiseAndScratches>();

        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);
        currentNode = transform;
        destinationNode = null;
        //forwardDirection = Vector3.Normalize (path[0].position - transform.position);

        //lastFramePos = transform.GetChild(1).transform.localPosition;
        //playerAnimator = this.transform.Find ("playerAnimator").GetComponent<Animator> ();
        //playerWidth = controller.radius * 2;
    }

    // Update is called once per frame
    void Update() {

        if (!gameManager.isGameOver && (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            //Debug.Log("Pressed Freeze " + isFrozen );
            StartCoroutine(glitchEffect());
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton5)))
        {
            OVRManager.display.RecenterPose();
        }

        if (destinationNode != null)
        {
            moveToNextNode();
        }

        if (gameManager.isGameOver || gameManager.isFrozen)
        {
            return;
        }
        /*
        if (gameManager.isChoosingPath)
        {
            print("Player Script: Is choosing path");
            if (destinationNode = null)
            {
                // Active script running in NodePathing.cs while player is choosing path
                //return;
            }
            else
            {
                // Player has chosen path and node script has supplied destination node
                gameManager.isFrozen = false;
                gameManager.isChoosingPath = false;
                moveToNextNode(destinationNode);
            }
        }*/

        // Jump
        if (controller.isGrounded && !gameManager.isFrozen)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= playerSpeed;
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        // Gravity
        if (!gameManager.isGameOver || !gameManager.isFrozen)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }

        // Forward and lateral movement
        if (!gameManager.isFrozen)
        {
            //Time.timeScale = 1.0f; FOR SLOWMO PURPOSES.
            float horizontal = Input.GetAxis("Horizontal");
            sideDirection = new Vector3(horizontal, 0, 0);
            sideDirection = transform.rotation * sideDirection;
            print("Forward: " + forwardDirection);
            controller.SimpleMove((forwardDirection * playerSpeed) + (sideDirection * lateralSpeed));
        }

        

        // Get position of player's hit area
		//currFramePos = transform.GetChild(1).transform.localPosition;
		
		if( Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown (KeyCode.JoystickButton1)) {
			meleeAttack();
		}
		
		
        
        /*
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
				
			}else {
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
        */
        //lastFramePos = currFramePos;
	}

	void OnTriggerEnter( Collider col )
	{	
        // if player collides with a node
		if (col.gameObject.layer == 10) {
            // access node script
            NodePathing node = col.GetComponent<NodePathing>();
            if(node.isEndNode)
            {
                gameManager.isFrozen = true;
                gameManager.isGameOver = true;
                canvas.gameObject.transform.Find("WinScreen").gameObject.SetActive(true);
            }
            else if(node.hasMultiplePath)
            {
                print("Player Script: Node has multiple paths");
                gameManager.isFrozen = true;
                gameManager.isChoosingPath = true;
            }
            // single path: get next node and continue travelling
            else
            {
                print("Player Script: Getting next single path node");
                destinationNode = node.getNextDestinationSinglePath();
                moveToNextNode();
            }

            /*
			col.gameObject.SetActive(false);
			//GameObject.Destroy(col.gameObject);
			//Debug.Log("working");
			currNode++;
	
			if (currNode == path.Length) {
				gameManager.isFrozen = true;
				gameManager.isGameOver = true;
				canvas.gameObject.transform.Find("WinScreen").gameObject.SetActive (true);
			}
	
			forwardDirection = Vector3.Normalize (path[currNode].position - path[currNode-1].position);
			Quaternion targetRotation = Quaternion.LookRotation (path[currNode].position - transform.position);
			StartCoroutine(RotateTowards(targetRotation));
            */
		}	
	}

    public void moveToNextNode()
    {
        forwardDirection = Vector3.Normalize(destinationNode.position - transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(destinationNode.position - transform.position);
        StartCoroutine(RotateTowards(targetRotation));
        currentNode = destinationNode;
        destinationNode = null;
        gameManager.isFrozen = false;
        gameManager.isChoosingPath = false;
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

	public int getCurrentState()
	{
		return currNode;
	}
}
