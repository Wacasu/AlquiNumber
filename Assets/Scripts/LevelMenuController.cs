using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
    public Button[] levelButtons; // Lista de botones de nivel
    public int maxLevelUnlocked = 3; // Número máximo de niveles desbloqueados

    void Start()
    {
        // Inicializar botones para que solo los niveles desbloqueados estén activos
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;  // Niveles numerados desde 1
            if (levelIndex <= maxLevelUnlocked)
            {
                levelButtons[i].interactable = true; // Habilitar el botón
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Asignar la acción de cargar la escena
            }
            else
            {
                levelButtons[i].interactable = false; // Deshabilitar el botón si el nivel no está desbloqueado
            }
        }
    }

    void LoadLevel(int levelIndex)
    {
        // Cargar la escena correspondiente al nivel
        SceneManager.LoadScene("Level" + levelIndex); // Asume que tienes escenas llamadas "Level1", "Level2", etc.
    }
}
