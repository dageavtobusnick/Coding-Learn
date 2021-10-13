using System.Collections;
using System.Collections.Generic;
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
    public Sprite Icon;
    public ItemType Type;

    private GameManager gameManager;

    private void PutItemInInventory()
    {
        gameManager.ScriptItems.Add(new InteractiveItem()
        {
            Name = Name,
            Description = Description,
            Icon = Icon,
            Type = Type
        });
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GetComponent<InteractiveItemMarker>().enabled)
            PutItemInInventory();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
}
