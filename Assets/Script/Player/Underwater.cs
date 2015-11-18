using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour {
	
	//This script enables underwater effects. Attach to main camera.
	
	//Define variable
	public float underwaterLevel = 7;
	public GameObject centerEyeAnchor;
	
	//The scene's default fog settings
	private bool defaultFog;
	private Color defaultFogColor;
	private float defaultFogDensity;
	private Material noSkybox;
	private Material defaultSkybox;
	
	void Start () {
		//Set the background color
		defaultFog = RenderSettings.fog;
		defaultFogColor = RenderSettings.fogColor;
		defaultFogDensity = RenderSettings.fogDensity;
		defaultSkybox = RenderSettings.skybox;

		centerEyeAnchor.GetComponent<Camera>().backgroundColor = new Color(0, 0.4f, 0.7f, 1);
	}
	
	void Update () {
		if (centerEyeAnchor.transform.position.y < underwaterLevel)
		{
			Debug.Log ("BELOW WATER LEVEL");
			RenderSettings.fog = true;
			RenderSettings.fogColor = new Color(0, 0.4f, 0.7f, 0.6f);
			RenderSettings.fogDensity = 0.04f;
			RenderSettings.skybox = noSkybox;
		}
		else
		{
			RenderSettings.fog = defaultFog;
			RenderSettings.fogColor = defaultFogColor;
			RenderSettings.fogDensity = defaultFogDensity;
			RenderSettings.skybox = defaultSkybox;
		}
	}


}