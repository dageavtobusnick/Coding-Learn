using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryBehaviour : MonoBehaviour
{
    public GameObject Inventory;
    public Text ItemDescription;

    [SerializeField] private GameObject scriptInventoryItems;
    [SerializeField] private GameObject otherInventoryItems;
    [SerializeField] private GameObject inventoryItemPrefab;

    private GameManager gameManager;
    private bool isOpen;

    private void ShowInventory()
    {

        for (var i = 0; i < gameManager.ScriptItems.Count; i++)
        {
            var newItem = Instantiate(inventoryItemPrefab, scriptInventoryItems.transform.GetChild(0).GetChild(0));
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = gameManager.ScriptItems[i].Icon;
            newItem.GetComponent<InventoryItem>().Name = gameManager.ScriptItems[i].Name;
            newItem.GetComponent<InventoryItem>().Description = gameManager.ScriptItems[i].Description;
            newItem.GetComponent<InventoryItem>().Icon = gameManager.ScriptItems[i].Icon;
            newItem.GetComponent<InventoryItem>().Type = gameManager.ScriptItems[i].Type;
        }
        /*for (var i = 0; i < gameManager.OtherItems.Count; i++)
        {
            var newItem = Instantiate(inventoryItemPrefab, otherInventoryItems.transform.GetChild(0).GetChild(0));
            newItem.GetComponentInChildren<Text>().text = gameManager.OtherItems[i].Name;
            newItem.transform.GetChild(1).GetComponent<Image>().sprite = gameManager.OtherItems[i].Icon;
        }*/
        Inventory.GetComponent<Animator>().Play("ShowInventory");
    }

    private void HideInventory()
    {
        var scriptItemsContainer = scriptInventoryItems.transform.GetChild(0).GetChild(0);
        for (var i = scriptItemsContainer.childCount - 1; i >= 0; i--)
            Destroy(scriptItemsContainer.GetChild(i).gameObject);
        Inventory.GetComponent<Animator>().Play("HideInventory");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isOpen)
                ShowInventory();
            else HideInventory();
            isOpen = !isOpen;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
    }
}
