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
            "¿Qué método es más estable para ecuaciones diferenciales?",
            // Nuevas preguntas añadidas
            "¿Quién fue el primer presidente de los Estados Unidos?",
            "¿En qué año comenzó la Primera Guerra Mundial?",
            "¿Cuál es la capital de Francia?",
            "¿Quién escribió 'Cien años de soledad'?",
            // Preguntas de historia de México
            "¿En qué año se consumó la Independencia de México?",
            "¿Quién es conocido como el Padre de la Patria en México?",
            "¿Durante qué periodo gobernó Porfirio Díaz?",
            "¿Cuál fue el plan proclamado por Francisco I. Madero para iniciar la Revolución Mexicana?",
            // Preguntas de teoría matemática y cultura general
            "¿Qué es un número primo?",
            "¿Cuál es el valor de Pi (aproximadamente)?",
            "¿Cuál es el océano más grande del mundo?",
            "¿Cuál es la montaña más alta sobre el nivel del mar?"
        };

        string[][] opciones = {
            new string[] { "Newton-Raphson", "Bisección", "Punto Fijo", "Secante" },
            new string[] { "Gauss-Seidel", "Jacobi", "Eliminación Gaussiana", "Montante" },
            new string[] { "Simpson 3/8", "Trapecio", "Simpson 1/3", "Newton-Cotes" },
            new string[] { "Runge-Kutta 4", "Euler", "Euler Modificado", "Runge-Kutta 2" },
            // Opciones para las nuevas preguntas
            new string[] { "Abraham Lincoln", "George Washington", "Thomas Jefferson", "John Adams" },
            new string[] { "1914", "1918", "1939", "1945" },
            new string[] { "Londres", "Madrid", "Berlín", "París" },
            new string[] { "Gabriel García Márquez", "Mario Vargas Llosa", "Jorge Luis Borges", "Isabel Allende" },
            // Opciones para las preguntas de historia de México
            new string[] { "1810", "1821", "1910", "1917" },
            new string[] { "Miguel Hidalgo y Costilla", "José María Morelos", "Benito Juárez", "Emiliano Zapata" },
            new string[] { "1857-1872", "1876-1911", "1917-1934", "1940-1952" },
            new string[] { "Plan de Ayala", "Plan de San Luis", "Plan de Guadalupe", "Plan de San Luis Potosí" },
            // Opciones para las preguntas de teoría matemática y cultura general
            new string[] { "Un número divisible solo por 1", "Un número par", "Un número divisible solo por 1 y por sí mismo", "Un número impar" },
            new string[] { "3.0", "3.14", "3.1416", "3.2" },
            new string[] { "Atlántico", "Índico", "Ártico", "Pacífico" },
            new string[] { "K2", "Monte Everest", "Kangchenjunga", "Lhotse" }
        };

        int[] respuestasCorrectas = { 0, 2, 0, 0, 1, 0, 3, 0, 1, 0, 1, 1, 2, 2, 3, 1 };

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
            ManejarRespuestaCorrecta();
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