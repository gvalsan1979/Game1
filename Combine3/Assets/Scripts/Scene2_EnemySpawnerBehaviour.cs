using UnityEngine;
using System.Collections;

public class Scene2_EnemySpawnerBehaviour : MonoBehaviour {
    public GameObject enemy;
    private const int FPS = 30;
    public static int NumEnemies = 1;
    private int m_frameCount;

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        m_frameCount++;
        if (NumEnemies <= 10 && m_frameCount % FPS == 0) {
            Instantiate(enemy, new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-4.0f, 4.0f)), new Quaternion());
            NumEnemies++;
        }
    }
}
