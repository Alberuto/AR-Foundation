using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance; //Singleton class

    [Header("RANKING UI")]
    public GameObject panelRanking;
    public GameObject panelSettings;
    public Transform rankingContainer;
    public GameObject rankingEntryPrefab; // Prefab con TextMeshPro

    [Header("GAME SCENE")]
    public TMP_InputField inputInicialesGameScene;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Persiste entre escenas
        }
    }
    public void Jugar() {
        //en SETTINGS PODEMOS OBVIAR ESTOS BORRADOS PARA CONTINUAR LA PARTIDA POR DONDE IBAMOS, METER BOLEANO Y SEGUN{}
        if (PlayerPrefs.GetInt("PartidaInfinita", 0) != 1) {
            PlayerPrefs.DeleteKey("ScoreTotalRanking");
            PlayerPrefs.DeleteKey("NivelActual");
            PlayerPrefs.DeleteKey("ScoreNivelCompletado");
        }
        SceneManager.LoadScene("2 GameScene");
    }
    public void Salir() {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
    public void Configuracion() {
        if (panelSettings != null) {
            panelSettings.SetActive(true);
            SettingsManager.Instance?.CargarSettings(); // ← Singleton
        }
    }
    public void Menu() {

        string nombre = "";
        if (inputInicialesGameScene != null) {
            nombre = inputInicialesGameScene.text; // lo que ha escrito el jugador
        }
        GuardarRankingDirecto(
            nombre,
            GameManager.Instance.GetScoreTotalRanking(),
            GameManager.Instance.GetNivelActual()
            );
            SceneManager.LoadScene(0);
    }
    public void MostrarSettings() {
        panelSettings.SetActive(true);
    }
    public void SalirRanking() { 
            panelRanking.SetActive(false);
    }
    public void SalirSettings() {
        if (panelSettings != null) {
            panelSettings.SetActive(false);
            SettingsManager.Instance?.GuardarSettings(); // ← Singleton
        }
    }
    //SINGLETON HELPERS (desde GameManager)
    private static int GetScoreActual() {
        GameManager gm = FindObjectOfType<GameManager>();
        return gm != null ? gm.GetScoreTotalRanking() : 0;
    }
    private static int GetNivelActual() {
        GameManager gm = FindObjectOfType<GameManager>();
        return gm != null ? gm.GetNivelActual() : 1;
    }
    //FASE 3: RANKING FUNCIONAL
    public void Ranking() {
        MostrarRanking();
    }
    //RANKING METHODS
    public void MostrarRanking() {

        RankingData data = new RankingData();
        data.Load();

        //Limpiar
        foreach (Transform child in rankingContainer) {
            Destroy(child.gameObject);
        }
        // Generar TOP 10
        for (int i = 0; i < data.top10.Count; i++) {
            GameObject entry = Instantiate(rankingEntryPrefab, rankingContainer);
            TMP_Text texto = entry.GetComponentInChildren<TMP_Text>();
            texto.text = $"{i + 1}. {data.top10[i].nombre} - {data.top10[i].scoreTotal}pts (N:{data.top10[i].nivelMaximo})";
        }
        panelRanking.SetActive(true);
    }
    public static void GuardarRankingDirecto(string nombre, int score, int nivel) {

        RankingData data = new RankingData();
        data.Load(); //para que sea acumulativo en cada ejecucion
        data.AddEntry(nombre, score, nivel);
    }

    //CLASES JSON
    [System.Serializable]
    public class RankingEntry {
        public string nombre;
        public int scoreTotal;
        public int nivelMaximo;
        public string fecha;
    }

    [System.Serializable]
    public class RankingData {

        public List<RankingEntry> top10 = new List<RankingEntry>();

        public void AddEntry(string nombre, int score, int nivel) {
            top10.Add(new RankingEntry
            {
                nombre = nombre,
                scoreTotal = score,
                nivelMaximo = nivel,
                fecha = System.DateTime.Now.ToString("dd/MM HH:mm")
            });
            top10.Sort((a, b) => b.scoreTotal.CompareTo(a.scoreTotal));
            if (top10.Count > 10) top10.RemoveRange(10, top10.Count - 10);
            Save();
        }
        public void Save() {
            string path = Application.persistentDataPath + "/ranking.json";
            string json = JsonUtility.ToJson(this, true);
            File.WriteAllText(path, json);
            Debug.Log("Ranking guardado en: " + path);
            Debug.Log("Contenido:\n" + json);
        }
        public void Load() {
            string path = Application.persistentDataPath + "/ranking.json";
            if (File.Exists(path)) {

                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reproducir música al cambiar escena
        if (GetComponent<AudioSource>() != null && !GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}