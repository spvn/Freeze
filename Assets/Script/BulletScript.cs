using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	Vector3 targetPoint;
	public float duration = 0.0000001f;

	Vector3 bulletDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

	}

	public void setBulletDirection(Vector3 targetVector)
	{
		targetPoint = targetVector;
		bulletDirection = targetPoint - this.transform.localPosition;
	}
	// Update is called once per frame
	void Update () {

		if ( bulletDirection != Vector3.zero ) {
			this.transform.localPosition += bulletDirection * Time.deltaTime * duration;
		}
	}

	void OnCollisionEnter( Collision col )
	{
		Debug.Log ("Deleting bullet " + col.gameObject.name + " at " + this.transform.localPosition.ToString());
		Destroy(gameObject);
	}
}
