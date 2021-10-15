using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

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
        var currentInteractiveObject = gameManager.CurrentInteractiveObject.GetComponent<InteractiveEnvironment>();
        if (ItemReference.Name == currentInteractiveObject.RequiredItemName)
        {
            inventoryBehaviour.GetComponent<Animator>().Play("HideInventory");
            yield return new WaitForSeconds(0.75f);
            if (!currentInteractiveObject.HasCodingPuzzle)
            {
                var usageAnimation = currentInteractiveObject.GetComponent<PlayableDirector>();
                usageAnimation.Play();
                yield return new WaitForSeconds((float)usageAnimation.playableAsset.duration);
            }
            /*else
            {

            }*/
        }
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
