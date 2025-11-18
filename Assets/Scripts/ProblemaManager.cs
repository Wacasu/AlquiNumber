using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ProblemaManager : MonoBehaviour
{
    [Header("Texto en pantalla")]
    public TextMeshProUGUI textoProblema;
    public TextMeshProUGUI textoTema;
    
    [Header("CSV")]
    public TextAsset csvFile;

    [Header("Ingrediente Prefab")]
    public GameObject ingredientePrefab;

    [Header("Slots de Spawn")]
    public Transform[] spawnSlots;

    // Estructura del CSV: Problema, Tema, A, B, C, D, Respuesta correcta
    private List<string[]> csvData = new List<string[]>();
    private string[] problemaActual; // Almacena el problema actual siendo mostrado

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
            // Manejar casos donde las comas pueden estar dentro de campos con comillas
            string[] values = ParseCSVLine(line);

            if (firstLine)
            {
                // Ignorar la línea de encabezados
                firstLine = false;
            }
            else
            {
                // Verificar que la fila tenga al menos 7 columnas (Problema, Tema, A, B, C, D, Respuesta correcta)
                if (values.Length >= 7)
                {
                    csvData.Add(values);
                }
                else
                {
                    Debug.LogWarning($"Fila con formato incorrecto ignorada: {line}");
                }
            }
        }
    }

    // Método auxiliar para parsear líneas CSV considerando comillas
    private string[] ParseCSVLine(string line)
    {
        List<string> result = new List<string>();
        bool dentroDeComillas = false;
        string campoActual = "";

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '"')
            {
                dentroDeComillas = !dentroDeComillas;
            }
            else if (c == ',' && !dentroDeComillas)
            {
                result.Add(campoActual.Trim());
                campoActual = "";
            }
            else
            {
                campoActual += c;
            }
        }
        result.Add(campoActual.Trim()); // Agregar el último campo

        return result.ToArray();
    }

    public void MostrarProblemaYSpawn()
    {
        if (csvData.Count == 0)
        {
            Debug.LogError("CSV no cargado correctamente o está vacío.");
            return;
        }

        // Seleccionar un problema aleatorio
        int problemaIndex = Random.Range(0, csvData.Count);
        problemaActual = csvData[problemaIndex];

        // Verificar que el problema tenga el formato correcto
        if (problemaActual.Length < 7)
        {
            Debug.LogError($"Problema con formato incorrecto: {string.Join(",", problemaActual)}");
            return;
        }

        // Columna 0: Problema, Columna 1: Tema, Columna 2-5: A-D, Columna 6: Respuesta correcta
        string problema = problemaActual[0];
        string tema = problemaActual[1];
        string respuestaA = problemaActual[2];
        string respuestaB = problemaActual[3];
        string respuestaC = problemaActual[4];
        string respuestaD = problemaActual[5];
        string respuestaCorrecta = problemaActual[6].Trim().ToUpper(); // A, B, C, o D

        // Mostrar el problema en pantalla
        textoProblema.text = problema;
        
        // Mostrar el tema del problema en pantalla
        if (textoTema != null)
        {
            textoTema.text = $"Tema: {tema}";
        }

        // Crear lista de opciones con su letra y texto
        List<(string letra, string texto, bool esCorrecto)> opciones = new List<(string, string, bool)>
        {
            ("A", respuestaA, respuestaCorrecta == "A"),
            ("B", respuestaB, respuestaCorrecta == "B"),
            ("C", respuestaC, respuestaCorrecta == "C"),
            ("D", respuestaD, respuestaCorrecta == "D")
        };

        // Mezclar las opciones para que no siempre aparezcan en el mismo orden
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
            ing.transform.localPosition = Vector3.zero;

            Ingrediente ingredienteScript = ing.GetComponent<Ingrediente>();
            if (ingredienteScript != null)
            {
                var (letra, texto, esCorrecto) = opciones[i];
                // Mostrar la letra y el texto de la respuesta (ej: "A: 15")
                string textoMostrar = $"{letra}: {texto}";
                ingredienteScript.SetRespuesta(letra, texto, esCorrecto, textoMostrar);
            }
            else
            {
                Debug.LogWarning("El prefab no tiene el script Ingrediente.");
            }
        }
    }

    // Método para obtener el problema actual (útil para debugging)
    public string[] ObtenerProblemaActual()
    {
        return problemaActual;
    }
}