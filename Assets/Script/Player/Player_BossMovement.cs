using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Player_BossMovement : MonoBehaviour {

    public float rotationSpeed = 1.0f;
	public float playerSpeed = 0.0f;
    public float jumpSpeed = 10f;
    public float gravity = 1.81f;
    public GameObject focalPoint;
    public LevelManager levelManager;

    private NoiseAndScratches[] nsObjects;
    private CharacterController controller;
    private Vector3 downDirection = Vector3.zero;
    private float continueRotation;

    void Start () {
        continueRotation = 0; // to be used while in the air
        nsObjects = GetComponentsInChildren<NoiseAndScratches>();
        controller = GetComponent<CharacterController>();
    }

	void Update () {
		Debug.Log (controller.isGrounded);
        if (levelManager.isGameOver || levelManager.isFrozen || levelManager.isPause)
        {
            return;
        }
        
		if ( Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.JoystickButton0) )
        {
            StartCoroutine(glitchEffect());
        }

		if ( Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            PulseAttack attack = gameObject.GetComponent<PulseAttack>();
            attack.Pulse();
        }

	    if (( Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0) && controller.isGrounded )
        {
            continueRotation = rotationSpeed; // will be used to continue rotation in the air if player jumps
            focalPoint.transform.Rotate(new Vector3(0, rotationSpeed, 0));
        }
		else if ( ( Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0) && controller.isGrounded )
        {
            continueRotation = -rotationSpeed; // will be used to continue rotation in the air if player jumps
            focalPoint.transform.Rotate(new Vector3(0, -rotationSpeed, 0));
        } else if (controller.isGrounded)
        {
            continueRotation = 0; // no rotation by default if player jumps without moving
        }

		if (controller.isGrounded && (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.JoystickButton2)))
        {
            downDirection = new Vector3(0, jumpSpeed, 0);
        }
        updateFrameMovement();
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

    void jump()
    {
        downDirection = new Vector3(0, jumpSpeed, 0);
    }

    void updateFrameMovement()
    {
        downDirection.y -= gravity * Time.deltaTime;
        controller.Move(downDirection * Time.deltaTime);
        // Player in air will continue moving
        if (!controller.isGrounded)
        {
            focalPoint.transform.Rotate(new Vector3(0, continueRotation, 0));
        }
    }
}
