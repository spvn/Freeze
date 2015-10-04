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
    private bool playerInRange = false;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        // Preprocessing data
        countRoutes();
        if (numRoutes != 1) hasMultiplePath = true;
        if (numRoutes == 0) isEndNode = true;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if(playerInRange)
        {
            if(Input.GetKeyDown(KeyCode.A) && LeftDestinationNode != null)
            {
                // send left node position to player
            }
            else if (Input.GetKeyDown(KeyCode.W) && MiddleDestinationNode != null)
            {
                // send middle node position to player
            }
            else if (Input.GetKeyDown(KeyCode.D) && RightDestinationNode != null)
            {
                // send right node position to player
            }

        }
	}

    void OnTriggerEnter ()
    {
        playerInRange = true;
        if (hasMultiplePath)
        {
            gameManager.FreezeGame();
            // TODO: Player UI displays options
            // UI.displayPathChoice(hasLeft, hasMid, hasRight);
        }
    }
    
    void OnTriggerExit ()
    {
        playerInRange = false;
    }

    private void countRoutes()
    {
        if (LeftDestinationNode != null) numRoutes++;
        if (MiddleDestinationNode != null) numRoutes++;
        if (RightDestinationNode != null) numRoutes++;
    }
}
