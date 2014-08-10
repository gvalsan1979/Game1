using UnityEngine;
using System.Collections;


public class Wall_Scene1 : MonoBehaviour {

    public GameObject CorrectPosition;
    public GameObject Background;
    public GameObject Background_Complete;

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
        if ((collider.gameObject.tag == "Player") && ((SimpleGame_HandTracking_Scene1.GaspedObject == "") || (SimpleGame_HandTracking_Scene1.GaspedObject == gameObject.tag)))
        {
            if ((collider.renderer.material.color == Color.red) || (collider.renderer.material.color == Color.green))
            {
                gameObject.transform.position = collider.transform.position;
                collider.transform.renderer.material.color = Color.green;
                SimpleGame_HandTracking_Scene1.GaspedObject = gameObject.tag;

            }

            if ((gameObject.tag == "CorrectWall") )
            {
                if ((CorrectPosition.transform.position - transform.position).magnitude <= 0.5f)
                {

                   // Background_Complete.layer = 1;
                    //Background.layer = 0;
                    Background.transform.Rotate(new Vector3(0, 90, 0));
                    Background_Complete.transform.Rotate(new Vector3(0,-90,0));

                    AudioSource sound = collider.GetComponent<AudioSource>();
                    sound.Play();

                    //System.Threading.Thread.SpinWait(1000000);
                    //System.Threading.Thread.Sleep(5000);
                    StartCoroutine(Wait());   

                    Application.LoadLevel("BridgeGame");

                    Destroy(gameObject);
                   
                    //Background.transform.Rotate(new Vector3(0, 90, 0));
                    //Background_Complete.transform.Rotate(new Vector3(0,0,0));
                    //Destroy(Background);
                    //Background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("COMPLETE");
                    //Instantiate(Background);
                }
            }
           
        }
    }

    IEnumerator Wait() { yield return new WaitForSeconds(5.0f);}
}
