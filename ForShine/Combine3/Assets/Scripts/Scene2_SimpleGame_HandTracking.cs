using System.Linq;
using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class Scene2_SimpleGame_HandTracking : MonoBehaviour {

    /// <summary>
    /// The game object that manages the track of the body from the Kinect
    /// </summary>
    public GameObject BodySourceManager;

    /// <summary>
    /// Internal object that receives the game object. 
    /// </summary>
    private BodySourceManager _BodyManager;

    /// <summary>
    /// Static variable that receives right hand position
    /// </summary>
    public static Vector2 HandUniversalPosition;
    
    /// <summary>
    /// left hand position. 
    /// </summary>
    public Vector2 leftHandPosition;
     
    /// <summary>
    /// number of bodys tracked by Kinect
    /// </summary>
    private int numberOfPlayers;

 
    private Vector3 bufferForRightHand;

    public static string withaPuzzle = null; 
 
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {     

        // Unpdates the right hand position that is seen by the other game objects.
        HandUniversalPosition = transform.position; 

        // Checks if we have a BodySourceManager. (error checking step)
        if (BodySourceManager == null)
        {
            return;
        }

        // Atributes the BodySourceManager of the imput game object to the new internal BodySourceManager object. 
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        
        // error cheking step
        if (_BodyManager == null)
        {
            return;
        }
       
        // Gets the right hand position using BodyManager and turn it to the game`s coordinate system. 
        bufferForRightHand = CoordinatesTranform(_BodyManager.GetHandsPositionOnColorSpace()[0]);

        // Atributes that position to the game object
        transform.position = new Vector3(bufferForRightHand.x, bufferForRightHand.y, transform.position.z);

        Body[] bodies = _BodyManager.GetData();
        if (bodies != null)
        {
            foreach (Body body in bodies)
            {
                if (body.IsTracked && body.TrackingId == _BodyManager.GetMainPlayerId())
                {
                    if (body.HandRightState == HandState.Closed)
                    {
                        if (gameObject.renderer.material.color == Color.green)
                        {
                            gameObject.renderer.material.color = Color.green;
                        }
                        else
                        {
                            gameObject.renderer.material.color = Color.red;
                        }                     
                    }
                    else
                    {
                        gameObject.renderer.material.color = Color.blue;
                        withaPuzzle = null;
                    }
                }
            }
        }
    }
    

    private Vector2 CoordinatesTranform(ColorSpacePoint orignalPoint)
    {
        const float newX = 18.0f, newY = 10.0f;
        const float oldX = 1920.0f, oldY = 850.0f;
        const float specialY = 1.28f;

        float cameraX = orignalPoint.X, cameraY = orignalPoint.Y;

        float accurateX = cameraX/oldX*newX - newX/2.0f;
        float accurateY = -(cameraY/oldY*newY/specialY - newY/2.0f);

        return new Vector2(accurateX, accurateY);
        
        /*orignalPoint.Y = -orignalPoint.Y;
        Vector2 originalVector = new Vector2(orignalPoint.X, orignalPoint.Y);
        Vector2 finalVector = new Vector2((30.0F / (float)1920), (25.0F / (float)1080));
        Vector2 origin = new Vector2(15F, -15F);
        finalVector = Vector2.Scale(finalVector, originalVector);
        finalVector = finalVector - origin;
        finalVector = new Vector2(finalVector.x * 1.3f, finalVector.y * 0.8f - 0.8f);*/
    }

    private int GetRealDataLength(Windows.Kinect.Body[] data)
    {
        int realLength = 0;
        foreach (var body in data)
        {
            if (body == null)
            {
                return 0;
            }

            if (body.IsTracked)
            {
                realLength++;
            }       
        }
        return realLength; 
    }



    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Equals("puzzle01") || collision.name.Equals("puzzle02") || collision.name.Equals("puzzle03") || collision.name.Equals("puzzle04"))
        {
            if (withaPuzzle == null)
            {
                withaPuzzle = collision.name;
            }
        }
        
        //if (withaPuzzle == null)
        //{
        //    if (collider.name.Equals("piceTopLeft") || collider.name.Equals("piceTopRight") || collider.name.Equals("piceDownLeft") || collider.name.Equals("piceDownRight"))
        //    {
        //        if ((gameObject.renderer.material.color == Color.red) || (gameObject.renderer.material.color == Color.green))
        //        {
        //            // gameObject.transform.position = collider.transform.position;
        //            gameObject.renderer.material.color = Color.green;
        //            withaPuzzle = collider.name;
        //        }
        //    }
        //}
        //else
        //{
        //    if (collider.name.Equals(withaPuzzle))
        //    {
        //        if ((gameObject.renderer.material.color == Color.red) || (gameObject.renderer.material.color == Color.green))
        //        {
        //            // gameObject.transform.position = collider.transform.position;
        //            gameObject.renderer.material.color = Color.green;
        //            withaPuzzle = collider.name;
        //        }
        //        else
        //        {
        //            withaPuzzle = null;
        //        }
        //    }
        //}
    }


}
