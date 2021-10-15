using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public InteractiveItem ItemReference;

    private InventoryBehaviour inventoryBehaviour;
    private GameManager gameManager;

    public void ChooseAction()
    {
        var inventoryStatement = inventoryBehaviour.InventoryStatement;
        switch (inventoryStatement)
        {
            case InventoryStatement.PuzzleSolving:
                StartCoroutine(TrySolvePuzzle());
                break;
        }
    }

    private IEnumerator TrySolvePuzzle()
    {
        var currentInteractiveEnvironment = gameManager.CurrentInteractiveEnvironment.GetComponent<InteractiveEnvironment>();
        if (ItemReference.Name == currentInteractiveEnvironment.RequiredItemName)
        {

        }
        yield break;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryBehaviour.ItemName.text = ItemReference.Name;
        inventoryBehaviour.ItemDescription.text = ItemReference.Description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryBehaviour.ItemName.text = "";
        inventoryBehaviour.ItemDescription.text = "";
    }

    private void OnEnable()
    {
        inventoryBehaviour = GetComponentInParent<InventoryBehaviour>();
        gameManager = GameManager.Instance;
    }
}
