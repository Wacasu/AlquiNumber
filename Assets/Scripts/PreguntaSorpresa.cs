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
            // Métodos Numéricos
            "¿Cuál es el método más eficiente para encontrar raíces de ecuaciones no lineales?",
            "¿Qué método numérico es mejor para sistemas de ecuaciones lineales grandes?",
            "¿Cuál es el método más preciso para integración numérica?",
            "¿Qué método es más estable para ecuaciones diferenciales?",
            "¿Cuál es el método de interpolación que usa polinomios de Lagrange?",
            "¿Qué método numérico se usa para resolver sistemas de ecuaciones no lineales?",
            "¿Cuál es el método de integración que usa puntos de Gauss?",
            "¿Qué método se usa para aproximar derivadas numéricamente?",
            "¿Cuál es el método de factorización LU?",
            "¿Qué método se usa para resolver ecuaciones diferenciales parciales?",

            // Matemáticas Teóricas
            "¿Qué es un número primo?",
            "¿Cuál es el valor de Pi (aproximadamente)?",
            "¿Qué es una matriz singular?",
            "¿Cuál es la fórmula del teorema de Bayes?",
            "¿Qué es un espacio vectorial?",
            "¿Cuál es la definición de límite en cálculo?",
            "¿Qué es una función continua?",
            "¿Cuál es la regla de la cadena en derivadas?",
            "¿Qué es un número complejo?",
            "¿Cuál es la fórmula de Euler?",

            // Historia de México
            "¿En qué año se consumó la Independencia de México?",
            "¿Quién es conocido como el Padre de la Patria en México?",
            "¿Durante qué periodo gobernó Porfirio Díaz?",
            "¿Cuál fue el plan proclamado por Francisco I. Madero para iniciar la Revolución Mexicana?",
            "¿En qué año se promulgó la Constitución de 1917?",
            "¿Quién fue el último emperador de México?",
            "¿En qué año ocurrió la Batalla de Puebla?",
            "¿Quién fue el primer presidente de México?",
            "¿En qué año se inició la Guerra de Reforma?",
            "¿Quién fue conocido como el Benemérito de las Américas?",
            "¿En qué año se firmó el Tratado de Guadalupe Hidalgo?",
            "¿Quién fue el líder del Ejército Libertador del Sur?",
            "¿En qué año se inició la Guerra Cristera?",
            "¿Quién fue el primer gobernador indígena de México?",
            "¿En qué año se nacionalizó el petróleo en México?",

            // Cultura General
            "¿Cuál es el océano más grande del mundo?",
            "¿Cuál es la montaña más alta sobre el nivel del mar?",
            "¿Quién fue el primer presidente de los Estados Unidos?",
            "¿En qué año comenzó la Primera Guerra Mundial?",
            "¿Cuál es la capital de Francia?",
            "¿Quién escribió 'Cien años de soledad'?",
            "¿Cuál es el río más largo del mundo?",
            "¿En qué año se descubrió América?",
            "¿Quién pintó la Mona Lisa?",
            "¿Cuál es el país más grande del mundo?",
            "¿En qué año comenzó la Segunda Guerra Mundial?",
            "¿Quién fue el primer ser humano en el espacio?",
            "¿Cuál es el desierto más grande del mundo?",
            "¿En qué año se fundó la ONU?",
            "¿Quién escribió 'Don Quijote de la Mancha'?"
        };

        string[][] opciones = {
            // Métodos Numéricos
            new string[] { "Newton-Raphson", "Bisección", "Punto Fijo", "Secante" },
            new string[] { "Gauss-Seidel", "Jacobi", "Eliminación Gaussiana", "Montante" },
            new string[] { "Simpson 3/8", "Trapecio", "Simpson 1/3", "Newton-Cotes" },
            new string[] { "Runge-Kutta 4", "Euler", "Euler Modificado", "Runge-Kutta 2" },
            new string[] { "Método de Lagrange", "Método de Newton", "Método de Hermite", "Método de Neville" },
            new string[] { "Método de Newton", "Método de Bisección", "Método de Secante", "Método de Punto Fijo" },
            new string[] { "Cuadratura Gaussiana", "Regla del Trapecio", "Regla de Simpson", "Método de Monte Carlo" },
            new string[] { "Diferencias Finitas", "Método de Euler", "Método de Taylor", "Método de Runge-Kutta" },
            new string[] { "Descomposición LU", "Método de Gauss", "Método de Jacobi", "Método de Seidel" },
            new string[] { "Método de Diferencias Finitas", "Método de Elementos Finitos", "Método de Volúmenes Finitos", "Método de Galerkin" },

            // Matemáticas Teóricas
            new string[] { "Un número divisible solo por 1", "Un número par", "Un número divisible solo por 1 y por sí mismo", "Un número impar" },
            new string[] { "3.0", "3.14", "3.1416", "3.2" },
            new string[] { "Matriz con determinante cero", "Matriz cuadrada", "Matriz simétrica", "Matriz diagonal" },
            new string[] { "P(A|B) = P(B|A)P(A)/P(B)", "P(A|B) = P(A)P(B)", "P(A|B) = P(A)+P(B)", "P(A|B) = P(A)-P(B)" },
            new string[] { "Conjunto con operaciones de suma y multiplicación", "Conjunto de números", "Conjunto de matrices", "Conjunto de funciones" },
            new string[] { "Valor al que se aproxima una función", "Valor máximo", "Valor mínimo", "Valor promedio" },
            new string[] { "Función sin saltos", "Función derivable", "Función integrable", "Función periódica" },
            new string[] { "f'(g(x))g'(x)", "f'(x)g'(x)", "f(x)g'(x)", "f'(x)g(x)" },
            new string[] { "a + bi", "a - bi", "a * bi", "a / bi" },
            new string[] { "e^(ix) = cos(x) + i*sin(x)", "e^x = cos(x) + sin(x)", "e^(ix) = cos(x) - i*sin(x)", "e^x = cos(x) * sin(x)" },

            // Historia de México
            new string[] { "1810", "1821", "1910", "1917" },
            new string[] { "Miguel Hidalgo y Costilla", "José María Morelos", "Benito Juárez", "Emiliano Zapata" },
            new string[] { "1857-1872", "1876-1911", "1917-1934", "1940-1952" },
            new string[] { "Plan de Ayala", "Plan de San Luis", "Plan de Guadalupe", "Plan de San Luis Potosí" },
            new string[] { "1910", "1917", "1920", "1929" },
            new string[] { "Maximiliano I", "Agustín I", "Carlos I", "Fernando I" },
            new string[] { "1862", "1867", "1876", "1910" },
            new string[] { "Guadalupe Victoria", "Benito Juárez", "Porfirio Díaz", "Venustiano Carranza" },
            new string[] { "1857", "1860", "1863", "1867" },
            new string[] { "Benito Juárez", "Miguel Hidalgo", "José María Morelos", "Emiliano Zapata" },
            new string[] { "1848", "1854", "1862", "1867" },
            new string[] { "Emiliano Zapata", "Pancho Villa", "Venustiano Carranza", "Álvaro Obregón" },
            new string[] { "1926", "1929", "1932", "1935" },
            new string[] { "Benito Juárez", "Miguel Hidalgo", "José María Morelos", "Emiliano Zapata" },
            new string[] { "1938", "1940", "1942", "1946" },

            // Cultura General
            new string[] { "Atlántico", "Índico", "Ártico", "Pacífico" },
            new string[] { "K2", "Monte Everest", "Kangchenjunga", "Lhotse" },
            new string[] { "Abraham Lincoln", "George Washington", "Thomas Jefferson", "John Adams" },
            new string[] { "1914", "1918", "1939", "1945" },
            new string[] { "Londres", "Madrid", "Berlín", "París" },
            new string[] { "Gabriel García Márquez", "Mario Vargas Llosa", "Jorge Luis Borges", "Isabel Allende" },
            new string[] { "Amazonas", "Nilo", "Misisipi", "Yangtsé" },
            new string[] { "1492", "1498", "1500", "1502" },
            new string[] { "Leonardo da Vinci", "Miguel Ángel", "Rafael", "Donatello" },
            new string[] { "China", "Rusia", "Canadá", "Estados Unidos" },
            new string[] { "1939", "1941", "1942", "1945" },
            new string[] { "Yuri Gagarin", "Neil Armstrong", "Alan Shepard", "John Glenn" },
            new string[] { "Sahara", "Gobi", "Atacama", "Antártida" },
            new string[] { "1945", "1946", "1947", "1948" },
            new string[] { "Miguel de Cervantes", "William Shakespeare", "Dante Alighieri", "Johann Wolfgang von Goethe" }
        };

        int[] respuestasCorrectas = { 
            0, 2, 0, 0, 0, 0, 0, 0, 0, 0,  // Métodos Numéricos
            2, 2, 0, 0, 0, 0, 0, 0, 0, 0,  // Matemáticas Teóricas
            1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0,  // Historia de México
            3, 1, 1, 0, 3, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0   // Cultura General
        };

        int preguntaIndex = Random.Range(0, preguntas.Length);
        textoPregunta.text = preguntas[preguntaIndex];
        respuestaCorrecta = respuestasCorrectas[preguntaIndex];

        // Mezclar opciones
        List<int> indices = new List<int> { 0, 1, 2, 3 };
        int respuestaOriginal = indices[respuestaCorrecta];
        indices.RemoveAt(respuestaCorrecta);
        
        // Mezclar solo las opciones incorrectas
        for (int i = indices.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }
        
        // Insertar la respuesta correcta en una posición aleatoria
        int posicionCorrecta = Random.Range(0, 4);
        indices.Insert(posicionCorrecta, respuestaOriginal);
        respuestaCorrecta = posicionCorrecta;

        // Asignar opciones a los botones
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            textosOpciones[i].text = opciones[preguntaIndex][indices[i]];
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