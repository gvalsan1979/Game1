using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_CoinSpawner : MonoBehaviour {

        public GameObject CoinObject;
        private float m_dieTime;

        // Use this for initialization
        void Start () {
            m_dieTime = Time.time + 1.0f / Scene3_BeachConstants.COIN_PER_SECOND;
        }

        // Update is called once per frame
        void Update () {
            if (Time.time > m_dieTime) {
                m_dieTime = Time.time + 1.0f / Scene3_BeachConstants.COIN_PER_SECOND;
                Instantiate(CoinObject, new Vector3(), new Quaternion());
            }
        }
    }
}
