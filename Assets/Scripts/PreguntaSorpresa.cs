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
    [SerializeField] private float maxWidth = 1200f;  // Aumentado de 800 a 1200
    [SerializeField] private float maxHeight = 900f;  // Aumentado de 600 a 900
    [SerializeField] private float padding = 30f;     // Aumentado de 20 a 30 para mejor espaciado

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

        // Configurar el componente Image
        imagenProblema.preserveAspect = true;
        imagenProblema.type = Image.Type.Simple;
        
        // Configurar el RectTransform con tamaño fijo
        RectTransform rectTransform = imagenProblema.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(650f, 300f);
        
        // Centrar la imagen
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
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

        // Guardar la respuesta correcta original
        string respuestaCorrectaOriginal = problemaActual.opciones[problemaActual.respuestaCorrecta];
        
        // Crear lista de opciones incorrectas
        List<string> opcionesIncorrectas = new List<string>();
        for (int i = 0; i < problemaActual.opciones.Length; i++)
        {
            if (i != problemaActual.respuestaCorrecta)
            {
                opcionesIncorrectas.Add(problemaActual.opciones[i]);
            }
        }

        // Mezclar las opciones incorrectas
        for (int i = opcionesIncorrectas.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            string temp = opcionesIncorrectas[i];
            opcionesIncorrectas[i] = opcionesIncorrectas[j];
            opcionesIncorrectas[j] = temp;
        }

        // Elegir posición aleatoria para la respuesta correcta
        int posicionCorrecta = Random.Range(0, 4);
        
        // Asignar opciones a los botones
        int indiceIncorrectas = 0;
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            if (i == posicionCorrecta)
            {
                textosOpciones[i].text = respuestaCorrectaOriginal;
                respuestaCorrecta = i;
            }
            else
            {
                textosOpciones[i].text = opcionesIncorrectas[indiceIncorrectas];
                indiceIncorrectas++;
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
                itemSlot.ContinuarDespuesDePreguntaCorrecta();
            }
        }
        else
        {
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.8f, 0.2f, 0.2f);
            botonesOpciones[respuestaCorrecta].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f);
            
            if (itemSlot != null)
            {
                itemSlot.PerderVida();
            }
        }
        
        Invoke("OcultarPanel", tiempoMostrarPregunta);
    }

    private void ManejarRespuestaCorrecta()
    {
        OcultarPanel();
    }

    public void OcultarPanel()
    {
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(false);
            preguntaRespondida = false;
        }
    }
} 