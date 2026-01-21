using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public ImpactEffect impactoEfecto;

    void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Enemigo")) {

            GameManager.Instance.PerderVida();
            impactoEfecto.ImpactoEnemigo();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("PowerUp")) {

            GameManager.Instance.GanarPuntos();
            impactoEfecto.ImpactoPowerUp();
            other.gameObject.SetActive(false);
        }
    }
}
