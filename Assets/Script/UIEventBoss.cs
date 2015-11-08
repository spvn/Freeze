using UnityEngine;
using System.Collections;

public class UIEventBoss : MonoBehaviour {
	public GameObject[] relatedUINodes;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.layer == 9) {
			for(int i=0; i < relatedUINodes.Length; i++)
			{
				Destroy(relatedUINodes[i]);
			}
			Destroy (gameObject);
		}
	}
}
