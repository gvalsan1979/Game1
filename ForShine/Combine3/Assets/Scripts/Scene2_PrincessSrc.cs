using UnityEngine;
using System.Collections;

public class Scene2_PrincessSrc : MonoBehaviour {

    public static Vector3 Position;

    // Use this for initialization
    void Start () {
        Position.x = transform.localPosition.x;
        Position.y = transform.localPosition.y;
        Position.z = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update () {
        transform.localPosition += new Vector3(0.01f, 0.01f);
        Position.x = transform.localPosition.x;
        Position.y = transform.localPosition.y;
        Position.z = transform.localPosition.z;
        //Debug.Log(new { Msg = "Message", MyX = Position.x, MyY = Position.y });
    }
}
