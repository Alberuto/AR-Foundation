using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImpactEffect : MonoBehaviour {

    public Image panelRojo;
    public Image panelVerde;
    public float duracionEfecto = 0.5f;

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
}
