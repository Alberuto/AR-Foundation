using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public int vidas = 3, score = 0;
    public float tiempo = 0f;
    public float intervaloDificultad = 5f;
    public float incrementoVelocidad = 0.5f;
    private float tiempoAcumulado = 0f;

    public TextMeshProUGUI vidasText, scoreText, tiempoText;
    public GameObject gameWinPanel;
    public GameObject gameOverPanel;

    void Awake() { if (Instance == null) Instance = this; }

    void Update() {

        tiempo += Time.deltaTime;
        tiempoText.text = "Tiempo: " + Mathf.Floor(tiempo).ToString();
        tiempoAcumulado += Time.deltaTime;

        if (tiempoAcumulado >= intervaloDificultad) {
            tiempoAcumulado = 0f;
            EnemyMover.speed += incrementoVelocidad;
            PowerUpMover.speed += incrementoVelocidad;
            Debug.Log("Nueva velocidad: " + EnemyMover.speed);
        }
    }
    public void PerderVida() {
        vidas--;
        vidasText.text = "Vidas: " + vidas;
        if (vidas <= 0) {
            Time.timeScale = 0;
            if (gameOverPanel) gameOverPanel.SetActive(true);
        }
    }
    public void GanarPuntos() {

        score += 100;
        scoreText.text = "Score: " + score;

        if (score % 500 == 0) {  
            vidas++;
            vidasText.text = "Vidas: " + vidas;
            Debug.Log("¡Vida extra! Score: " + score);
        }
    }
}