using UnityEngine;
using System.Collections;

public class PulseAttack : MonoBehaviour {

    public float pulseRadius = 0f;
    public GameObject actionBarObject;
    private ActionBar actionBar;

    // Use this for initialization
    void Start ()
    {
        actionBar = actionBarObject.GetComponent<ActionBar>();
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnDrawGizmos()
    {
        // Dawing of sphere to debug overlap sphere
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, pulseRadius);
    }

    public void destroyEnemies ()
    {
        // Depleting action bar stock
        actionBar.DepleteActionStock(20);
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, pulseRadius);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.layer == 12 &&
                    hitColliders[i].gameObject.tag == "Destroyable")
            {
                hitColliders[i].gameObject.SetActive(false);
            }
        }
    }

}
