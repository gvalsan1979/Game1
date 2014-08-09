﻿using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class SimpleGame_HandTracking : MonoBehaviour {

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
            
        
    }
    

    private Vector2 CoordinatesTranform(ColorSpacePoint orignalPoint)
    {
        orignalPoint.Y = -orignalPoint.Y;
        Vector2 originalVector = new Vector2(orignalPoint.X, orignalPoint.Y);
        Vector2 finalVector = new Vector2((30.0F / (float)1920), (25.0F / (float)1080));
        Vector2 origin = new Vector2(15F, -15F);
        finalVector = Vector2.Scale(finalVector, originalVector);
        finalVector = finalVector - origin;
        finalVector = new Vector2(finalVector.x * 1.3f, finalVector.y * 0.8f - 0.8f); 
        return finalVector;
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


}
