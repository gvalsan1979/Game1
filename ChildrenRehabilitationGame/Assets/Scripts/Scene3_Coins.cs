using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_Coins : MonoBehaviour {

        private float m_disappearTime;

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
            if (Time.time >= m_disappearTime) {
                Destroy(gameObject);
            }
            if (Scene3_Shield.IsLockedToHand &&
                Vector2.Distance(SimpleGame_HandTracking.HandUniversalPosition, gameObject.transform.position) <= 1f) {
                Destroy(gameObject);
                //points++ ?
            }
        }
    }
}
