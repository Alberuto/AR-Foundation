using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SettingsManager : MonoBehaviour {

    public static SettingsManager Instance; //Singleton

    [Header("UI SETTINGS")]
    public Toggle toggleInfinita;
    public TMP_InputField inputVidas;
    public Slider sliderVolumenMusica;
    public TMP_Dropdown dropdownGraficos;
    public Button btnResetRanking;

    [Header("AUDIO")]
    public AudioSource musicaFondo; // ← Drag MenuManager AudioSource
    private void Awake() {

        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    void Start() {
        CargarSettings();
    }
    public void ToggleInfinita(bool activo) {

        PlayerPrefs.SetInt("PartidaInfinita", activo ? 1 : 0);
        Debug.Log($"Partida Infinita: {(activo ? "ON" : "OFF")}");
    }
    public void CambiarVidas(string texto) {

        if (int.TryParse(texto, out int vidas) && vidas > 0 && vidas < 10) {

            PlayerPrefs.SetInt("VidasPersonalizadas", vidas);
            if (GameManager.Instance != null) {
                GameManager.Instance.vidas = vidas;
            }
            Debug.Log($"Vidas: {vidas}");
        }
    }
    public void SliderVolumenMusica(float valor) {

        PlayerPrefs.SetFloat("VolumenMusica", valor);
        if (musicaFondo != null) {
            musicaFondo.volume = valor;
        }
        AudioListener.volume = valor;
        Debug.Log($"Volumen Música: {valor:P0}");
    }
    public void DropdownGraficos(int indice) {

        QualitySettings.SetQualityLevel(indice);
        PlayerPrefs.SetInt("CalidadGrafica", indice);
        Debug.Log($"Calidad Gráfica: {QualitySettings.names[indice]}");
    }
    public void ResetRanking() {

        string path = Application.persistentDataPath + "/ranking.json";
        if (File.Exists(path)) {
            File.Delete(path);
            Debug.Log("✅ Ranking RESETEADO completamente");
        }
        else {
            Debug.Log("ℹ️ No hay ranking para resetear");
        }
    }
    public void CargarSettings() {

        if (toggleInfinita != null) {
            toggleInfinita.isOn = PlayerPrefs.GetInt("PartidaInfinita", 0) == 1;
        }
        if (inputVidas != null) {
            int vidas = PlayerPrefs.GetInt("VidasPersonalizadas", 3);
            inputVidas.text = vidas.ToString();
            if (GameManager.Instance != null) {
                GameManager.Instance.vidas = vidas;
            }
        }
        if (sliderVolumenMusica != null) {
            float volumen = PlayerPrefs.GetFloat("VolumenMusica", 0.7f);
            sliderVolumenMusica.value = volumen;
            if (musicaFondo != null) {
                musicaFondo.volume = volumen;
            }
            AudioListener.volume = volumen;
        }
        if (dropdownGraficos != null) {

            int calidad = PlayerPrefs.GetInt("CalidadGrafica", 2);
            dropdownGraficos.value = calidad;
            QualitySettings.SetQualityLevel(calidad);
        }
    }
    public void GuardarSettings() {

        if (inputVidas != null && int.TryParse(inputVidas.text, out int vidas)) {
            if (vidas > 0 && vidas < 10) {
                PlayerPrefs.SetInt("VidasPersonalizadas", vidas);
                Debug.Log($"✅ Vidas guardadas: {vidas}");
            }
        }
    }
}