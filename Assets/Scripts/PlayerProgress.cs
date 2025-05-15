using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance;

    public int nivelMaxDesbloqueado = 1;
    public int experiencia = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CargarProgreso();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GanarExperiencia(int cantidad)
    {
        experiencia += cantidad;
        GuardarProgreso();
    }

    public void DesbloquearNivel(int nivel)
    {
        if (nivel > nivelMaxDesbloqueado)
        {
            nivelMaxDesbloqueado = nivel;
            GuardarProgreso();
        }
    }

    void GuardarProgreso()
    {
        PlayerPrefs.SetInt("NivelMax", nivelMaxDesbloqueado);
        PlayerPrefs.SetInt("XP", experiencia);
    }

    void CargarProgreso()
    {
        nivelMaxDesbloqueado = PlayerPrefs.GetInt("NivelMax", 1);
        experiencia = PlayerPrefs.GetInt("XP", 0);
    }
}
