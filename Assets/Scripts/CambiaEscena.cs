using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiaASiguienteEscena : MonoBehaviour
{
    public void CargarSiguienteEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual + 1);
    }
}

