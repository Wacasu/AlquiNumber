using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemTooltipUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private string itemName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipPanel.SetActive(true);
        tooltipText.text = itemName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}
