using UnityEngine;
using System.Collections;

public class Empty_Screen_Control : MonoBehaviour {

    float timeLeft = 10f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime; if (timeLeft < 0) { Application.LoadLevel("BridgeGame"); }
	}
}
