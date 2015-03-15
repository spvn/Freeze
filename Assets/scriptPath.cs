using UnityEngine;
using System.Collections;

public class scriptPath : MonoBehaviour {
	
	public Transform[] path;
	public float moveSpeed = 5.0f;
	public float rotateSpeed = 2.0f;
	
	private int currNode;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, path[currNode].position, moveSpeed * Time.deltaTime);
	}
	
	void OnTriggerEnter( Collider col )
	{
		currNode++;
		Quaternion targetRotation = Quaternion.LookRotation (path[currNode].position - transform.position);
		StartCoroutine(RotateTowards(targetRotation));
	}
	
	IEnumerator RotateTowards(Quaternion targetRotation) {
		
		float t;
		for (t = 0f; t<rotateSpeed; t+= Time.deltaTime ) {		
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, t/rotateSpeed);
			Debug.Log (transform.rotation + " " + targetRotation);
			yield return null;
		}
		
	}
}
