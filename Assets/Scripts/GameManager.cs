using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int vidas = 3;
    public float tiempoRestante = 60f;

    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI tiempoText;

    public GameObject pantallaGameOver;

    void Start()
    {
        ActualizarUI();
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            GameOver();
        }

        ActualizarUI();
    }

    void ActualizarUI()
    {
        vidasText.text = "Vidas: " + vidas;
        tiempoText.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante);
    }

    public void PerderVida()
    {
        vidas--;
        if (vidas <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        pantallaGameOver.SetActive(true);
        Time.timeScale = 0;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
