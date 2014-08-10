using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_Clock : MonoBehaviour {

        // Use this for initialization
        private float m_startTime;

        void Start () {
            m_startTime = Time.time;
        }

        // Update is called once per frame
        void Update () {
            gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 360.0f * (1.0f - ((Time.time - m_startTime) / Scene3_BeachConstants.BOMB_LEVEL_TIME)));

            if (Time.time >= m_startTime + Scene3_BeachConstants.BOMB_LEVEL_TIME) {
                //SwitchLevel()
            }
        }
    }
}
