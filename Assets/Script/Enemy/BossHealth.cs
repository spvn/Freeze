using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public float maxHealth;

	private GameObject player;
	private LevelManager levelManager;

	private float currentHealth;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("OVRCameraRig");
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();

		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TakeDamage(float damage){
		currentHealth -= damage;

		if (currentHealth <= 0) {
			currentHealth = 0;
		}
	}

	public float getCurrentHealth(){
		return currentHealth;
	}

	public float getCurrentHealthPercentage(){
		return currentHealth/maxHealth*100.0f;
	}
}
