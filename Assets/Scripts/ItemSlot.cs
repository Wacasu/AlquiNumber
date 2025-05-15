using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public AudioClip destroySound;
    public GameObject destroyEffectPrefab;
    public bool conditionMet = false;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");

        if (eventData.pointerDrag != null)
        {
            GameObject droppedItem = eventData.pointerDrag;

            // Reproducir sonido
            if (destroySound != null)
                AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position);

            // Instanciar animación (efecto visual)
            if (destroyEffectPrefab != null)
                Instantiate(destroyEffectPrefab, droppedItem.transform.position, Quaternion.identity);

            // Desactivar objeto
            droppedItem.SetActive(false);

            // Activar condición lógica
            conditionMet = true;
        }
    }
}
