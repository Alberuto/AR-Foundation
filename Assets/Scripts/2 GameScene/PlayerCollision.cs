using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public ImpactEffect impactoEfecto;

    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Enemy")) {

            GameManager.Instance.PerderVida();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("PowerUp")) {

            GameManager.Instance.GanarPuntos();
            impactoEfecto.ImpactoPowerUp();
            other.gameObject.SetActive(false);
        }
    }
}