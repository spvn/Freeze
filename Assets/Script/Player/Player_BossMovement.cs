using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Player_BossMovement : MonoBehaviour {

    public float rotationSpeed = 1.0f;
    public GameObject focalPoint;
    public LevelManager levelManager;
    private NoiseAndScratches[] nsObjects;

    void Start () {
        nsObjects = GetComponentsInChildren<NoiseAndScratches>();
    }
	
	void Update () {
        /*
        if (levelManager.isGameOver || levelManager.isFrozen || levelManager.isPause)
        {
            return;
        }
        */
        if ( Input.GetKeyDown(KeyCode.J) )
        {
            StartCoroutine(glitchEffect());
        }

        if ( Input.GetKeyDown(KeyCode.K) )
        {
            PulseAttack attack = gameObject.GetComponent<PulseAttack>();
            attack.deflectBossProjectile();
        }

	    if ( Input.GetKey(KeyCode.A) )
        {
            focalPoint.transform.Rotate(new Vector3(0, rotationSpeed, 0));
        }
        else if ( Input.GetKey(KeyCode.D) )
        {
            focalPoint.transform.Rotate(new Vector3(0, -rotationSpeed, 0));
        }

        if ( Input.GetKey(KeyCode.LeftAlt) )
        {
            jump();
        }
    }

    IEnumerator glitchEffect()
    {
        foreach (NoiseAndScratches obj in nsObjects)
        {
            obj.enabled = true;
        }
        yield return new WaitForSeconds(0.5f);
        foreach (NoiseAndScratches obj in nsObjects)
        {
            obj.enabled = false;
        }
    }

    void jump()
    {

    }
}
