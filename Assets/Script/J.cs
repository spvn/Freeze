using UnityEngine;
using System.Collections;

public class J : MonoBehaviour {
	int time=100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		this.transform.position += new Vector3 (-0.1f, 0.0f, 0.0f) * Time.deltaTime;
	
	}

	void OnTriggerEnter(Collider col){
		Destroy (this.gameObject);
	}
}
