using UnityEngine;
using System.Collections;

public class scriptBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision col) {
		Debug.Log (gameObject.name + " has collided with " + col.gameObject.name);
	
	}
}
