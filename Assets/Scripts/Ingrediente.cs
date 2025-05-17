using UnityEngine;
using TMPro;  // Si usas TextMeshPro para texto

public class Ingrediente : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreTexto;  // Asignar en prefab
    private string metodoNombre;

    public void SetMetodo(string metodo)
    {
        metodoNombre = metodo;
        if (nombreTexto != null)
            nombreTexto.text = metodoNombre;
        else
            Debug.LogWarning("No asignaste el TextMeshProUGUI en Ingrediente.");
    }
}
