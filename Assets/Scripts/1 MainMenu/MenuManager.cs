using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;

public class MenuManager : MonoBehaviour {

    public static MenuManager Instance; //Singleton

    [Header("RANKING UI")]
    public GameObject panelRanking;
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
        SceneManager.LoadScene("2 GameScene");
    }
    public void Salir() {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
    public void Configuracion() {
        Debug.Log("Configuración (próximamente)");
    }
    public void Menu() {
        SceneManager.LoadScene(0);
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
    public static void GuardarRankingDirecto(string nombre, int score, int nivel) {

        RankingData data = new RankingData();
        data.AddEntry(nombre, score, nivel);
    }
    public void MostrarRanking() {
        RankingData data = new RankingData();
        data.Load();

        // Limpiar
        foreach (Transform child in rankingContainer)
        {
            Destroy(child.gameObject);
        }

        // Generar TOP 10
        for (int i = 0; i < data.top10.Count; i++)
        {
            GameObject entry = Instantiate(rankingEntryPrefab, rankingContainer);
            TMP_Text texto = entry.GetComponentInChildren<TMP_Text>();
            texto.text = $"{i + 1}. {data.top10[i].nombre} - {data.top10[i].scoreTotal}pts (N:{data.top10[i].nivelMaximo})";
        }
        panelRanking.SetActive(true);
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
        }
        public void Load() {
            string path = Application.persistentDataPath + "/ranking.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, this);
            }
        }
    }
}