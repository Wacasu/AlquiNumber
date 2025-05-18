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
    public TextMeshProUGUI vidasTexto;
    public TextMeshProUGUI temporizadorTexto;

    [Header("Configuración del juego")]
    public int vidasIniciales = 3;
    public float tiempoPorProblema = 30f; // segundos

    private int vidasActuales;
    private float tiempoRestante;
    private bool problemaActivo = true;

    void Start()
    {
        vidasActuales = vidasIniciales;
        ActualizarVidasUI();
        feedbackTexto.text = "";
        pantallaGameOver.SetActive(false);

        tiempoRestante = tiempoPorProblema;
        problemaActivo = true;
    }

    void Update()
    {
        if (!problemaActivo || vidasActuales <= 0) return;

        tiempoRestante -= Time.deltaTime;
        tiempoRestante = Mathf.Max(0, tiempoRestante);

        ActualizarTemporizadorUI();

        if (tiempoRestante <= 0f)
        {
            TiempoAgotado();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!problemaActivo) return;

        GameObject dragged = eventData.pointerDrag;
        if (dragged == null) return;

        Ingrediente ingrediente = dragged.GetComponent<Ingrediente>();

        if (ingrediente != null)
        {
            problemaActivo = false;

            if (ingrediente.EsCorrecto())
            {
                feedbackTexto.text = "¡Correcto!";
                feedbackTexto.color = Color.green;

                Invoke(nameof(RecargarEscena), 1.2f);
            }
            else
            {
                vidasActuales--;
                ActualizarVidasUI();

                feedbackTexto.text = "Incorrecto. Intenta otra vez.";
                feedbackTexto.color = Color.red;

                if (vidasActuales <= 0)
                {
                    GameOver();
                }
                else
                {
                    // Reiniciar temporizador para siguiente intento
                    ReiniciarTemporizador();
                    problemaActivo = true;
                }
            }

            Destroy(dragged);
        }
    }

    void TiempoAgotado()
    {
        vidasActuales--;
        ActualizarVidasUI();
        feedbackTexto.text = "¡Tiempo agotado!";
        feedbackTexto.color = Color.red;

        if (vidasActuales <= 0)
        {
            problemaActivo = false;
            GameOver();
        }
        else
        {
            ReiniciarTemporizador();
        }
    }

    void ActualizarVidasUI()
    {
        if (vidasTexto != null)
            vidasTexto.text = $"Vidas: {vidasActuales}";
    }

    void ActualizarTemporizadorUI()
    {
        if (temporizadorTexto != null)
            temporizadorTexto.text = $"Tiempo: {tiempoRestante:F1}s";
    }

    void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoPorProblema;
        problemaActivo = true;
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
    }
}
