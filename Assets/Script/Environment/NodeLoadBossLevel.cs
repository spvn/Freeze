﻿using UnityEngine;
using System.Collections;

public class NodeLoadBossLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.gameObject.name == "OVRCameraRig")
		{
			Application.LoadLevel(5);
		}
	}
}
