using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum InventoryStatement
{
    Normal,
    PuzzleSolving
}

public class InventoryBehaviour : MonoBehaviour
{
    public GameObject Inventory;
    public Text ItemName;
    public Text ItemDescription;

    [HideInInspector] public InventoryStatement InventoryStatement = InventoryStatement.Normal;

    [SerializeField] private GameObject scriptInventoryItems;
    [SerializeField] private GameObject otherInventoryItems;
    [SerializeField] private GameObject inventoryItemPrefab;

    private GameManager gameManager;
    private bool isOpen;

    public void ShowInventory_SolvePuzzle()
    {
        ShowInventory();
        InventoryStatement = InventoryStatement.PuzzleSolving;
    }

    public void HideInventory_SolvePuzzle()
    {
        HideInventory();
        InventoryStatement = InventoryStatement.Normal;
    }

    public void ShowScriptingItems()
    {
        scriptInventoryItems.SetActive(true);
        otherInventoryItems.SetActive(false);
    }

    public void ShowOtherItems()
    {
        scriptInventoryItems.SetActive(false);
        otherInventoryItems.SetActive(true);
    }

    private void ShowInventory()
    {
        gameManager.Player.GetComponent<PlayerBehaviour>().FreezePlayer();
        UpdateInventory();
        otherInventoryItems.SetActive(false);
        Inventory.GetComponent<Animator>().Play("ShowInventory");
    }

    private void HideInventory()
    {
        ClearInventory();
        Inventory.GetComponent<Animator>().Play("HideInventory");
        gameManager.Player.GetComponent<PlayerBehaviour>().UnfreezePlayer();
    }

    private void UpdateInventory()
    {
        ClearInventory();
        FillInventory(gameManager.ScriptItems, scriptInventoryItems);
        FillInventory(gameManager.OtherItems, otherInventoryItems);
    }

    private void FillInventory(List<InteractiveItem> items, GameObject itemsContainer)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (items[i].Count <= 0)
            {
                items.RemoveAt(i);
                continue;
            }
            var newItem = Instantiate(inventoryItemPrefab, itemsContainer.transform.GetChild(0).GetChild(0));
            var itemComponent = newItem.GetComponent<InventoryItem>();
            itemComponent.ItemReference = items[i];
            newItem.transform.GetChild(0).GetComponent<Image>().sprite = itemComponent.ItemReference.Icon;
            newItem.transform.GetChild(1).GetComponent<Text>().text = itemComponent.ItemReference.Count > 1 ? itemComponent.ItemReference.Count.ToString() : "";
        }
    }

    private void ClearInventory()
    {
        var scriptItemsContainer = scriptInventoryItems.transform.GetChild(0).GetChild(0);
        for (var i = scriptItemsContainer.childCount - 1; i >= 0; i--)
            Destroy(scriptItemsContainer.GetChild(i).gameObject);
        var otherItemsContainer = otherInventoryItems.transform.GetChild(0).GetChild(0);
        for (var i = otherItemsContainer.childCount - 1; i >= 0; i--)
            Destroy(otherItemsContainer.GetChild(i).gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && InventoryStatement == InventoryStatement.Normal)
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
