using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Paths;
using UnityEngine;

namespace Assets.Scripts {

    public class Scene3_Enemy : MonoBehaviour {

        public Scene3_IPath Path { get; set; }
        public float EndTime { get; set; }
        private float m_startTime;
        private Vector2 m_startPosition;
        private Vector2 m_endPosition;
        private Animator m_animator;
        private float m_timeOfDeath;

        private static readonly IList<Scene3_IPath> m_paths = new List<Scene3_IPath> {
            new Scene3_SPath(),
            new Scene3_LinearPath()
        };

        // Use this for initialization
        void Start () {
            Debug.Log(new { Msg = "Create new enemy!", Time = Time.time });

            m_animator = GetComponent<Animator>();

            Path = m_paths.ElementAt(Random.Range(0, m_paths.Count()));

            m_startTime = Time.time;
            EndTime = m_startTime + 3.0f;

            m_startPosition = new Vector2(
                Random.Range(Scene3_BeachConstants.SENDOFF_LEFT_X, Scene3_BeachConstants.SENDOFF_RIGHT_X),
                Random.Range(5.0f, Scene3_BeachConstants.SENDOFF_Y));

            m_endPosition = new Vector2(
                Random.Range(-9.0f, 9.0f),
                Random.Range(-5.0f, Scene3_BeachConstants.BEACH_BORDER_Y));

            //Debug.Log(new { Start = m_startPosition, End = m_endPosition, StartTime = m_startTime });

            transform.position = m_startPosition;
        }

        // Update is called once per frame
        void Update () {
            //Debug.Log(new { EnemyTime = Time.time });

            //Updating position
            var currentTime = Time.time;
            transform.position = Path.GeneratePath(transform.position, m_startPosition, m_endPosition,
                currentTime - m_startTime, EndTime - m_startTime);

            if (Scene3_Shield.IsLockedToHand &&
                Vector2.Distance(SimpleGame_HandTracking.HandUniversalPosition, gameObject.transform.position) <= 1f) {
                m_animator.SetBool("StartBlowUp", true);
                m_timeOfDeath = Time.time;
            }

            if (m_timeOfDeath != 0.0f && (Time.time - m_timeOfDeath) >= 0.15f) {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D (Collider2D collider) {
            if (collider.gameObject.tag == "LowerBorder" && m_timeOfDeath == 0.0f) {
                m_animator.SetBool("StartBlowUp", true);
                m_timeOfDeath = Time.time;
            }
        }
    }
}
