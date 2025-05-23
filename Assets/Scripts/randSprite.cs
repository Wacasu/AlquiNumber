using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class randSprite : MonoBehaviour

{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        float f = Random.Range(0, sprites.Length);
        image.sprite = sprites[(int)f];

        //GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
