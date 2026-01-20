using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Enemigo")) {

            GameManager.Instance.PerderVida();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("PowerUp")) {

            GameManager.Instance.GanarPuntos();
            other.gameObject.SetActive(false);
        }
    }
}
