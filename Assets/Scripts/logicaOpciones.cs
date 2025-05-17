using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class logicaOpciones : MonoBehaviour
{

    public controlOpciones panelOpciones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<controlOpciones>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mostrarOpciones();
        }
    }

    public void mostrarOpciones()
    {
        panelOpciones.pantallaOpciones.SetActive(true);
    }
}
