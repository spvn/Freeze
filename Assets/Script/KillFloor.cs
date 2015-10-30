using UnityEngine;
using System.Collections;

public class KillFloor : MonoBehaviour {
	private LevelManager levelManager;
	public GameObject bulletDestroyer;
	// Use this for initialization
	void Start () {
		levelManager = GameObject.Find ("Level Manager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter( Collider other )
	{
		Debug.Log ("HIT");
		if (other.gameObject.layer == 9 || other.gameObject == bulletDestroyer) {
			Debug.Log ("Fell");
			levelManager.GameOver ();
		}
	}
}
