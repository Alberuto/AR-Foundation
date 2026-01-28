using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImpactEffect : MonoBehaviour {

    [Header("Efectos Básicos")]
    public Image panelRojo;
    public Image panelVerde;
    private float duracionEfecto = 1.5f;

    [Header("GAME OVER")]
    public GameObject panelDerrota;
    public float delayGameOver = 0.5f;

    public void ImpactoEnemigo() {
        StartCoroutine(MostrarEfecto(panelRojo, Color.red));
    }
    public void ImpactoPowerUp() {
        StartCoroutine(MostrarEfecto(panelVerde, Color.green));
    }
    IEnumerator MostrarEfecto(Image panel, Color colorObjetivo) {
        // Activar panel
        panel.gameObject.SetActive(true);
        panel.color = new Color(colorObjetivo.r, colorObjetivo.g, colorObjetivo.b, 0.3f);

        // Fade out suave
        float tiempo = 0;
        while (tiempo < duracionEfecto) {
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0f, tiempo / duracionEfecto);
            panel.color = new Color(colorObjetivo.r, colorObjetivo.g, colorObjetivo.b, alpha);
            yield return null;
        }
        // Desactivar
        panel.gameObject.SetActive(false);
    }

    public void GameOver() {
        StartCoroutine(GameOverConRojo());
    }
    IEnumerator GameOverConRojo() {

        panelRojo.gameObject.SetActive(true);
        panelRojo.color = new Color(1, 0, 0, 0.3f);

        float tiempo = 0;
        while (tiempo < 1.5f)
        { //1.5s TOTAL rojo
            tiempo += Time.deltaTime;
            float alpha = Mathf.Lerp(0.3f, 0f, tiempo / 1.5f);
            panelRojo.color = new Color(1, 0, 0, alpha);
            yield return null;
        }
        panelRojo.gameObject.SetActive(false);

        // 2. Delay para limpiar raycast
        yield return new WaitForSeconds(delayGameOver);

        Time.timeScale = 0f;

        // 3. Panel derrota
        if (panelDerrota != null) {
            panelDerrota.SetActive(true);
        }
    }
}