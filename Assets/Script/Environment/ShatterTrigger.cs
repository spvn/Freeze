using UnityEngine;
using System.Collections;

public class ShatterTrigger : MonoBehaviour {

    public GameObject window;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter (Collider col)
    {
        print("Shattering");
        if (col.gameObject.layer == 9)
        {
            window.SetActive(true);
        }
    }
}
