using UnityEngine;
using System.Collections;

public class NodePathing : MonoBehaviour {

    public bool hasMultiplePath;
    public Transform destinationNode1;
    public Transform destinationNode2;
    public Transform destinationNode3;


    // Use this for initialization
    void Start () {
        print (destinationNode1);
        int numRoutes = countPaths();
        if (numRoutes != 1)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private int countPaths()
    {
        return 0;
    }
}
