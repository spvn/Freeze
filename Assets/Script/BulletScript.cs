using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	Transform targetPoint;
	public GameObject player;
	public float duration = 0.0000001f;

	// Use this for initialization
	void Start () {
		targetPoint = player.transform;
		Debug.Log ("Targeting at " + player.transform.localPosition.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = Vector3.Lerp (this.transform.localPosition, targetPoint.localPosition, Time.deltaTime * duration);
	}

	void OnCollisionEnter( Collision col )
	{
		Debug.Log ("Deleting bullet " + col.gameObject.name + " at " + this.transform.localPosition.ToString());
		Destroy(gameObject);
	}
}
