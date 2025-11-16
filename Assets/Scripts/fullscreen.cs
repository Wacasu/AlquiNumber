using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class fullscreen : MonoBehaviour {
    public Toggle toggle;

    public TMP_Dropdown dropdownRes;
    Resolution[] resoluciones;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    private void Update()
    {

    }

    public void ActivaFullscreen(bool fullscreen) {
        Screen.fullScreen = fullscreen;
    }


    public void RevisarResolucion() { 
        resoluciones = Screen.resolutions;
        dropdownRes.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {

                resolucionActual = i;
            }
        }
            dropdownRes.AddOptions(opciones);
            dropdownRes.value = resolucionActual;
            dropdownRes.RefreshShownValue();

        
    } 

    public void CambiarRes(int indexRes)
    {
        Resolution resolucion = resoluciones[indexRes];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
    
}
