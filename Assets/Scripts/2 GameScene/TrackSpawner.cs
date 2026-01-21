using UnityEngine;

public class TrackSpawner : MonoBehaviour
{
    public GameObject[] puntosMalos, puntosBuenos;
    public float delayEntreSpawn = 2.5f; // Más rápido
    private float timer;
    private float powerUpChance = 0.25f; // 25% power-ups, 75% enemigos

    void Update() {
        timer += Time.deltaTime;
        if (timer > delayEntreSpawn) {
            timer = 0;
            SpawnSecuencia();
        }
    }
    void SpawnSecuencia() {
        if (Random.value < powerUpChance) {
            // 25% chance: PowerUp
            int index = Random.Range(0, puntosBuenos.Length);
            if (puntosBuenos[index] != null) {
                ActivarHijos(puntosBuenos[index]);
                Debug.Log("POWER-UP desde punto " + index);
            }
        }
        else {
            // 75% chance: Enemigo
            int index = Random.Range(0, puntosMalos.Length);
            if (puntosMalos[index] != null) {
                ActivarHijos(puntosMalos[index]);
                Debug.Log("ENEMIGO desde punto " + index);
            }
        }
    }
    void ActivarHijos(GameObject punto) {
        punto.SetActive(true);
        for (int j = 0; j < punto.transform.childCount; j++) {
            Transform hijo = punto.transform.GetChild(j);
            if (hijo.GetComponent<EnemyMover>() || hijo.GetComponent<PowerUpMover>()) {
                hijo.gameObject.SetActive(false);      // por si quedó activo lejos
                hijo.localPosition = Vector3.zero;    // resetea posición local
                hijo.gameObject.SetActive(true);       // re-spawn
                Debug.Log("Spawn de " + hijo.name + " en " + punto.name);
                break;
            }
        }
    }
}