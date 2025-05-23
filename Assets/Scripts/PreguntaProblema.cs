using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PreguntaProblema : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private Sprite[] preguntasSprites;  // Reemplaza el array de strings por sprites
    [SerializeField] private Image imagenPregunta;       // Asigna este componente desde el inspector

    public Text[] textosOpciones; // Asignar desde el inspector
    public Button[] botonesOpciones; // Asignar desde el inspector

    private int respuestaCorrecta;


    [Header("Configuración")]
    [SerializeField] private float tiempoMostrarPregunta = 3f;
    [SerializeField] private GameObject panelVictoria;
    [SerializeField] private GameObject panelSiguienteNivel;

    private bool preguntaRespondida = false;
    //private int respuestaCorrecta = -1;
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

    public class PreguntaConImagen
    {
        public Sprite imagenPregunta;
        public Sprite[] opciones; // Deben ser 4 sprites
        public int indiceRespuestaCorrecta; // Valor entre 0 y 3
    }

    public void MostrarPreguntaProblema()
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

    public void CargarPreguntaAleatoria()
    {
      
        

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

            // Incrementar puntuación
            if (itemSlot != null)
            {
                itemSlot.IncrementarPreguntasCorrectas();
            }

            ManejarRespuestaCorrecta(); // Puedes mostrar animación, efectos, pasar al siguiente nivel, etc.
        }
        else
        {
            // Respuesta incorrecta
            botonesOpciones[opcionSeleccionada].GetComponent<Image>().color = new Color(0.8f, 0.2f, 0.2f); // Rojo oscuro
            botonesOpciones[respuestaCorrecta].GetComponent<Image>().color = new Color(0.2f, 0.8f, 0.2f); // Verde oscuro

            if (itemSlot != null)
            {
                itemSlot.PerderVida();
            }

            Invoke("OcultarPanel", tiempoMostrarPregunta); // Retraso para mostrar resultado
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