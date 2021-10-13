using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public ItemType Type;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<InventoryBehaviour>().ItemDescription.text = Name + "\n" + Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<InventoryBehaviour>().ItemDescription.text = "";
    }
}
