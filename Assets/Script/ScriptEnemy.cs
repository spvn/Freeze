using UnityEngine;
using System.Collections;

public class ScriptEnemy : MonoBehaviour {
	public GameObject player;
	bool shooting = false;
	public GameObject bulletPrefab;
	public Animator enemyAnimator;

	float timer = 0.0f;
	public float intervalShootTime;
	public bool isHostile = true;
	Quaternion initialAngle;
	Vector3 bulletOffset = new Vector3(0, 2.5f, 0);
	Vector3 playerOffset = new Vector3(0, 0.3f, 0);
	Vector3 randomOffset;
	float playerSpeed;

	bool isAiming = false;

	// Use this for initialization
	void Start () {
		initialAngle = this.transform.localRotation;
		playerSpeed = player.GetComponent<scriptMovement>().playerSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (isHostile){
			//Debug.Log(gameObject.name + " " + isFacingPlayer() + " " + player.GetComponent<scriptMovement>().isFrozen + withinRotationRange());
			if (isFacingPlayer() && !player.GetComponent<scriptMovement>().isFrozen && withinRotationRange()) {
				if(!isAiming)
				{
					isAiming = true;
					enemyAnimator.SetBool("Aim", true);
				}
				transform.LookAt(player.transform);
			
				timer += Time.deltaTime;
				if (timer > intervalShootTime) {
					timer = 0.0f;
					shootBullet ();
				}
			}
		}
	}

	bool withinRotationRange()
	{
		float rotationAngle = Mathf.Abs ((this.transform.rotation.y) - initialAngle.y) * Mathf.Rad2Deg;
		//Debug.Log (this.transform.rotation.y * Mathf.Rad2Deg + " " + initialAngle.y * Mathf.Rad2Deg);
		if (rotationAngle <= 30.0f && rotationAngle >= 0.0f) {
			return true;
		}
		Debug.Log ("Not turning " + rotationAngle);
		return false;
	}

	bool isFacingPlayer()
	{
		Vector3 playerPos = player.transform.position;
		float distance = Vector3.Distance (this.transform.position, playerPos) - 1;
		Vector3 direction = playerPos - (this.transform.position + bulletOffset);
	
		Debug.DrawRay (this.transform.position + bulletOffset, direction);
		if (!Physics.Raycast(this.transform.position + bulletOffset, direction, distance, (1<<9 | 1 <<0))) {
//			Debug.Log("Enemy is facing player");
			return true;
		}

		return false;
	}

	void shootBullet(){
		Vector3 bulletTargetPoint;

		if (shooting) {
			return;
		}

		shooting = true;
		enemyAnimator.Play ("Fire 1Pistol");

		GameObject bullet = (GameObject)Instantiate (bulletPrefab);

		bullet.transform.position = this.transform.position + bulletOffset;
		
		//Time of flight to reach player
		float bulletTimeToCurrPos = (player.transform.position - transform.position).magnitude / bulletPrefab.GetComponent<BulletScript>().speed;
		Debug.Log (bulletTimeToCurrPos);
		
		//playerOffset += player.transform.forward * playerSpeed;
		bulletTargetPoint = player.transform.position + playerOffset + ((player.transform.forward * playerSpeed) * bulletTimeToCurrPos /2);
		
		bullet.GetComponent<BulletScript> ().setBulletDirection (bulletTargetPoint);
		bullet.GetComponent<BulletScript> ().playerMovement = player.GetComponent<scriptMovement>();

		shooting = false;
	}

	void OnMouseDown(){
		if (withinMeleeRange ()) {
			Destroy (gameObject);
		}

	}

	bool withinMeleeRange()
	{
		if (Vector3.Distance (player.transform.position, this.transform.position) < 2.0f) {
			return true;
		}
		return false;
	}

	IEnumerator Fall()
	{
		yield return null;
	}
}
