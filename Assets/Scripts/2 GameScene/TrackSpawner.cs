using UnityEngine;

public class TrackSpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    public Transform[] puntosMalos = new Transform[3];
    public Transform[] puntosBuenos = new Transform[3];

    public float delay = 3f;
    float timer;
    void Update() {

        timer += Time.deltaTime;
        if (timer > delay) {

            SpawnSafe();
            timer = 0;
        }
    }
    void SpawnSafe() {

        try  {
            if (enemyPrefab == null || powerupPrefab == null) return;

            if (Random.value > 0.7f) // 30% powerup
            {
                int i = Random.Range(0, 3);
                if (puntosBuenos[i])
                    Instantiate(powerupPrefab, puntosBuenos[i].position, Quaternion.identity);
            }
            else {
                int i = Random.Range(0, 3);
                if (puntosMalos[i])
                    Instantiate(enemyPrefab, puntosMalos[i].position, Quaternion.identity);
            }
        }
        catch (System.Exception e) {
            Debug.LogError("SPAWN ERROR: " + e.Message);
        }
    }
}