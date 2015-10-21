using UnityEngine;
using System.Collections;

public class PulseAttack : MonoBehaviour {

    public float pulseRadius = 0f;
    public int energyCost = 60;
    public GameObject actionBarObject;
    private ActionBar actionBar;
    public GameObject pulseEffect;

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
        if ( actionBar.canDeplete(energyCost) )
        {
            Instantiate(pulseEffect, transform.position - new Vector3(0,1.5f,0), transform.rotation);
            actionBar.DepleteActionStock(energyCost);

            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, pulseRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.layer == 12 &&
                        hitColliders[i].gameObject.tag == "Destroyable")
                {
                    ScriptTurret turret = hitColliders[i].gameObject.GetComponent<ScriptTurret>();
                    turret.Die();
                    //hitColliders[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void deflectBossProjectile ()
    {

    }
}
