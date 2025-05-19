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
    public TextMeshProUGUI timerTexto;

    [Header("Botones Pantallas")]
    public Button botonMenuGameOver;
    public Button botonMenuVictoria;
    public Button botonSiguienteNivel;

    [Header("Sonidos")]
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    public AudioClip sonidoVictoria;
    public AudioClip sonidoDerrota;

    [Header("Configuraci�n del juego")]
    public int vidasIniciales = 3;
    public int preguntasParaGanar = 5;
    public float tiempoPorPregunta = 20f;

    private int vidasActuales;
    private int preguntasCorrectas;
    private float tiempoRestante;
    private bool juegoActivo = true;
    private AudioSource audioSource;
    private ProblemaManager problemaManager;

    void Start()
    {
        vidasActuales = vidasIniciales;
        preguntasCorrectas = 0;
        tiempoRestante = tiempoPorPregunta;
        juegoActivo = true;
        audioSource = GetComponent<AudioSource>();
        problemaManager = FindObjectOfType<ProblemaManager>();

        pantallaGameOver.SetActive(false);
        pantallaVictoria.SetActive(false);
        feedbackTexto.text = "";
        ActualizarVidasUI();

        if (botonMenuGameOver != null)
            botonMenuGameOver.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        if (botonMenuVictoria != null)
            botonMenuVictoria.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        if (botonSiguienteNivel != null)
            botonSiguienteNivel.onClick.AddListener(() => SceneManager.LoadScene("Nivel2"));
    }

    void Update()
    {
        if (!juegoActivo) return;

        tiempoRestante -= Time.deltaTime;
        if (timerTexto != null)
            timerTexto.text = $"Tiempo: {Mathf.CeilToInt(tiempoRestante)}s";

        if (tiempoRestante <= 0)
        {
            GameOver();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!juegoActivo) return;

        GameObject dragged = eventData.pointerDrag;
        if (dragged == null) return;

        Ingrediente ingrediente = dragged.GetComponent<Ingrediente>();

        if (ingrediente != null)
        {
            Destroy(dragged);

            if (ingrediente.EsCorrecto())
            {
                preguntasCorrectas++;
                feedbackTexto.text = "�Correcto!";
                feedbackTexto.color = Color.green;

                if (audioSource && sonidoCorrecto)
                    audioSource.PlayOneShot(sonidoCorrecto);

                if (preguntasCorrectas >= preguntasParaGanar)
                    Invoke(nameof(MostrarPantallaVictoria), 1.2f);
                else
                    ReiniciarRonda();
            }
            else
            {
                vidasActuales--;
                ActualizarVidasUI();

                feedbackTexto.text = "Incorrecto. Intenta otra vez.";
                feedbackTexto.color = Color.red;

                if (audioSource && sonidoIncorrecto)
                    audioSource.PlayOneShot(sonidoIncorrecto);

                if (vidasActuales <= 0)
                    Invoke(nameof(GameOver), 1.2f);
                else
                    ReiniciarRonda();
            }
        }
    }

    void ActualizarVidasUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = $"Vidas: {vidasActuales}";
    }

    void ReiniciarRonda()
    {
        tiempoRestante = tiempoPorPregunta;
        foreach (Transform slot in problemaManager.spawnSlots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }
        problemaManager.MostrarProblemaYSpawn();
    }

    void GameOver()
    {
        juegoActivo = false;
        pantallaGameOver.SetActive(true);
        feedbackTexto.text = "Has perdido todas tus vidas o el tiempo.";
        feedbackTexto.color = Color.red;

        if (audioSource && sonidoDerrota)
            audioSource.PlayOneShot(sonidoDerrota);
    }

    void MostrarPantallaVictoria()
    {
        juegoActivo = false;
        pantallaVictoria.SetActive(true);
        feedbackTexto.text = "�Ganaste!";
        feedbackTexto.color = Color.green;

        if (audioSource && sonidoVictoria)
            audioSource.PlayOneShot(sonidoVictoria);

        PlayerPrefs.SetInt("NivelSuperado", 1);
    }
}
