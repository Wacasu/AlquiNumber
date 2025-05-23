using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FloatingItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public float floatAmplitude = 8f;  // Menor amplitud para oscilación sutil
    public float floatFrequency = 1.5f;

    public GameObject auraObject;

    private Vector3 startPos;
    private bool isBeingDragged = false;

    void Start()
    {
        startPos = transform.localPosition;

        //if (auraObject == null)
        //{
        //    CreateAura();
        //}
    }

    void Update()
    {
        if (isBeingDragged) return;

        // Movimiento de oscilación vertical
        float newY = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.localPosition = startPos + new Vector3(0f, newY, 0f);
    }

    //void CreateAura()
    //{
    //    auraObject = new GameObject("Aura");
    //    auraObject.transform.SetParent(transform, false);
    //    auraObject.transform.SetAsFirstSibling(); // Detrás

    //    Image auraImage = auraObject.AddComponent<Image>();
    //    auraImage.color = new Color(0.5f, 0.8f, 1f, 0.3f); // Celeste más tenue

    //    Image thisImage = GetComponent<Image>();
    //    if (thisImage != null)
    //    {
    //        auraImage.sprite = thisImage.sprite;
    //        auraImage.rectTransform.sizeDelta = thisImage.rectTransform.sizeDelta * 1.1f; // Más delgado
    //    }
    //}

    public void OnBeginDrag(PointerEventData eventData)
    {
        isBeingDragged = true;
        if (auraObject != null)
            auraObject.SetActive(false); // Oculta aura al arrastrar
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isBeingDragged = false;
        if (auraObject != null)
            auraObject.SetActive(true); // Muestra aura al soltar
    }
}
