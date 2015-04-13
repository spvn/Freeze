using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	private Vector3 targetPoint;
	private Vector3 firstPoint;

	public float speed;
	public scriptMovement playerMovement;

	private GameManager gameManager;
	public LineRenderer bulletLine;

	private Vector3 bulletDirection = Vector3.zero;
	private float bulletLineLength;
	private RaycastHit objHit;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("Game Manager").GetComponent<GameManager>();
	}

	public void setBulletDirection(Vector3 targetVector)
	{
		targetPoint = targetVector;
		bulletDirection = targetPoint - this.transform.position;
		bulletDirection = transform.rotation * bulletDirection;
		bulletDirection = Vector3.Normalize (bulletDirection);
		
		Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection);
		transform.rotation = bulletRotation;
		
		Vector3 lineFirstPoint = this.transform.position + (bulletDirection);
		this.transform.localPosition = lineFirstPoint;
		firstPoint = this.transform.localPosition;
		bulletLine.SetPosition (0, lineFirstPoint);
		bulletLine.SetPosition (1, targetPoint);
		bulletLine.SetPosition (2, targetPoint + (bulletDirection * 10));
		bulletLineLength = Vector3.Distance (lineFirstPoint, (targetPoint + (bulletDirection * 100))); 
	}
	// Update is called once per frame
	void Update () {

		checkHitPlayer ();
		if ( !gameManager.isGameOver && bulletDirection != Vector3.zero && !gameManager.isFrozen) {
			this.transform.localPosition += bulletDirection * speed * Time.deltaTime;
		}

	}

	void checkHitPlayer()
	{
		if (Physics.Raycast (firstPoint, bulletDirection, out objHit, Mathf.Infinity, (1<<9))) {
			bulletLine.SetColors (Color.red, Color.red);
		} else {
			bulletLine.SetColors(Color.green, Color.green);
		}
	
	}

	void OnCollisionEnter( Collision col )
	{	
		Debug.Log ("Collided bullet " + col.gameObject.name + " at " + this.transform.localPosition.ToString());

		Destroy(gameObject);
		if (col.gameObject.layer == 9) {
			//Debug.Log("Bullet position: " + this.transform.localPosition +" hit player " + playerMovement.gameObject.transform.localPosition);
			gameManager.GameOver();
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.layer == 9) {
			gameManager.GameOver();
		}
		Destroy (gameObject);
	}
}
