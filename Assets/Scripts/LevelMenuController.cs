using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public Button[] levelButtons; // Lista de botones de nivel
    public int maxLevelUnlocked = 3; // N�mero m�ximo de niveles desbloqueados

    void Start()
    {
        // Inicializar botones para que solo los niveles desbloqueados est�n activos
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;  // Niveles numerados desde 1
            if (levelIndex <= maxLevelUnlocked)
            {
                levelButtons[i].interactable = true; // Habilitar el bot�n
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Asignar la acci�n de cargar la escena
            }
            else
            {
                levelButtons[i].interactable = false; // Deshabilitar el bot�n si el nivel no est� desbloqueado
            }
        }
    }

    void LoadLevel(int levelIndex)
    {
        // Cargar la escena correspondiente al nivel
        SceneManager.LoadScene("Level" + levelIndex); // Asume que tienes escenas llamadas "Level1", "Level2", etc.
    }
}
