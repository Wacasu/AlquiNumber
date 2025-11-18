using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controlador para la selección de niveles de puzzle.
/// Integra los puzzles con el sistema de niveles del juego principal.
/// </summary>
public class PuzzleMenuController : MonoBehaviour
{
    [Header("Botones de Niveles")]
    [Tooltip("Array de botones para seleccionar niveles de puzzle")]
    public Button[] botonesNivelesPuzzle;

    [Header("UI")]
    [Tooltip("Texto que muestra información sobre el nivel")]
    public TextMeshProUGUI textoInfoNivel;

    [Header("Configuración")]
    [Tooltip("Nombre de la escena donde se juega el puzzle")]
    public string nombreEscenaPuzzle = "PuzzleNivel";

    [Tooltip("Número máximo de niveles de puzzle desbloqueados")]
    [SerializeField] private int maxNivelesDesbloqueados = 10;

    void Start()
    {
        // Cargar progreso de puzzles desbloqueados
        CargarProgresoPuzzles();

        // Configurar botones
        ConfigurarBotones();
    }

    void CargarProgresoPuzzles()
    {
        // El primer nivel siempre está desbloqueado
        maxNivelesDesbloqueados = 1;

        // Verificar cuántos niveles están completados
        for (int i = 0; i < botonesNivelesPuzzle.Length; i++)
        {
            int nivelCompletado = PlayerPrefs.GetInt($"PuzzleNivel{i}Completado", 0);
            if (nivelCompletado == 1)
            {
                // Si el nivel está completado, el siguiente está desbloqueado
                maxNivelesDesbloqueados = Mathf.Max(maxNivelesDesbloqueados, i + 2);
            }
        }

        // También considerar el progreso del jugador principal
        if (PlayerProgress.Instance != null)
        {
            // Desbloquear más niveles según el progreso general
            int nivelMaxDesbloqueado = PlayerProgress.Instance.nivelMaxDesbloqueado;
            if (nivelMaxDesbloqueado > maxNivelesDesbloqueados)
            {
                maxNivelesDesbloqueados = Mathf.Min(nivelMaxDesbloqueado, botonesNivelesPuzzle.Length);
            }
        }

        // Limitar al número de botones disponibles
        maxNivelesDesbloqueados = Mathf.Min(maxNivelesDesbloqueados, botonesNivelesPuzzle.Length);
    }

    void ConfigurarBotones()
    {
        for (int i = 0; i < botonesNivelesPuzzle.Length; i++)
        {
            if (botonesNivelesPuzzle[i] != null)
            {
                int nivelIndex = i; // Capturar para el closure

                // Habilitar o deshabilitar botón según si está desbloqueado
                bool estaDesbloqueado = (i < maxNivelesDesbloqueados);
                botonesNivelesPuzzle[i].interactable = estaDesbloqueado;

                // Configurar texto del botón
                TextMeshProUGUI textoBoton = botonesNivelesPuzzle[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textoBoton != null)
                {
                    if (estaDesbloqueado)
                    {
                        // Verificar si está completado
                        bool estaCompletado = PlayerPrefs.GetInt($"PuzzleNivel{i}Completado", 0) == 1;
                        if (estaCompletado)
                        {
                            textoBoton.text = $"Nivel {i + 1} ✓";
                        }
                        else
                        {
                            textoBoton.text = $"Nivel {i + 1}";
                        }
                    }
                    else
                    {
                        textoBoton.text = $"Nivel {i + 1} 🔒";
                    }
                }

                // Configurar listener
                botonesNivelesPuzzle[i].onClick.RemoveAllListeners();
                if (estaDesbloqueado)
                {
                    botonesNivelesPuzzle[i].onClick.AddListener(() => CargarNivelPuzzle(nivelIndex));
                }
                else
                {
                    // Mostrar mensaje de bloqueado
                    botonesNivelesPuzzle[i].onClick.AddListener(() => MostrarMensajeBloqueado(nivelIndex));
                }
            }
        }
    }

    void CargarNivelPuzzle(int nivelIndex)
    {
        // Guardar el nivel seleccionado
        PlayerPrefs.SetInt("PuzzleNivel", nivelIndex);

        // Cargar la escena del puzzle
        SceneManager.LoadScene(nombreEscenaPuzzle);
    }

    void MostrarMensajeBloqueado(int nivelIndex)
    {
        if (textoInfoNivel != null)
        {
            textoInfoNivel.text = $"El nivel {nivelIndex + 1} está bloqueado. Completa el nivel anterior para desbloquearlo.";
            textoInfoNivel.color = Color.red;
        }
    }

    public void RegresarMenu()
    {
        SceneManager.LoadScene("MenuInicial");
    }
}

