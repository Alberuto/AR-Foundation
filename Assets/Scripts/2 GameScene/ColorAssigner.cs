using UnityEngine;

public class ColorAssigner : MonoBehaviour {

    [Header("Materiales")]
    public Material[] enemyMats;   // Rojo, Negro, Gris
    public Material[] powerupMats; // Verde, Azul, Naranja

    void Start()
    {
        AssignRandomColor();
    }

    //MÉTODO PÚBLICO para TrackSpawner
    public void AssignRandomColor()
    {
        Renderer rend = GetComponent<Renderer>();

        if (Random.value > 0.5f)
        {
            // ENEMIGO
            rend.material = enemyMats[Random.Range(0, enemyMats.Length)];
            gameObject.tag = "Enemy";
        }
        else
        {
            // POWERUP
            rend.material = powerupMats[Random.Range(0, powerupMats.Length)];
            gameObject.tag = "PowerUp";
        }
    }
}