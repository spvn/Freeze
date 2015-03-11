using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool isFrozen;
	// Use this for initialization
	void Start () {
		isFrozen = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			isFrozen = !isFrozen;
		}
	
	}
	
}
