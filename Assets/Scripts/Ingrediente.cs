using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ingrediente : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI nombreTexto;
    [SerializeField] private Image iconoImagen;

    [Header("Datos del Método")]
    [SerializeField] private string descripcionLarga;

    private string metodoNombre;
    private bool esCorrecto;

    private void Awake()
    {
        if (nombreTexto == null)
            Debug.LogWarning($"{gameObject.name} → No se asignó 'nombreTexto' (TextMeshProUGUI)");

        if (iconoImagen == null)
            Debug.LogWarning($"{gameObject.name} → No se asignó 'iconoImagen' (Image)");
    }

    public void SetMetodo(string metodo, bool correcto, Sprite icono = null, string descripcion = "")
    {
        metodoNombre = metodo;
        esCorrecto = correcto;
        descripcionLarga = descripcion;

        if (nombreTexto != null)
            nombreTexto.text = metodoNombre;

        if (iconoImagen != null)
        {
            iconoImagen.sprite = icono;
            iconoImagen.enabled = icono != null;
        }

        AplicarEstiloResaltado();
    }

    public void AsignarCorrectitud(bool correcto) => esCorrecto = correcto;

    public void SetDescripcion(string descripcion) => descripcionLarga = descripcion;

    public string ObtenerDescripcion() => descripcionLarga;

    public bool EsCorrecto() => esCorrecto;

    public string ObtenerMetodo() => metodoNombre;

    private void AplicarEstiloResaltado()
    {
        if (iconoImagen != null)
        {
            iconoImagen.color = new Color(1f, 1f, 0.85f); // Amarillo claro (ligeramente más sutil)
        }
    }
}
