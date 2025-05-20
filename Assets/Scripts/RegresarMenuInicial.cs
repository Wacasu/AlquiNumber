using UnityEngine;
using UnityEngine.SceneManagement;

public class RegresarMenuInicial : MonoBehaviour
{
    public void CargarMenuInicial()
    {
        SceneManager.LoadScene("MenuInicial");
    }
} 