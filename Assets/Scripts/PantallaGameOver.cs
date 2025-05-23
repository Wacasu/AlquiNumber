using UnityEngine;
using TMPro;

public class PantallaGameOver : MonoBehaviour
{
    public TextMeshProUGUI mensajeText;

    void Start()
    {
        mensajeText.text = "¡Juego terminado!";
    }
}
