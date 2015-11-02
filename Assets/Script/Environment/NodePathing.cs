/* 
 *  Attach script to pathing nodes. 
 */

using UnityEngine;
using System.Collections;

public class NodePathing : MonoBehaviour {

    public Transform LeftDestinationNode;
    public Transform MiddleDestinationNode;
    public Transform RightDestinationNode;
    public int numRoutes = 0;              // number of forked destination nodes
    public bool hasMultiplePath = false;   // default node has 1 destination node
    public bool isEndNode = false;         // default node is not an end node
    public bool playerInRange = false;
    public bool disableStrafe = false;
    public bool isCheckpoint = false;
    private LevelManager levelManager;

    // Use this for initialization
    void Start () {
        // Preprocess data
        countRoutes();
        if (numRoutes > 1 && numRoutes != 0) hasMultiplePath = true;
        if (numRoutes == 0) isEndNode = true;

        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.A) && LeftDestinationNode != null)
            {
                print("Selected Left Path");
                sendNodeToPlayer(LeftDestinationNode);  
            }
            else if (Input.GetKeyDown(KeyCode.W) && MiddleDestinationNode != null)
            {
                print("Selected Middle Path");
                sendNodeToPlayer(MiddleDestinationNode);
            }
            else if (Input.GetKeyDown(KeyCode.D) && RightDestinationNode != null)
            {
                print("Selected Right Path");
                sendNodeToPlayer(RightDestinationNode);
            }
        }
	}

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.name == "OVRCameraRig")
        {
            playerInRange = true;
        }
    }
    
    void OnTriggerExit ()
    {
        playerInRange = false;
        //gameObject.SetActive(false);
    }

    private void countRoutes()
    {
        if (LeftDestinationNode != null) numRoutes++;
        if (MiddleDestinationNode != null) numRoutes++;
        if (RightDestinationNode != null) numRoutes++;
    }

    private void sendNodeToPlayer(Transform destinationNode)
    {
        print("Sending node to player");
        GameObject.Find("OVRCameraRig").GetComponent<script2ORMovement>().destinationNode = destinationNode;
        gameObject.SetActive(false);
    }

    public Transform getNextDestinationSinglePath()
    {
        return MiddleDestinationNode;
    }
}
