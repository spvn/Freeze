using UnityEngine;
using System.Collections;

public class BlastRadius : MonoBehaviour {

	public float damage;
	public GameObject explosionEffect;

	private LevelManager levelManager;
	private BossHealth bossHealth;

	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
		//bossHealth = GameObject.Find ("Boss").GetComponent<BossHealth>();

		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!explosionEffect.GetComponent<ParticleSystem>().IsAlive()) {
			Destroy (gameObject);
		} 
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "OVRCameraRig") {
			Debug.Log ("Player within Blast Bullet radius.");
			//levelManager.GameOver ();
		} 
		if (other.gameObject.name == "Boss") {
			Debug.Log ("Boss within Blast Bullet radius.");
			//bossHealth.TakeDamage(damage);
		}
	}
}
