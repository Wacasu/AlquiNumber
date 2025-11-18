using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Ingrediente : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private TextMeshProUGUI nombreTexto;
    [SerializeField] private Image iconoImagen;

    [Header("Datos de la Respuesta")]
    [SerializeField] private string descripcionLarga;

    private string letraRespuesta; // A, B, C, o D
    private string textoRespuesta; // El texto de la respuesta
    private bool esCorrecto;

    // Mantener compatibilidad con el sistema anterior
    private string metodoNombre; // Deprecated, mantener para compatibilidad

    private void Awake()
    {
        if (nombreTexto == null)
            Debug.LogWarning($"{gameObject.name} → No se asignó 'nombreTexto' (TextMeshProUGUI)");

        if (iconoImagen == null)
            Debug.LogWarning($"{gameObject.name} → No se asignó 'iconoImagen' (Image)");
    }

    // Nuevo método para establecer respuestas
    public void SetRespuesta(string letra, string texto, bool correcto, string textoMostrar = null)
    {
        letraRespuesta = letra;
        textoRespuesta = texto;
        esCorrecto = correcto;

        if (nombreTexto != null)
        {
            // Si se proporciona textoMostrar, usarlo; si no, usar "A: texto"
            nombreTexto.text = textoMostrar ?? $"{letra}: {texto}";
        }

        if (iconoImagen != null)
        {
            iconoImagen.enabled = false; // Ocultar icono por defecto
        }

        AplicarEstiloResaltado();
    }

    // Método antiguo mantenido para compatibilidad (pero ahora usa el nuevo sistema)
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

    // Nuevos métodos para obtener datos de la respuesta
    public string ObtenerLetraRespuesta() => letraRespuesta;
    public string ObtenerTextoRespuesta() => textoRespuesta;

    // Método antiguo mantenido para compatibilidad
    public string ObtenerMetodo() => metodoNombre ?? letraRespuesta;

    private void AplicarEstiloResaltado()
    {
        if (iconoImagen != null && iconoImagen.enabled)
        {
            iconoImagen.color = new Color(1f, 1f, 0.85f); // Amarillo claro (ligeramente más sutil)
        }
    }
}
