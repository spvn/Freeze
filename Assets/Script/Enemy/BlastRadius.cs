using UnityEngine;
using System.Collections;

public class BlastRadius : MonoBehaviour {

	public float damage;
	public float explosionTime = 1f;

	private LevelManager levelManager;
	private BossHealth bossHealth;

	private float explosionTimer;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		//bossHealth = GameObject.Find ("Boss").GetComponent<BossHealth>();

		gameObject.SetActive (false);

		explosionTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		explosionTimer += Time.deltaTime;
		if (explosionTimer >= explosionTime) {
			Destroy (gameObject);
		} 
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "OVRCameraRig") {
			Debug.Log ("Player within Blast Bullet radius.");
			levelManager.GameOver ();
		} 
		if (other.gameObject.name == "Boss") {
			Debug.Log ("Boss within Blast Bullet radius.");
			//bossHealth.TakeDamage(damage);
		}
	}
}
