using UnityEngine;
using System.Collections;

public class ScriptEnemy : MonoBehaviour {
	public GameObject playerHolder;

	public GameObject player;
	bool shooting = false;
	public GameObject bulletPrefab;

	float timer = 0.0f;
	float intervalShootTime = 1.0f;
	Quaternion initialAngle;

	// Use this for initialization
	void Start () {
		initialAngle = this.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		if (isFacingPlayer() && !player.GetComponent<scriptMovement>().isFrozen && withinRotationRange()) {

			transform.LookAt(player.transform);
		
			timer += Time.deltaTime;
			if (timer > intervalShootTime) {
				timer = 0.0f;
				shootBullet ();
			}
		}
	}

	bool withinRotationRange()
	{
		float rotationAngle = Mathf.Abs ((this.transform.rotation.y) - initialAngle.y) * Mathf.Rad2Deg;
		Debug.Log (this.transform.rotation.y * Mathf.Rad2Deg + " " + initialAngle.y * Mathf.Rad2Deg);
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
		Vector3 direction = playerPos - this.transform.position;
	
		//Debug.DrawRay (this.transform.position, direction);
		if (!Physics.Raycast(this.transform.position, direction, distance, (1<<9))) {
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

		GameObject bullet = (GameObject)Instantiate (bulletPrefab);

		bullet.transform.position = this.transform.position + new Vector3(0,0.3f,0);

		bulletTargetPoint = player.transform.position;
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
