using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ProblemaManager : MonoBehaviour
{
    public TextAsset archivoCSV;

    void Start()
    {
        MostrarProblema();
    }

    void MostrarProblema()
    {
        if (archivoCSV == null)
        {
            Debug.LogError("❌ No se asignó el archivo CSV.");
            return;
        }

        string[] lineas = archivoCSV.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (lineas.Length < 2)
        {
            Debug.LogError("❌ El archivo CSV no tiene suficientes datos.");
            return;
        }

        // Leer encabezados (métodos)
        string[] encabezados = lineas[0].Split(',').Skip(1).ToArray();

        // Guardar filas válidas (mismo número de columnas que encabezado + 1)
        List<string[]> filasValidas = new();
        for (int i = 1; i < lineas.Length; i++)
        {
            string[] columnas = lineas[i].Split(',');

            if (columnas.Length == encabezados.Length + 1)
                filasValidas.Add(columnas);
        }

        if (filasValidas.Count == 0)
        {
            Debug.LogError("❌ No hay filas válidas en el CSV.");
            return;
        }

        // Elegir una fila al azar (problema)
        string[] fila = filasValidas[Random.Range(0, filasValidas.Count)];
        string problemaTexto = fila[0];

        // Crear listas de métodos correctos (1) e incorrectos (0)
        List<string> metodosCorrectos = new();
        List<string> metodosIncorrectos = new();

        for (int i = 1; i < fila.Length; i++)
        {
            if (fila[i].Trim() == "1")
                metodosCorrectos.Add(encabezados[i - 1]);
            else if (fila[i].Trim() == "0")
                metodosIncorrectos.Add(encabezados[i - 1]);
        }

        if (metodosCorrectos.Count == 0 || metodosIncorrectos.Count < 3)
        {
            Debug.LogWarning("⚠️ No hay suficientes métodos correctos o incorrectos en esta fila.");
            return;
        }

        // Escoger 1 correcto y 3 incorrectos al azar
        string metodoCorrecto = metodosCorrectos[Random.Range(0, metodosCorrectos.Count)];
        List<string> opciones = new List<string> { metodoCorrecto };
        opciones.AddRange(metodosIncorrectos.OrderBy(x => Random.value).Take(3));

        // Mezclar opciones
        opciones = opciones.OrderBy(x => Random.value).ToList();

        // Mostrar
        Debug.Log("🧠 Problema: " + problemaTexto);
        for (int i = 0; i < opciones.Count; i++)
        {
            Debug.Log($"🔹 Opción {i + 1}: {opciones[i]}");
        }
        Debug.Log("✅ Respuesta correcta: " + metodoCorrecto);
    }
}
