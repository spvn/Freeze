using UnityEngine;
using System.Collections;

public class scriptMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis("Horizontal")/2;
		
		Vector3 direction = new Vector3(horizontal, 0, 0.1f);
		transform.position += direction;
	}
}
