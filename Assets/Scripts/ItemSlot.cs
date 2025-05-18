using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [Header("UI")]
    public TextMeshProUGUI feedbackTexto;
    public GameObject pantallaGameOver;
    public GameObject pantallaVictoria;
    public TextMeshProUGUI vidasTexto;

    [Header("Botones Pantallas")]
    public Button botonMenuGameOver;
    public Button botonMenuVictoria;
    public Button botonSiguienteNivel;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    public AudioClip sonidoVictoria;
    public AudioClip sonidoDerrota;

    [Header("Configuración del juego")]
    public int vidasIniciales = 3;
    public int preguntasParaGanar = 5;

    private int vidasActuales;
    private int preguntasCorrectas;
    private AudioSource audioSource;

    void Start()
    {
        vidasActuales = vidasIniciales;
        preguntasCorrectas = 0;
        audioSource = GetComponent<AudioSource>();

        pantallaGameOver.SetActive(false);
        pantallaVictoria.SetActive(false);
        feedbackTexto.text = "";
        ActualizarVidasUI();

        // Botones
        if (botonMenuGameOver != null)
            botonMenuGameOver.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        if (botonMenuVictoria != null)
            botonMenuVictoria.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        if (botonSiguienteNivel != null)
            botonSiguienteNivel.onClick.AddListener(() => SceneManager.LoadScene("Nivel2"));
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dragged = eventData.pointerDrag;
        if (dragged == null) return;

        Ingrediente ingrediente = dragged.GetComponent<Ingrediente>();

        if (ingrediente != null)
        {
            if (ingrediente.EsCorrecto())
            {
                preguntasCorrectas++;
                feedbackTexto.text = "¡Correcto!";
                feedbackTexto.color = Color.green;

                if (audioSource && sonidoCorrecto)
                    audioSource.PlayOneShot(sonidoCorrecto);

                Destroy(dragged);

                if (preguntasCorrectas >= preguntasParaGanar)
                {
                    Invoke(nameof(MostrarPantallaVictoria), 1.2f);
                }
                else
                {
                    Invoke(nameof(RecargarEscena), 1.2f);
                }
            }
            else
            {
                vidasActuales--;
                ActualizarVidasUI();

                feedbackTexto.text = "Incorrecto. Intenta otra vez.";
                feedbackTexto.color = Color.red;

                if (audioSource && sonidoIncorrecto)
                    audioSource.PlayOneShot(sonidoIncorrecto);

                Destroy(dragged);

                if (vidasActuales <= 0)
                {
                    Invoke(nameof(GameOver), 1.2f);
                }
            }
        }
    }

    void ActualizarVidasUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = $"Vidas: {vidasActuales}";
    }

    void RecargarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GameOver()
    {
        pantallaGameOver.SetActive(true);
        feedbackTexto.text = "Has perdido todas tus vidas.";
        feedbackTexto.color = Color.red;

        if (audioSource && sonidoDerrota)
            audioSource.PlayOneShot(sonidoDerrota);
    }

    void MostrarPantallaVictoria()
    {
        pantallaVictoria.SetActive(true);
        feedbackTexto.text = "¡Ganaste!";
        feedbackTexto.color = Color.green;

        if (audioSource && sonidoVictoria)
            audioSource.PlayOneShot(sonidoVictoria);

        PlayerPrefs.SetInt("NivelSuperado", 1); // Marca como superado
    }
}
