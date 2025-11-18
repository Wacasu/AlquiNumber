using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Controlador de cada pieza individual del puzzle.
/// Maneja la detección de encaje y la posición correcta.
/// Adaptado del proyecto Puzzle original.
/// </summary>
public class PiezaPuzzle : MonoBehaviour
{
    private Vector3 posicionCorrecta;
    public bool Encajada { get; private set; }
    public bool Seleccionada { get; set; }

    [Header("Configuración")]
    [Tooltip("Distancia mínima para considerar que la pieza está en su lugar correcto")]
    [SerializeField] private float distanciaEncaje = 0.5f;
    
    [Tooltip("Rango aleatorio para la posición inicial X")]
    [SerializeField] private Vector2 rangoX = new Vector2(5f, 11f);
    
    [Tooltip("Rango aleatorio para la posición inicial Y")]
    [SerializeField] private Vector2 rangoY = new Vector2(2.5f, -7f);

    private PuzzleNivel puzzleNivel;
    private SortingGroup sortingGroup;

    void Start()
    {
        // Guardar la posición correcta (posición inicial)
        posicionCorrecta = transform.position;
        
        // Posicionar aleatoriamente al inicio
        transform.position = new Vector3(
            Random.Range(rangoX.x, rangoX.y), 
            Random.Range(rangoY.x, rangoY.y), 
            0
        );

        // Obtener referencia al PuzzleNivel
        puzzleNivel = Camera.main?.GetComponent<PuzzleNivel>();
        if (puzzleNivel == null)
        {
            puzzleNivel = FindObjectOfType<PuzzleNivel>();
        }

        // Obtener o agregar SortingGroup
        sortingGroup = GetComponent<SortingGroup>();
        if (sortingGroup == null)
        {
            sortingGroup = gameObject.AddComponent<SortingGroup>();
        }

        // Inicializar estado
        Encajada = false;
        Seleccionada = false;
    }

    void Update()
    {
        // Verificar si la pieza está cerca de su posición correcta
        if (Vector3.Distance(transform.position, posicionCorrecta) < distanciaEncaje)
        {
            // Solo encajar si no está seleccionada y aún no está encajada
            if (!Seleccionada && !Encajada)
            {
                // Encajar la pieza
                transform.position = posicionCorrecta;
                Encajada = true;
                
                // Restablecer el orden de renderizado
                if (sortingGroup != null)
                {
                    sortingGroup.sortingOrder = 0;
                }

                // Notificar al PuzzleNivel
                if (puzzleNivel != null)
                {
                    puzzleNivel.IncrementarPiezasEncajadas();
                }
            }
        }
    }

    /// <summary>
    /// Reinicia la pieza a su estado inicial (sin encajar y en posición aleatoria)
    /// </summary>
    public void ReiniciarPieza()
    {
        Encajada = false;
        Seleccionada = false;
        
        // Posicionar aleatoriamente
        transform.position = new Vector3(
            Random.Range(rangoX.x, rangoX.y), 
            Random.Range(rangoY.x, rangoY.y), 
            0
        );

        // Restablecer orden de renderizado
        if (sortingGroup != null)
        {
            sortingGroup.sortingOrder = 0;
        }
    }

    /// <summary>
    /// Obtiene la posición correcta de la pieza
    /// </summary>
    public Vector3 ObtenerPosicionCorrecta()
    {
        return posicionCorrecta;
    }
}

