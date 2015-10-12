using UnityEngine;
using System.Collections;

public class StrafeEnabler : MonoBehaviour {

    private script2ORMovement player;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider col)
    {
        print("Enabled Strafing");
        player = col.GetComponent<script2ORMovement>();
        player.canStrafe = true;
    }
}
