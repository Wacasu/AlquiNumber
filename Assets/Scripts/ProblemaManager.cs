using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
public class ProblemaManager : MonoBehaviour
{

    [Header("Texto en pantalla")]
    public TextMeshProUGUI textoProblema;

    [Header("CSV")]
    public TextAsset csvFile;

    [Header("Ingrediente Prefab")]
    public GameObject ingredientePrefab;

    [Header("Slots de Spawn")]
    public Transform[] spawnSlots; // 4 slots vacíos en la escena para poner ingredientes

    private List<string[]> csvData = new List<string[]>();
    private string[] metodos; // nombres de métodos desde la primera fila

    


    void Start()
    {
        CargarCSV();
        MostrarProblemaYSpawn();

    }

    void CargarCSV()
    {
        StringReader reader = new StringReader(csvFile.text);

        bool firstLine = true;
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            if (firstLine)
            {
                metodos = values; // guardamos la primera fila (nombres métodos)
                firstLine = false;
            }
            else
            {
                csvData.Add(values);
            }
        }
    }

    void MostrarProblemaYSpawn()
    {
        if (csvData.Count == 0 || metodos.Length == 0)
        {
            Debug.LogError("CSV no cargado correctamente.");
            return;
        }

        // Elegir problema aleatorio (fila)
        int problemaIndex = Random.Range(0, csvData.Count);
        string[] filaProblema = csvData[problemaIndex];

        // Mostrar problema (primera columna)
        string problema = filaProblema[0];
        textoProblema.text = problema;


        // Encontrar métodos con 1 (válidos) y con 0 (inválidos)
        List<int> validos = new List<int>();
        List<int> invalidos = new List<int>();

        // Empieza en 1 porque la columna 0 es el problema
        for (int i = 1; i < filaProblema.Length; i++)
        {
            if (filaProblema[i] == "1")
                validos.Add(i);
            else if (filaProblema[i] == "0")
                invalidos.Add(i);
        }

        if (validos.Count == 0 || invalidos.Count < 3)
        {
            Debug.LogError("No hay suficientes métodos válidos o inválidos para este problema.");
            return;
        }

        // Elegir 1 método válido al azar
        int metodoValido = validos[Random.Range(0, validos.Count)];

        // Elegir 3 métodos inválidos al azar
        List<int> metodoInvalidos = new List<int>();
        while (metodoInvalidos.Count < 3)
        {
            int inval = invalidos[Random.Range(0, invalidos.Count)];
            if (!metodoInvalidos.Contains(inval))
                metodoInvalidos.Add(inval);
        }

        // Crear lista de opciones (1 válido + 3 inválidos)
        List<int> opciones = new List<int>(metodoInvalidos);
        opciones.Add(metodoValido);

        // Mezclar lista para que el método válido esté en posición aleatoria
        for (int i = 0; i < opciones.Count; i++)
        {
            int rnd = Random.Range(0, opciones.Count);
            int temp = opciones[rnd];
            opciones[rnd] = opciones[i];
            opciones[i] = temp;
        }

        // Instanciar ingredientes en slots con el nombre del método
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            GameObject ing = Instantiate(ingredientePrefab, spawnSlots[i].position, Quaternion.identity);
            ing.transform.SetParent(spawnSlots[i]);

            Ingrediente ingredienteScript = ing.GetComponent<Ingrediente>();
            if (ingredienteScript != null)
            {
                int metodoIndex = opciones[i];
                string nombreMetodo = metodos[metodoIndex];
                ingredienteScript.SetMetodo(nombreMetodo);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene el script Ingrediente.");
            }

            Debug.Log("Item " + i + "XD");
        }
    }
}
