using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_Coins : MonoBehaviour {

        public static int NumCoinsGrabbed { get; set; }

        private float m_disappearTime;
        private float m_killedTime;

        // Use this for initialization
        void Start () {
            transform.position = new Vector2(
                Random.Range(-8.0f, 8.0f),
                Random.Range(-4.0f, 4.0f));
            m_disappearTime = Time.time + Scene3_BeachConstants.COIN_LIFETIME;
        }

        // Update is called once per frame
        void Update () {
            //gone?
            if ((m_killedTime == 0.0f && Time.time >= m_disappearTime) ||
            (m_killedTime != 0.0f && Time.time > m_killedTime)) {
                Destroy(gameObject);
            }
            if (m_killedTime == 0.0f &&
                Scene3_Shield.IsLockedToHand &&
                Vector2.Distance(SimpleGame_HandTracking.HandUniversalPosition, gameObject.transform.position) <= 1f) {
                audio.Play();
                NumCoinsGrabbed++;
                gameObject.renderer.enabled = false;
                m_killedTime = Time.time + 0.5f;
                //points++ ?
            }
        }
    }
}
