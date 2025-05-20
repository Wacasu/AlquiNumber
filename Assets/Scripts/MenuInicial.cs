using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        Application.Quit();
    }
    public void Atras()
    {
        Destroy(GameObject.FindWithTag("opciones"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void AtrasMenu()
    {
        Destroy(GameObject.FindWithTag("opciones"));
        SceneManager.LoadScene("MenuInicial");
    }
}
