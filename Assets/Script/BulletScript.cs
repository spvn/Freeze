using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	Vector3 targetPoint;
	public float duration;
	public scriptMovement playerMovement;
	public LineRenderer bulletLine;

	Vector3 bulletDirection = Vector3.zero;

	// Use this for initialization
	void Start () {

	}

	public void setBulletDirection(Vector3 targetVector)
	{
		targetPoint = targetVector;
		bulletDirection = targetPoint - this.transform.localPosition;
		bulletLine.SetPosition (0, this.transform.localPosition);
		bulletLine.SetPosition (1, targetPoint);
	}
	// Update is called once per frame
	void Update () {

		if ( bulletDirection != Vector3.zero && !playerMovement.isFrozen) {
			this.transform.localPosition += Vector3.Normalize(bulletDirection) * duration * Time.deltaTime;
		}
		if ( Mathf.Abs(this.transform.localPosition.z) > Mathf.Abs(playerMovement.gameObject.transform.parent.transform.localPosition.z - 1) ) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter( Collision col )
	{	
		Debug.Log ("Collided bullet " + col.gameObject.name + " at " + this.transform.localPosition.ToString());

		if (col.gameObject.name != "PlayerHolder") {
			Debug.Log("Deleted bullet");
			Destroy(gameObject);
		}
	}
}
