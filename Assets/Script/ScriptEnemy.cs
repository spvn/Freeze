using UnityEngine;
using System.Collections;

public class ScriptEnemy : MonoBehaviour {
	public GameObject playerHolder;

	public GameObject player;
	bool shooting = false;
	public GameObject bulletPrefab;

	float timer = 0.0f;
	float intervalShootTime = 1.0f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (isFacingPlayer() && !player.GetComponent<scriptMovement>().isFrozen) {
			timer += Time.deltaTime;
			if (timer > intervalShootTime) {
				timer = 0.0f;
				shootBullet ();
			}
		}
	}

	bool isFacingPlayer()
	{
		Vector3 playerPos = playerHolder.transform.localPosition + player.transform.localPosition;
		float distance = Vector3.Distance (this.transform.localPosition, playerPos);
		Vector3 direction = playerPos - this.transform.localPosition;
	
		if (Physics.Raycast(this.transform.localPosition, direction, distance)) {
			Debug.Log("Enemy is facing player");
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

		bullet.transform.localPosition = this.transform.localPosition;

		bulletTargetPoint = playerHolder.transform.localPosition + player.transform.localPosition;
		bullet.GetComponent<BulletScript> ().setBulletDirection (bulletTargetPoint);
		bullet.GetComponent<BulletScript> ().playerMovement = player.GetComponent<scriptMovement>();

		shooting = false;
	}

}
