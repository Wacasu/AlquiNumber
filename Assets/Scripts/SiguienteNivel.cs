using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteNivel : MonoBehaviour
{
    public void CargarSiguienteNivel()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        int numeroNivelActual = 0;

        // Extraer el número del nivel actual
        if (escenaActual.StartsWith("Level"))
        {
            string numeroStr = escenaActual.Substring("Level".Length);
            if (int.TryParse(numeroStr, out numeroNivelActual))
            {
                // Número de nivel obtenido con éxito
            }
            else
            {
                Debug.LogError($"No se pudo parsear el número del nivel de la escena: {escenaActual}");
                // Opcional: Cargar una escena de error o regresar al menú
                return;
            }
        }
        else
        {
            Debug.LogError($"La escena actual no sigue el formato 'LevelN': {escenaActual}");
            // Opcional: Cargar una escena de error o regresar al menú
            return;
        }

        // Determinar el siguiente nivel
        int siguienteNumeroNivel = numeroNivelActual + 1;
        string siguienteEscena;

        if (siguienteNumeroNivel <= 5) // Asumiendo que tienes 5 niveles (Level1 a Level5)
        {
            siguienteEscena = $"Level{siguienteNumeroNivel}";
            SceneManager.LoadScene(siguienteEscena);
        }
        else
        {
            // Si es el último nivel, regresar al menú inicial
            SceneManager.LoadScene("MenuInicial");
        }
    }
} 