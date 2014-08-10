using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_EnemySpawner : MonoBehaviour {

        public GameObject EnemyObject;
        private float m_nextTime;

        // Use this for initialization
        void Start () {
            m_nextTime = Time.time + 1.0f / Scene3_BeachConstants.ENEMIES_PER_SECOND;
        }

        // Update is called once per frame
        void Update () {
            if (Time.time > m_nextTime) {
                m_nextTime = Time.time + 1.0f / Scene3_BeachConstants.ENEMIES_PER_SECOND;
                Instantiate(EnemyObject, new Vector3(), new Quaternion());
            }
        }
    }
}
