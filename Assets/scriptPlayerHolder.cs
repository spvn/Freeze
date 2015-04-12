using UnityEngine;
using System.Collections;

public class scriptPlayerHolder : MonoBehaviour {
	
	public Transform[] pathNodes;
	public float moveSpeed = 1.0f;
	public float rotateSpeed = 2.0f;
	
	
	private int currNode = 0;
	private Transform player;
	private bool isRotated = true;
	private Vector3 targetVectorRotation;
	
	
	// Use this for initialization
	void Start () {
		player = transform.Find("Player");
		transform.LookAt (pathNodes[0].position);
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion targetRotation;
//		if(!player.GetComponent<scriptMovement>().isFrozen){
			if (transform.position == pathNodes[currNode].position) {
				currNode++;
				targetRotation = Quaternion.LookRotation (pathNodes[currNode].position - player.transform.position);
			
				
				//player.transform.rotation = Quaternion.Slerp (player.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
				targetVectorRotation = pathNodes[currNode].position - player.transform.position;
				StartCoroutine(RotateTowards(targetRotation));

			}
			
			if (!player.GetComponent<scriptMovement>().hasCollisionInFront) {
				transform.position = Vector3.MoveTowards (transform.position, pathNodes[currNode].position, moveSpeed * Time.deltaTime);
			}
//		}
		
		
	}
	
	IEnumerator RotateTowards(Quaternion targetRotation) {
		
		float t;
		for (t = 0f; t<rotateSpeed; t+= Time.deltaTime ) {		
			player.transform.rotation = Quaternion.Slerp (player.transform.rotation, targetRotation, t/rotateSpeed);
			Debug.Log (player.transform.rotation + " " + targetRotation);
			yield return null;
		}

	}
}
