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
		shootBullet ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player.GetComponent<scriptMovement>().isFrozen) {
			timer += Time.deltaTime;
			if (timer > intervalShootTime) {
				timer = 0.0f;
				shootBullet ();
			}
		}
	}

	void shootBullet(){
		Vector3 bulletTargetPoint;

		if (shooting) {
			return;
		}

		shooting = true;

		GameObject bullet = (GameObject)Instantiate (bulletPrefab);

		bullet.transform.localPosition = new Vector3( this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - 1.0f);

		bulletTargetPoint = playerHolder.transform.localPosition + player.transform.localPosition;
		bullet.GetComponent<BulletScript> ().setBulletDirection (bulletTargetPoint);
		bullet.GetComponent<BulletScript> ().playerMovement = player.GetComponent<scriptMovement>();

		shooting = false;
	}

}
