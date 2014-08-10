using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour 
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;
    private CoordinateMapper mapper = null; 
    private CameraSpacePoint[] DataInCameraSpace = null; 
    private ColorSpacePoint[] colorpoints = null;
    private int numberOfPlayers = 0;
    private ulong mainPlayerId = 0;
    private Windows.Kinect.Vector4[] HandsOrientations; 
   
    
    /// <summary>
    /// Fuction for acessing the Body[] out of the class
    /// </summary>
    /// <returns></returns>
    public Body[] GetData()
    {
        return _Data;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Windows.Kinect.Vector4[] GetHandsOrientation()
    {
        return HandsOrientations; 
    }

    /// <summary>
    /// Function that returns only the left and right hand position already in ColorSpace
    /// </summary>
    /// <returns></returns>
    public ColorSpacePoint[] GetHandsPositionOnColorSpace()
    {
        return colorpoints;
    }


    /// <summary>
    ///  Returns main player ID
    /// </summary>
    public ulong GetMainPlayerId()
    {
        return mainPlayerId;
    }

    void Start () 
    {
        // Initializes de Kinect
        _Sensor = KinectSensor.GetDefault();
        // Alocates de arrays for receiving the positions for the hands in both camera and space coordinates
        DataInCameraSpace = new CameraSpacePoint[3];
        colorpoints = new ColorSpacePoint[3]; 
        
        // Initializes componets if sensor is plugged 
        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();
            
            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();          
            }

            mapper = _Sensor.CoordinateMapper;
        }

        HandsOrientations = new Windows.Kinect.Vector4[2]; 
    }

    void Update()
    {
        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_Data);
               // float planeEquation = frame.FloorClipPlane.W; NEVER USE THIS FUNCTION!! (Unity instant crash!) 
                frame.Dispose();
                frame = null;

                numberOfPlayers = GetRealDataLength(_Data);
                //Debug.Log(numberOfPlayers); 
                foreach (var body in _Data)
                {

                    if (body == null)
                    {
                        continue;
                    }

                    // A modification has been made in this part of the code so it returns the points of interes in colorspace coordinate
                    if (body.IsTracked && numberOfPlayers == 1)
                    {
                        DataInCameraSpace[0] = body.Joints[JointType.HandTipRight].Position;  // -------------------->
                        DataInCameraSpace[1] = body.Joints[JointType.HandTipLeft].Position;   // --------------------> this part was not in the original code from Microsoft
                        DataInCameraSpace[2] = body.Joints[JointType.ElbowRight].Position;    // -------------------->
                        mapper.MapCameraPointsToColorSpace(DataInCameraSpace, colorpoints);   // -------------------->
                        mainPlayerId = body.TrackingId;

                        HandsOrientations[0] = body.JointOrientations[Windows.Kinect.JointType.HandRight].Orientation;
                        HandsOrientations[1] = body.JointOrientations[Windows.Kinect.JointType.HandLeft].Orientation;                       
                    }
                    else 
                    {
                        if (body.IsTracked && body.TrackingId == mainPlayerId)
                        {
                            DataInCameraSpace[0] = body.Joints[JointType.HandTipRight].Position;
                            DataInCameraSpace[1] = body.Joints[JointType.HandTipLeft].Position;
                            mapper.MapCameraPointsToColorSpace(DataInCameraSpace, colorpoints);
                            mainPlayerId = body.TrackingId;
                        }
                    }
                }
            }
        }
    }
    
    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }
        
        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }
            
            _Sensor = null;
        }
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

    public JointType Joints { get; set; }
}
