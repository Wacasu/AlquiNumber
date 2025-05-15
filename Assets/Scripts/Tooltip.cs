using UnityEngine;
using TMPro; // Si usas TextMeshPro, si no usar UnityEngine.UI y Text

public class Tooltip : MonoBehaviour
{
    public GameObject panel;     // Panel que contiene el texto
    public TextMeshProUGUI texto; // Texto donde aparece el nombre

    private RectTransform panelRect;

    private void Awake()
    {
        panelRect = panel.GetComponent<RectTransform>();
        panel.SetActive(false);
    }

    public void Mostrar(string mensaje, Vector3 posicion)
    {
        panel.SetActive(true);
        texto.text = mensaje;

        // Mover el panel cerca del mouse
        panelRect.position = posicion + new Vector3(10f, -10f, 0f);
    }

    public void Ocultar()
    {
        panel.SetActive(false);
    }
}
