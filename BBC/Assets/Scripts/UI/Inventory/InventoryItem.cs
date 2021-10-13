using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Name;
    public string Description;
    public int Count;
    public Sprite Icon;
    public ItemType Type;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInParent<InventoryBehaviour>().ItemName.text = Name;
        GetComponentInParent<InventoryBehaviour>().ItemDescription.text = Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInParent<InventoryBehaviour>().ItemName.text = "";
        GetComponentInParent<InventoryBehaviour>().ItemDescription.text = "";
    }
}
