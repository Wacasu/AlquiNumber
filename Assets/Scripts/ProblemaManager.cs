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
    public Transform[] spawnSlots;

    private List<string[]> csvData = new List<string[]>();
    private string[] metodos;

    


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
                metodos = values;
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

        // Instanciar ingredientes
        for (int i = 0; i < spawnSlots.Length; i++)
        {
            GameObject ing = Instantiate(ingredientePrefab, spawnSlots[i].position, Quaternion.identity);
            ing.transform.SetParent(spawnSlots[i], false);

            Ingrediente ingredienteScript = ing.GetComponent<Ingrediente>();
            if (ingredienteScript != null)
            {
                var (metodoIndex, esCorrecto) = opciones[i];
                string nombreMetodo = metodos[metodoIndex];
                ingredienteScript.SetMetodo(nombreMetodo, esCorrecto);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene el script Ingrediente.");
            }

            Debug.Log("Item " + i + "XD");
        }
    }
}
