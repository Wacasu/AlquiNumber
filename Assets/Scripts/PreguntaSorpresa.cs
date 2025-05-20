using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PreguntaSorpresa : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private TextMeshProUGUI textoPregunta;
    [SerializeField] private Button[] botonesOpciones;
    [SerializeField] private TextMeshProUGUI[] textosOpciones;

    [Header("Configuración")]
    [SerializeField] private float tiempoMostrarPregunta = 3f;
    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private GameObject panelSiguienteNivel;

    private bool preguntaRespondida = false;
    private int respuestaCorrecta = -1;
    private ItemSlot itemSlot;
    private Color colorNormal = new Color(0.2f, 0.2f, 0.2f); // Color oscuro para los botones

    private void Start()
    {
        // Ocultar panel al inicio
        OcultarPanel();

        // Obtener referencia al ItemSlot
        itemSlot = FindObjectOfType<ItemSlot>();

        // Configurar listeners de botones
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int index = i; // Necesario para el closure
            botonesOpciones[i].onClick.AddListener(() => ResponderPregunta(index));
            // Establecer color inicial de los botones
            botonesOpciones[i].GetComponent<Image>().color = colorNormal;
        }
    }

    public void MostrarPreguntaSorpresa()
    {
        preguntaRespondida = false;
        respuestaCorrecta = -1;

        // Activar panel
        panelPregunta.SetActive(true);

        // Cargar pregunta aleatoria
        CargarPreguntaAleatoria();

        // Desactivar otros paneles
        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        if (panelSiguienteNivel != null)
            panelSiguienteNivel.SetActive(false);

        // Reactivar todos los botones y restaurar color normal
        foreach (Button boton in botonesOpciones)
        {
            boton.interactable = true;
            boton.GetComponent<Image>().color = colorNormal;
        }
    }

    private void CargarPreguntaAleatoria()
    {
        // Aquí puedes cargar preguntas desde un archivo o tenerlas hardcodeadas
        string[] preguntas = {
            "¿Cuál es el método más eficiente para encontrar raíces de ecuaciones no lineales?",
            "¿Qué método numérico es mejor para sistemas de ecuaciones lineales grandes?",
            "¿Cuál es el método más preciso para integración numérica?",
            "¿Qué método es más estable para ecuaciones diferenciales?"
        };

        string[][] opciones = {
            new string[] { "Newton-Raphson", "Bisección", "Punto Fijo", "Secante" },
            new string[] { "Gauss-Seidel", "Jacobi", "Eliminación Gaussiana", "Montante" },
            new string[] { "Simpson 3/8", "Trapecio", "Simpson 1/3", "Newton-Cotes" },
            new string[] { "Runge-Kutta 4", "Euler", "Euler Modificado", "Runge-Kutta 2" }
        };

        int[] respuestasCorrectas = { 0, 2, 0, 0 };

        int preguntaIndex = Random.Range(0, preguntas.Length);
        textoPregunta.text = preguntas[preguntaIndex];
        respuestaCorrecta = respuestasCorrectas[preguntaIndex];

        // Mezclar opciones
        List<int> indices = new List<int> { 0, 1, 2, 3 };
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        // Asignar opciones a los botones
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            textosOpciones[i].text = opciones[preguntaIndex][indices[i]];
            if (indices[i] == respuestaCorrecta)
                respuestaCorrecta = i;
        }
    }

    private void ResponderPregunta(int opcionSeleccionada)
    {
        if (preguntaRespondida) return;
        preguntaRespondida = true;

        // Desactivar todos los botones
        foreach (Button boton in botonesOpciones)
        {
            boton.interactable = false;
        }

        // Mostrar resultado
        if (opcionSeleccionada == respuestaCorrecta)
        {
            // Respuesta correcta
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f); // Verde oscuro
            
            // Incrementar puntuación ANTES de reanudar el juego
            if (itemSlot != null)
            {
                itemSlot.IncrementarPreguntasCorrectas();
            }

            // Invoke("ContinuarJuego", tiempoMostrarPregunta);
            Invoke("ManejarRespuestaCorrecta", tiempoMostrarPregunta);
        }
        else
        {
            // Respuesta incorrecta
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.8f, 0.2f, 0.2f); // Rojo oscuro
            botonesOpciones[respuestaCorrecta].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f); // Verde oscuro
            
            // Restar vida inmediatamente
            if (itemSlot != null)
            {
                itemSlot.PerderVida();
            }
            
            // Ocultar panel después de mostrar la respuesta
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

    //private void ContinuarJuego()
    //{
    //    if (itemSlot != null)
    //    {
    //        itemSlot.ContinuarDespuesDePreguntaCorrecta();
    //    }
    //    OcultarPanel();
    //}

    public void OcultarPanel()
    {
        if (panelPregunta != null)
        {
            panelPregunta.SetActive(false);
        }
    }
} 