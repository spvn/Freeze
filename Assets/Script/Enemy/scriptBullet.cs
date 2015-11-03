using UnityEngine;
using System.Collections;

public class scriptBullet : MonoBehaviour {
	private Vector3 targetPoint;
	private Vector3 firstPoint;

	public float speed;
    public float secondsToDestroyAfterMissing = 1f;

	public GameObject player;
	private LevelManager levelManager;
	public LineRenderer bulletLine;

	private Vector3 bulletDirection = Vector3.zero;
	private RaycastHit objHit;
	private Vector3 previousPlayerPos;
    private float distanceFromPlayer = 9999999999999;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		player = GameObject.Find("OVRCameraRig").transform.gameObject;
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
		bulletLine.SetPosition (1, targetPoint + (bulletDirection * 100));
	}
	// Update is called once per frame
	void Update () {
		checkHitPlayer ();
		checkNearMisses ();
		if ( !levelManager.isGameOver && bulletDirection != Vector3.zero && !levelManager.isFrozen) {
			this.transform.localPosition += bulletDirection * speed * Time.deltaTime;
		}
        if (checkPlayerDistance() <= this.distanceFromPlayer)
        {
            this.distanceFromPlayer = checkPlayerDistance();
        } else
        {
            StartCoroutine(destroyBullet());
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

	void checkNearMisses () {
		Vector3 currentPlayerPos = player.transform.position;
		float distOfBulletAndPlayer = Vector3.Distance (currentPlayerPos, transform.position);
		if (currentPlayerPos != previousPlayerPos && distOfBulletAndPlayer < 0.75) {
			ScoreManager.score += 10;
			previousPlayerPos = currentPlayerPos;
		}

	}

//	void OnCollisionEnter( Collision col )
//	{	
//		Destroy(gameObject);
//		if (col.gameObject.layer == 9) {
//			Debug.Log ("Collided bullet " + col.gameObject.name + " at " + this.transform.localPosition.ToString());
//			//Debug.Log("Bullet position: " + this.transform.localPosition +" hit player " + playerMovement.gameObject.transform.localPosition);
//			gameManager.GameOver();
//		}
//	}

	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.layer == 9) {
			Debug.Log ("Collided bullet " + other.gameObject.name + " at " + this.transform.localPosition.ToString ());
			levelManager.GameOver ();
		} else {
			// Bullet misses the player
			ScoreManager.score += 1;
		}
		Destroy (gameObject);
	}

    private float checkPlayerDistance()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

    private IEnumerator destroyBullet()
    {
        yield return new WaitForSeconds(secondsToDestroyAfterMissing);
        Debug.Log("Destorying a bulelt that missed");
        Destroy(this.gameObject);
    }
}
