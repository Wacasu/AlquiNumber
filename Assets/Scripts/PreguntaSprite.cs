using UnityEngine;

[CreateAssetMenu(fileName = "PreguntaNueva", menuName = "Trivia/Pregunta Sprite")]
public class PreguntaSprite : ScriptableObject
{
    public Sprite spritePregunta;
    public Sprite[] spritesOpciones = new Sprite[4];
    [Range(0, 3)]
    public int indiceRespuestaCorrecta;
}
