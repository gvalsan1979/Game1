using UnityEngine;
using System.Collections;

public class SpotsBehavior : MonoBehaviour {

    public GameObject piceToFit; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == piceToFit.name)
        {
            collider.transform.position = transform.position;
            collider.name = "Done!";
            Game02_GM.points++; 
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.name == piceToFit.name)
        {
            collider.transform.position = transform.position;
            collider.name = "Done!";
            Game02_GM.points++;
        }
    }
}
