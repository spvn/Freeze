using UnityEngine;
using System.Collections;

public class ScriptEnemy : MonoBehaviour {
	public GameObject player;
	bool shooting = false;
	public GameObject bulletPrefab;

	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		shootBullet ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > 0.5f) {
			timer = 0.0f;
			shootBullet();
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

		bulletTargetPoint = player.transform.localPosition + player.transform.FindChild("Player").transform.localPosition;
		bullet.GetComponent<BulletScript> ().setBulletDirection (bulletTargetPoint);

		shooting = false;
	}

}
