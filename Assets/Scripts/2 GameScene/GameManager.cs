using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public int vidas = 3, score = 0;
    public float tiempo = 0f;
    public float intervaloDificultad = 5f;
    public float incrementoVelocidad = 0.5f;
    private float tiempoAcumulado = 0f;

    public TextMeshProUGUI vidasText, scoreText, tiempoText;

    public ImpactEffect impactEffect;

    private int scoreTotalRanking = 0;

    [Header("SISTEMA NIVELES")]
    public int scoreVictoriaBase = 1000;
    private int nivelActual = 1;
    private int scoreVictoriaActual;
    private bool gameActivo = true;

    void Awake() { if (Instance == null) Instance = this; }

    void Start() { 
        gameActivo = true; 
        //CARGAR scoreTotalRanking persistente
        if (PlayerPrefs.HasKey("ScoreTotalRanking")) {
            scoreTotalRanking = PlayerPrefs.GetInt("ScoreTotalRanking");
        }
        if (PlayerPrefs.HasKey("NivelActual")) {
            nivelActual = PlayerPrefs.GetInt("NivelActual");
        }
        else
        {
            nivelActual = 1; // Primera vez
        }
        CalcularScoreVictoria();
    }

    void CalcularScoreVictoria() {
        scoreVictoriaActual = scoreVictoriaBase * nivelActual;
    }

    void Update() {

        if (!gameActivo) return;

        tiempo += Time.deltaTime;
        tiempoText.text = "Tiempo: " + Mathf.Floor(tiempo).ToString();
        tiempoAcumulado += Time.deltaTime;

        if (tiempoAcumulado >= intervaloDificultad) {
            tiempoAcumulado = 0f;
            EnemyMover.speed += incrementoVelocidad;
            PowerUpMover.speed += incrementoVelocidad;
            Debug.Log("Nueva velocidad: " + EnemyMover.speed);
        }

        if (score >= scoreVictoriaActual) {
            gameActivo = false;
            if (impactEffect) impactEffect.GameWin();
            return;
        }
    }
    public void PerderVida() {
        vidas--;
        vidasText.text = "Vidas: " + vidas;
        if (vidas <= 0) {
            gameActivo = false;
            if (impactEffect != null)  {
                impactEffect.GameOver();   //se encarga de rojo + panel derrota
            }
        }
        else {
            // Impacto normal enemigo
            if (impactEffect != null) {
                impactEffect.ImpactoEnemigo(); // Rojo 0.5s
            }
        }
    }
    public void GanarPuntos() {

        score += 100;
        scoreText.text = "Score: " + score;
        scoreTotalRanking += 100;
        PlayerPrefs.SetInt("ScoreTotalRanking", scoreTotalRanking);

        if (score % 500 == 0) {  
            vidas++;
            vidasText.text = "Vidas: " + vidas;
            Debug.Log("¡Vida extra! Score: " + score);
        }
    }
    public void SiguienteNivel() {

        //GUARDAR score del NIVEL COMPLETADO al ranking
        PlayerPrefs.SetInt("ScoreNivelCompletado", score);
        PlayerPrefs.SetInt("ScoreTotalRanking", scoreTotalRanking);
        PlayerPrefs.SetInt("NivelActual", nivelActual + 1);
        nivelActual++;
        CalcularScoreVictoria();
        vidas = 3; score = 0; tiempo = 0;
        EnemyMover.speed = 3f; PowerUpMover.speed = 3f;
        PlayerPrefs.SetFloat("EnemySpeedReset", 3f);
        PlayerPrefs.SetFloat("PowerUpSpeedReset", 3f);
        gameActivo = true; Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public int GetScoreTotalRanking() {
        return scoreTotalRanking;
    }
    public int GetNivelActual() {
        return nivelActual;
    }
}