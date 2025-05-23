using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class ProblemaNumerico
{
    public Sprite imagenProblema;
    public string[] opciones = new string[4];
    public int respuestaCorrecta;
}

public class PreguntaSorpresa : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Image imagenProblema;
    [SerializeField] private Button[] botonesOpciones;
    [SerializeField] private TextMeshProUGUI[] textosOpciones;

    [Header("Configuración")]
    [SerializeField] private float tiempoMostrarPregunta = 3f;
    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private GameObject panelSiguienteNivel;
    [SerializeField] private float maxWidth = 800f;  // Ancho máximo de la imagen
    [SerializeField] private float maxHeight = 600f; // Alto máximo de la imagen
    [SerializeField] private float padding = 20f;    // Espacio alrededor de la imagen

    [Header("Problemas")]
    [SerializeField] private List<ProblemaNumerico> problemas = new List<ProblemaNumerico>();

    private bool preguntaRespondida = false;
    private int respuestaCorrecta = -1;
    private ItemSlot itemSlot;
    private Color colorNormal = new Color(0.2f, 0.2f, 0.2f);

    private void Start()
    {
        OcultarPanel();
        itemSlot = FindObjectOfType<ItemSlot>();

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int index = i;
            botonesOpciones[i].onClick.AddListener(() => ResponderPregunta(index));
            botonesOpciones[i].GetComponent<Image>().color = colorNormal;
        }
    }

    private void AjustarTamañoImagen(Sprite sprite)
    {
        if (sprite == null) return;

        RectTransform rectTransform = imagenProblema.GetComponent<RectTransform>();
        float aspectRatio = (float)sprite.texture.width / sprite.texture.height;

        // Calcular el tamaño máximo disponible
        float availableWidth = maxWidth - (padding * 2);
        float availableHeight = maxHeight - (padding * 2);

        // Calcular el tamaño final manteniendo la proporción
        float finalWidth, finalHeight;

        if (aspectRatio > 1) // Imagen más ancha que alta
        {
            finalWidth = Mathf.Min(availableWidth, sprite.texture.width);
            finalHeight = finalWidth / aspectRatio;
        }
        else // Imagen más alta que ancha
        {
            finalHeight = Mathf.Min(availableHeight, sprite.texture.height);
            finalWidth = finalHeight * aspectRatio;
        }

        // Aplicar el tamaño
        rectTransform.sizeDelta = new Vector2(finalWidth, finalHeight);
    }

    public void MostrarPreguntaSorpresa()
    {
        preguntaRespondida = false;
        respuestaCorrecta = -1;

        panelPregunta.SetActive(true);
        CargarProblemaAleatorio();

        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        if (panelSiguienteNivel != null)
            panelSiguienteNivel.SetActive(false);

        foreach (Button boton in botonesOpciones)
        {
            boton.interactable = true;
            boton.GetComponent<Image>().color = colorNormal;
        }
    }

    private void CargarProblemaAleatorio()
    {
        if (problemas.Count == 0)
        {
            Debug.LogError("No hay problemas configurados en el Inspector!");
            return;
        }

        int problemaIndex = Random.Range(0, problemas.Count);
        ProblemaNumerico problemaActual = problemas[problemaIndex];
        
        imagenProblema.sprite = problemaActual.imagenProblema;
        AjustarTamañoImagen(problemaActual.imagenProblema);
        respuestaCorrecta = problemaActual.respuestaCorrecta;

        // Crear array temporal para las opciones mezcladas
        string[] opcionesMezcladas = new string[4];
        int[] posiciones = new int[4];
        
        // Inicializar posiciones
        for (int i = 0; i < 4; i++)
        {
            posiciones[i] = i;
        }

        // Mezclar posiciones
        for (int i = posiciones.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = posiciones[i];
            posiciones[i] = posiciones[j];
            posiciones[j] = temp;
        }

        // Asignar opciones a las posiciones mezcladas
        for (int i = 0; i < 4; i++)
        {
            opcionesMezcladas[posiciones[i]] = problemaActual.opciones[i];
        }

        // Asignar opciones mezcladas a los botones
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            textosOpciones[i].text = opcionesMezcladas[i];
        }

        // Actualizar el índice de la respuesta correcta según la nueva posición
        for (int i = 0; i < 4; i++)
        {
            if (posiciones[i] == respuestaCorrecta)
            {
                respuestaCorrecta = i;
                break;
            }
        }
    }

    private void ResponderPregunta(int opcionSeleccionada)
    {
        if (preguntaRespondida) return;
        preguntaRespondida = true;

        foreach (Button boton in botonesOpciones)
        {
            boton.interactable = false;
        }

        if (opcionSeleccionada == respuestaCorrecta)
        {
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f);
            
            if (itemSlot != null)
            {
                itemSlot.IncrementarPreguntasCorrectas();
            }

            ManejarRespuestaCorrecta();
        }
        else
        {
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.8f, 0.2f, 0.2f);
            botonesOpciones[respuestaCorrecta].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f);
            
            if (itemSlot != null)
            {
                itemSlot.PerderVida();
            }
            
            Invoke("OcultarPanel", tiempoMostrarPregunta);
        }
    }

    private void ManejarRespuestaCorrecta()
    {
        if (itemSlot != null)
        {
            itemSlot.ContinuarDespuesDePreguntaCorrecta();
        }
        OcultarPanel();
    }

    public void OcultarPanel()
    {
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(false);
        }
    }
} 