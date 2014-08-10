using UnityEngine;
using System.Collections;

public class Game02_GM : MonoBehaviour {

    public static int points = 0;
    public GameObject finalBridge;
    private bool playLastaudio = true; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(points == 4 && playLastaudio)
        {
            finalBridge.transform.position = new Vector3(finalBridge.transform.position.x, finalBridge.transform.position.y, -4);
            audio.Play();
            playLastaudio = false; 
        }
	
	}
}
