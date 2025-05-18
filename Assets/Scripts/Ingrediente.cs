using UnityEngine;
using TMPro;

public class Ingrediente : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombreTexto;

    private string metodoNombre;
    private bool esCorrecto = false;

    /// <summary>
    /// Asigna el nombre del método y si es correcto.
    /// </summary>
    public void SetMetodo(string metodo, bool correcto)
    {
        metodoNombre = metodo;
        esCorrecto = correcto;

        if (nombreTexto != null)
            nombreTexto.text = metodoNombre;
        else
            Debug.LogWarning("No asignaste el TextMeshProUGUI en Ingrediente.");
    }

    public bool EsCorrecto()
    {
        return esCorrecto;
    }

    public string ObtenerMetodo()
    {
        return metodoNombre;
    }
}
