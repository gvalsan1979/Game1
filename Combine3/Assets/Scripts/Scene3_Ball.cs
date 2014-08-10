using UnityEngine;
using System.Collections;


public class Ball : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Send an event evey time 
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if ((collider.renderer.material.color == Color.red) || (collider.renderer.material.color == Color.green))
            {
                gameObject.transform.position = collider.transform.position;
                collider.transform.renderer.material.color = Color.green;

            }
           
        }
    }
}
