using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class keepConfig : MonoBehaviour 
{
    private void Awake()
    {
        var noDestruirEntreEscenas = FindObjectsByType<keepConfig>(FindObjectsSortMode.None);

        if (noDestruirEntreEscenas.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
