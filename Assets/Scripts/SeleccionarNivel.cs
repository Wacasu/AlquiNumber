using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionarNivel : MonoBehaviour
{
    public void CargarNivel(int numeroNivel)
    {
        string nombreEscenaNivel = $"Level{numeroNivel}";
        SceneManager.LoadScene(nombreEscenaNivel);
    }
} 