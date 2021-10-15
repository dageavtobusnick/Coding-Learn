using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Scripting,
    Other
}

public class InteractiveItem : MonoBehaviour
{
    [Header("Информация о предмете")]
    public string Name;
    public string Description;
    public int Count = 1;
    [Tooltip("Иконка для отображения в инвентаре")]
    public Sprite Icon;
    [Tooltip("Тип предмета: сюжетный или прочее")]
    public ItemType Type;

    private GameManager gameManager;

    private void CheckItemType()
    {
        List<InteractiveItem> itemsList;
        switch (Type)
        {
            case ItemType.Scripting:
                itemsList = gameManager.ScriptItems;
                break;
            default:
                itemsList = gameManager.OtherItems;
                break;
        }
        PutItemInInventory(itemsList);
    }

    private void PutItemInInventory(List<InteractiveItem> items)
    {
        var sameItem = items.Where(x => x.Name == Name);
        if (sameItem.Count() != 0)
            sameItem.First().Count += Count;
        else
        {
            items.Add(new InteractiveItem()
            {
                Name = Name,
                Description = Description,
                Count = Count,
                Icon = Icon,
                Type = Type
            });
        }    
        Destroy(gameObject.transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<InteractiveItemMarker>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
            CheckItemType();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
}
