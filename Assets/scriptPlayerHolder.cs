using UnityEngine;
using System.Collections;

public class scriptPlayerHolder : MonoBehaviour {
	
	public Transform[] pathNodes;
	public float moveSpeed = 1.0f;
	public float rotateSpeed = 2.0f;
	
	
	private int currNode = 0;
	private Transform player;
	private bool isRotated = true;
	
	
	// Use this for initialization
	void Start () {
		player = transform.Find("Player");
		transform.LookAt (pathNodes[0].position);
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion targetRotation;
		if(!player.GetComponent<scriptMovement>().isFrozen){
			if (transform.position == pathNodes[currNode].position) {
				currNode++;
				
			}
			
			if (!player.GetComponent<scriptMovement>().hasCollisionInFront) {
				transform.position = Vector3.MoveTowards (transform.position, pathNodes[currNode].position, moveSpeed * Time.deltaTime);
			}
		}
		
		targetRotation = Quaternion.LookRotation (pathNodes[currNode].position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
	}
}
