using UnityEngine;
using System.Collections;

public class scriptPlayerHolder : MonoBehaviour {
	
	public Transform[] pathNodes;
	public float moveSpeed = 1.0f;
	
	
	private int currNode = 0;
	private Transform player;
	
	
	// Use this for initialization
	void Start () {
		player = transform.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(!player.GetComponent<scriptMovement>().isFrozen){
			if (transform.position == pathNodes[currNode].position) {
				currNode++;
			}
			
			if (!player.GetComponent<scriptMovement>().hasCollisionInFront) {
				transform.position = Vector3.MoveTowards (transform.position, pathNodes[currNode].position, moveSpeed * Time.deltaTime);
			}
		}
	}
}
