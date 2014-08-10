using UnityEngine;
using System.Collections;

public class Scene2_Enemy : MonoBehaviour {

    private float m_velocity;

    // Use this for initialization
    void Start() {
        m_velocity = Random.Range(0.01f, 0.1f);
    }

    // Update is called once per frame
    void Update() {
        transform.position += m_velocity * (Scene2_PrincessSrc.Position - transform.position);
        if ((Scene2_PrincessSrc.Position - transform.position).magnitude <= 0.5f) {
            
            Destroy(gameObject);
            Scene2_EnemySpawnerBehaviour.NumEnemies--;
        }
    }
}
