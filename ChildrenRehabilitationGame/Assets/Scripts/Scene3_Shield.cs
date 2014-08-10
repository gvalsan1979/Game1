using UnityEditor;
using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_Shield : MonoBehaviour {

        public static bool IsLockedToHand;
        private const float TIGHT_LOOP_LIMIT = 120.0f;
        private float m_lastCheckHandState;

        // Use this for initialization
        void Start () {
            IsLockedToHand = false;
        }

        // Update is called once per frame
        void Update () {
            if (Time.time - m_lastCheckHandState >= 0.20f) {
                m_lastCheckHandState = Time.time;
                if (!SimpleGame_HandTracking.IsRightClosed) {
                    IsLockedToHand = false;
                }
            }
            if (IsLockedToHand == true) {
                MoveShieldToHandSmoothly();
            }
        }

        void OnTriggerStay2D (Collider2D collider) {
            //Debug.Log("Shield collider!");
            if (collider.gameObject.tag == "Player" &&
                (SimpleGame_HandTracking.IsRightClosed || IsLockedToHand)) {
                IsLockedToHand = true;
                MoveShieldToHandSmoothly();
            }
        }

        private void MoveShieldToHandSmoothly () {
            var diff = (Vector3)(SimpleGame_HandTracking.HandUniversalPosition - (Vector2)gameObject.transform.position) / TIGHT_LOOP_LIMIT;
            for (int i = 0; i < (int)TIGHT_LOOP_LIMIT; i++) {
                gameObject.transform.position += diff;
                SceneView.RepaintAll();
            }
        }
    }
}
