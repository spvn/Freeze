#pragma strict

var countdownTime : float = 10.0f;

function Start () {
	GetComponent.<Renderer>().material.color.a = 1;
}

function Update () {
		
		
		if (countdownTime >= 0.0){
			countdownTime -= Time.deltaTime;
			GetComponent.<Renderer>().material.color.a  -=  0.3 * Time.deltaTime;
		} else {
			GetComponent.<Renderer>().material.color.a  = 0.0f;		
			countdownTime = -1.0f;
		}
		
}	
