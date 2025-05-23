using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [Header("UI")]
    public TextMeshProUGUI feedbackTexto;
    public TextMeshProUGUI txtPuntuacion;
    public GameObject pantallaGameOver;
    public GameObject pantallaVictoria;
    public TextMeshProUGUI vidasTexto;
    public TextMeshProUGUI timerTexto;
    public Image heart1;
    public Image heart2;
    public Image heart3;

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
    public float tiempoPorPregunta = 20f;

    [Header("Pregunta Sorpresa")]
    [SerializeField] private PreguntaSorpresa preguntaSorpresa;

    [Header("Pregunta Problema")]
    [SerializeField] private PreguntaProblema preguntaProblema;


    private int puntos=0;

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
                puntos = puntos + 500;
                txtPuntuacion.text = ""+puntos;
                feedbackTexto.text = "¡Correcto!";
                feedbackTexto.color = Color.green;

                if (audioSource && sonidoCorrecto)
                    audioSource.PlayOneShot(sonidoCorrecto);
                
                // Pausar el juego y mostrar pregunta
                Time.timeScale = 0f;
                if (preguntaSorpresa != null)
                {
                    preguntaSorpresa.MostrarPreguntaSorpresa();
                }
                else
                {
                    // Si no hay pregunta sorpresa, continuar directamente
                    IncrementarPreguntasCorrectas();
                    ContinuarDespuesDePreguntaCorrecta();
                }
            }
            else
            {
                if (preguntaProblema != null)
                {
                    preguntaProblema.MostrarPreguntaProblema();
                }
                else
                {
                    PerderVidaPorProblema();
                }
                
            }
        }
    }

    void ActualizarVidasUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = $"Vidas: {vidasActuales}";
    }

    void ActualizarCorazones()
    {
        if (vidasActuales == 2) {
            Destroy(heart3);
        }
        if (vidasActuales == 1) {
            Destroy(heart2);
        }
        if (vidasActuales == 0) {
            Destroy(heart1);
        }
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
        if (preguntaSorpresa != null)
        {
            preguntaSorpresa.OcultarPanel();
        }
        pantallaGameOver.SetActive(true);
        feedbackTexto.text = "Has perdido todas tus vidas o el tiempo.";
        feedbackTexto.color = Color.red;

        if (audioSource && sonidoDerrota)
            audioSource.PlayOneShot(sonidoDerrota);
    }

    public void MostrarPantallaVictoria()
    {
        juegoActivo = false;
        if (preguntaSorpresa != null)
        {
            preguntaSorpresa.OcultarPanel();
        }
        pantallaVictoria.SetActive(true);
        feedbackTexto.text = "¡Ganaste!";
        feedbackTexto.color = Color.green;

        if (audioSource && sonidoVictoria)
            audioSource.PlayOneShot(sonidoVictoria);

        PlayerPrefs.SetInt("NivelSuperado", 1);
    }

    public void ReanudarJuego()
    {
        Time.timeScale = 1f;
        ReiniciarRonda();
    }

    public void ContinuarDespuesDePreguntaCorrecta()
    {
        Time.timeScale = 1f;
        juegoActivo = true;
        
        if (preguntasCorrectas >= preguntasParaGanar)
        {
            MostrarPantallaVictoria();
        }
        else
        {
            ReiniciarRonda();
            feedbackTexto.text = "";
        }
    }

    public void PerderVida()
    {
        vidasActuales--;
        ActualizarVidasUI();
        ActualizarCorazones();
        
        if (vidasActuales <= 0)
        {
            GameOver();
        }
        else
        {
            ReanudarJuego();
        }
    }

    private void PerderVidaPorProblema()
    {
        vidasActuales--;
        ActualizarVidasUI();
        ActualizarCorazones();

        feedbackTexto.text = "Incorrecto. Intenta otra vez.";
        feedbackTexto.color = Color.red;

        if (audioSource && sonidoIncorrecto)
            audioSource.PlayOneShot(sonidoIncorrecto);

        if (vidasActuales <= 0)
            Invoke(nameof(GameOver), 1.2f);
        else
            ReiniciarRonda();
    }

    public void IncrementarPreguntasCorrectas()
    {
        preguntasCorrectas++;
    }
}
