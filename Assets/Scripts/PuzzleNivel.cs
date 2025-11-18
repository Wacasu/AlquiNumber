using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controlador principal del nivel de puzzle.
/// Gestiona el rompecabezas, las piezas y la lógica de victoria.
/// Adaptado del proyecto Puzzle original.
/// </summary>
public class PuzzleNivel : MonoBehaviour
{
    [Header("Sprites de Puzzles")]
    [Tooltip("Array de sprites que representan los diferentes niveles de puzzle")]
    public Sprite[] nivelesPuzzle;

    [Header("UI")]
    [Tooltip("Panel que se muestra al completar el puzzle")]
    public GameObject panelVictoria;
    public GameObject panelGameOver;
    
    [Header("Textos UI")]
    public TextMeshProUGUI textoNivelPuzzle;
    public TextMeshProUGUI textoProgreso;
    public TextMeshProUGUI feedbackTexto;

    [Header("Botones")]
    public Button botonMenuPrincipal;
    public Button botonSiguienteNivel;
    public Button botonReintentar;

    [Header("Sonidos")]
    public AudioClip sonidoVictoria;
    public AudioClip sonidoPiezaEncajada;
    private AudioSource audioSource;

    [Header("Configuración")]
    [Tooltip("Número de piezas en el puzzle (por defecto 36 para un puzzle 6x6)")]
    public int totalPiezas = 36;
    [Tooltip("Nivel actual del puzzle (0-9)")]
    [SerializeField] private int nivelActual = 0;

    private GameObject piezaSeleccionada;
    private int capa = 1; // Para el ordenamiento de las piezas
    private int piezasEncajadas = 0;
    private bool juegoCompletado = false;
    private bool juegoActivo = true;

    void Start()
    {
        // Inicializar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Obtener el nivel del puzzle desde PlayerPrefs o usar el nivel actual de la escena
        nivelActual = PlayerPrefs.GetInt("PuzzleNivel", 0);
        
        // Limitar el nivel al rango disponible
        if (nivelActual >= nivelesPuzzle.Length)
        {
            nivelActual = 0;
            PlayerPrefs.SetInt("PuzzleNivel", 0);
        }

        // Configurar botones
        if (botonMenuPrincipal != null)
            botonMenuPrincipal.onClick.AddListener(() => SceneManager.LoadScene("MenuInicial"));
        
        if (botonSiguienteNivel != null)
            botonSiguienteNivel.onClick.AddListener(SiguienteNivel);
        
        if (botonReintentar != null)
            botonReintentar.onClick.AddListener(ReiniciarNivel);

        // Configurar UI
        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        
        if (panelGameOver != null)
            panelGameOver.SetActive(false);

        // Mostrar información del nivel
        if (textoNivelPuzzle != null)
            textoNivelPuzzle.text = $"Puzzle Nivel {nivelActual + 1}";

        // Asignar el sprite del nivel a todas las piezas
        AsignarSpritesAPiezas();
        
        // Actualizar progreso
        ActualizarProgreso();
    }

    void AsignarSpritesAPiezas()
    {
        if (nivelesPuzzle.Length == 0 || nivelActual >= nivelesPuzzle.Length)
        {
            Debug.LogError("No hay sprites de puzzle asignados o el nivel está fuera de rango.");
            return;
        }

        Sprite spriteNivel = nivelesPuzzle[nivelActual];
        
        // Calcular dimensiones del grid (asumiendo un puzzle cuadrado)
        int piezasPorFila = Mathf.RoundToInt(Mathf.Sqrt(totalPiezas)); // 6 para 36 piezas
        int piezasPorColumna = piezasPorFila;

        // Obtener la textura del sprite
        Texture2D texturaOriginal = spriteNivel.texture;
        int anchoOriginal = texturaOriginal.width;
        int altoOriginal = texturaOriginal.height;

        // Calcular el tamaño de cada pieza
        int anchoPieza = anchoOriginal / piezasPorFila;
        int altoPieza = altoOriginal / piezasPorColumna;

        // Obtener el rect del sprite original (puede no ocupar toda la textura)
        Rect rectOriginal = spriteNivel.rect;
        int offsetX = Mathf.RoundToInt(rectOriginal.x);
        int offsetY = Mathf.RoundToInt(rectOriginal.y);

        for (int i = 0; i < totalPiezas; i++)
        {
            GameObject pieza = GameObject.Find($"Pieza ({i})");
            if (pieza != null)
            {
                Transform puzzleTransform = pieza.transform.Find("Puzzle");
                if (puzzleTransform != null)
                {
                    SpriteRenderer spriteRenderer = puzzleTransform.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        // Calcular qué fila y columna corresponde a esta pieza
                        int fila = i / piezasPorFila;
                        int columna = i % piezasPorFila;

                        // Calcular la posición en la textura original
                        int x = offsetX + (columna * anchoPieza);
                        int y = offsetY + ((piezasPorColumna - 1 - fila) * altoPieza); // Invertir Y porque Unity usa coordenadas desde abajo

                        // Crear un nuevo sprite solo con la porción correspondiente
                        Sprite piezaSprite = CrearSpritePieza(texturaOriginal, x, y, anchoPieza, altoPieza, spriteNivel.pixelsPerUnit);
                        
                        if (piezaSprite != null)
                        {
                            spriteRenderer.sprite = piezaSprite;
                        }
                        else
                        {
                            Debug.LogWarning($"No se pudo crear el sprite para la pieza {i}");
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No se encontró la pieza con nombre 'Pieza ({i})'");
            }
        }
    }

    /// <summary>
    /// Crea un sprite a partir de una porción de una textura
    /// </summary>
    Sprite CrearSpritePieza(Texture2D textura, int x, int y, int ancho, int alto, float pixelsPerUnit)
    {
        // Asegurarse de que la textura es legible
        if (textura.isReadable == false)
        {
            Debug.LogError($"La textura '{textura.name}' no es legible. Ve a las propiedades del sprite en Unity y marca 'Read/Write Enabled'.");
            return null;
        }

        // Crear una nueva textura para esta pieza
        Texture2D texturaPieza = new Texture2D(ancho, alto);
        
        // Copiar los píxeles de la porción correspondiente
        Color[] pixeles = textura.GetPixels(x, y, ancho, alto);
        texturaPieza.SetPixels(pixeles);
        texturaPieza.Apply();

        // Crear el sprite
        Sprite spritePieza = Sprite.Create(
            texturaPieza,
            new Rect(0, 0, ancho, alto),
            new Vector2(0.5f, 0.5f), // Pivot en el centro
            pixelsPerUnit
        );

        return spritePieza;
    }

    void Update()
    {
        if (!juegoActivo || juegoCompletado) return;

        // Detectar clic del mouse para seleccionar pieza
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform != null && hit.transform.CompareTag("Puzzle"))
            {
                PiezaPuzzle piezaScript = hit.transform.GetComponent<PiezaPuzzle>();
                if (piezaScript != null && !piezaScript.Encajada)
                {
                    piezaSeleccionada = hit.transform.gameObject;
                    piezaScript.Seleccionada = true;
                    
                    SortingGroup sortingGroup = piezaSeleccionada.GetComponent<SortingGroup>();
                    if (sortingGroup != null)
                    {
                        sortingGroup.sortingOrder = capa;
                        capa++;
                    }
                }
            }
        }

        // Soltar pieza
        if (Input.GetMouseButtonUp(0))
        {
            if (piezaSeleccionada != null)
            {
                PiezaPuzzle piezaScript = piezaSeleccionada.GetComponent<PiezaPuzzle>();
                if (piezaScript != null)
                {
                    piezaScript.Seleccionada = false;
                }
                piezaSeleccionada = null;
            }
        }

        // Mover pieza seleccionada
        if (piezaSeleccionada != null)
        {
            Vector3 raton = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            piezaSeleccionada.transform.position = new Vector3(raton.x, raton.y, 0);
        }

        // Verificar si se completó el puzzle
        if (piezasEncajadas >= totalPiezas && !juegoCompletado)
        {
            CompletarPuzzle();
        }
    }

    public void IncrementarPiezasEncajadas()
    {
        piezasEncajadas++;
        ActualizarProgreso();

        // Reproducir sonido de pieza encajada
        if (audioSource != null && sonidoPiezaEncajada != null)
        {
            audioSource.PlayOneShot(sonidoPiezaEncajada);
        }
    }

    void ActualizarProgreso()
    {
        if (textoProgreso != null)
        {
            textoProgreso.text = $"Progreso: {piezasEncajadas}/{totalPiezas}";
        }
    }

    void CompletarPuzzle()
    {
        juegoCompletado = true;
        juegoActivo = false;

        // Mostrar panel de victoria
        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        if (feedbackTexto != null)
        {
            feedbackTexto.text = "¡Puzzle Completado!";
            feedbackTexto.color = Color.green;
        }

        // Reproducir sonido de victoria
        if (audioSource != null && sonidoVictoria != null)
        {
            audioSource.PlayOneShot(sonidoVictoria);
        }

        // Guardar progreso
        if (PlayerProgress.Instance != null)
        {
            PlayerProgress.Instance.GanarExperiencia(100); // Dar XP por completar
        }

        // Guardar que se completó este nivel
        PlayerPrefs.SetInt($"PuzzleNivel{nivelActual}Completado", 1);
    }

    public void SiguienteNivel()
    {
        nivelActual++;
        
        // Si es el último nivel, volver al primero o ir al menú
        if (nivelActual >= nivelesPuzzle.Length)
        {
            nivelActual = 0;
        }

        PlayerPrefs.SetInt("PuzzleNivel", nivelActual);
        ReiniciarNivel();
    }

    public void ReiniciarNivel()
    {
        // Reiniciar variables
        piezasEncajadas = 0;
        juegoCompletado = false;
        juegoActivo = true;
        capa = 1;

        // Ocultar paneles
        if (panelVictoria != null)
            panelVictoria.SetActive(false);
        
        if (panelGameOver != null)
            panelGameOver.SetActive(false);

        // Reiniciar todas las piezas
        for (int i = 0; i < totalPiezas; i++)
        {
            GameObject pieza = GameObject.Find($"Pieza ({i})");
            if (pieza != null)
            {
                PiezaPuzzle piezaScript = pieza.GetComponent<PiezaPuzzle>();
                if (piezaScript != null)
                {
                    piezaScript.ReiniciarPieza();
                }
            }
        }

        // Actualizar UI
        ActualizarProgreso();
        if (textoNivelPuzzle != null)
            textoNivelPuzzle.text = $"Puzzle Nivel {nivelActual + 1}";
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}

