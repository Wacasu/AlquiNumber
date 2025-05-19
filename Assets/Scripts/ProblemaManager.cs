using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ProblemaManager : MonoBehaviour
{
    [Header("Texto en pantalla")]
    public TextMeshProUGUI textoProblema;
    
    [Header("CSV")]
    public TextAsset csvFile;

    [Header("Ingrediente Prefab")]
    public GameObject ingredientePrefab;

    [Header("Slots de Spawn")]
    public Transform[] spawnSlots;

    [Header("Sprites de Métodos")]
    [SerializeField] private List<MetodoSprite> metodoSprites = new List<MetodoSprite>();

    private List<string[]> csvData = new List<string[]>();
    private string[] metodos;

    void Start()
    {
        if (csvFile != null)
        {
            CargarCSV();
            MostrarProblemaYSpawn();
        }
        else
        {
            Debug.LogError("No hay archivo CSV asignado.");
        }
    }

    void CargarCSV()
    {
        csvData.Clear();
        StringReader reader = new StringReader(csvFile.text);
        bool firstLine = true;

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            if (firstLine)
            {
                metodos = values;
                // Crear entradas en metodoSprites para cada método si no existen
                foreach (string metodo in metodos)
                {
                    if (!metodoSprites.Exists(m => m.nombreMetodo == metodo))
                    {
                        metodoSprites.Add(new MetodoSprite { nombreMetodo = metodo, sprite = null });
                    }
                }
                firstLine = false;
            }
            else
            {
                csvData.Add(values);
            }
        }
    }

    // Método para asignar un sprite específico a un método
    public void AsignarSpriteAMetodo(string nombreMetodo, Sprite sprite)
    {
        var metodoSprite = metodoSprites.Find(m => m.nombreMetodo == nombreMetodo);
        if (metodoSprite != null)
        {
            metodoSprite.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"No se encontró el método {nombreMetodo} para asignar el sprite");
        }
    }

    // Método para obtener todos los métodos disponibles
    public string[] ObtenerMetodosDisponibles()
    {
        return metodos;
    }

    public void MostrarProblemaYSpawn()
    {
        if (csvData.Count == 0 || metodos.Length == 0)
        {
            Debug.LogError("CSV no cargado correctamente.");
            return;
        }

        int problemaIndex = Random.Range(0, csvData.Count);
        string[] filaProblema = csvData[problemaIndex];
        string problema = filaProblema[0];
        textoProblema.text = problema;

        List<int> validos = new List<int>();
        List<int> invalidos = new List<int>();

        for (int i = 1; i < filaProblema.Length; i++)
        {
            if (filaProblema[i] == "1")
                validos.Add(i);
            else if (filaProblema[i] == "0")
                invalidos.Add(i);
        }

        if (validos.Count == 0 || invalidos.Count < 3)
        {
            Debug.LogError("No hay suficientes métodos válidos o inválidos.");
            return;
        }

        int metodoValido = validos[Random.Range(0, validos.Count)];

        List<int> metodoInvalidos = new List<int>();
        while (metodoInvalidos.Count < 3)
        {
            int inval = invalidos[Random.Range(0, invalidos.Count)];
            if (!metodoInvalidos.Contains(inval))
                metodoInvalidos.Add(inval);
        }

        List<(int index, bool esCorrecto)> opciones = new List<(int, bool)>
        {
            (metodoValido, true)
        };

        foreach (int invalido in metodoInvalidos)
            opciones.Add((invalido, false));

        // Mezclar lista
        for (int i = 0; i < opciones.Count; i++)
        {
            int rnd = Random.Range(0, opciones.Count);
            var temp = opciones[rnd];
            opciones[rnd] = opciones[i];
            opciones[i] = temp;
        }

        // Instanciar ingredientes en los spawn slots
        for (int i = 0; i < spawnSlots.Length && i < opciones.Count; i++)
        {
            GameObject ing = Instantiate(ingredientePrefab, spawnSlots[i]);
            ing.transform.localPosition = Vector3.zero; // Asegura que aparezca centrado en el slot

            Ingrediente ingredienteScript = ing.GetComponent<Ingrediente>();
            if (ingredienteScript != null)
            {
                var (metodoIndex, esCorrecto) = opciones[i];
                string nombreMetodo = metodos[metodoIndex];
                
                // Buscar el sprite correspondiente al método
                Sprite spriteMetodo = null;
                var metodoSprite = metodoSprites.Find(m => m.nombreMetodo == nombreMetodo);
                if (metodoSprite != null)
                {
                    spriteMetodo = metodoSprite.sprite;
                }
                
                ingredienteScript.SetMetodo(nombreMetodo, esCorrecto, spriteMetodo);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene el script Ingrediente.");
            }
        }
    }
}