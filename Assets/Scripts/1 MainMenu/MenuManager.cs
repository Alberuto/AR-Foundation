using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void Jugar() {
        SceneManager.LoadScene("2 GameScene");
    }
    public void Salir() {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
    public void Configuracion() {
        // FASE 2
        Debug.Log("Configuración (próximamente)");
    }
    public void Ranking() {
        // FASE 3  
        Debug.Log("Ranking (próximamente)");
    }
}