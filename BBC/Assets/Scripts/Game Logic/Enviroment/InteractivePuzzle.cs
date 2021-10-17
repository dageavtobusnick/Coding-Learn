using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using Cinemachine;

public class InteractivePuzzle : MonoBehaviour
{
    public string RequiredItemName;
    public bool HasCodingPuzzle;
    [Space]
    public UnityEvent OnPuzzleSolved;

    private GameManager gameManager;
    private UIManager uiManager;
    private bool isPlayerClose = false;
    private bool isPuzzleStarted = false;

    public void GoToNextPuzzleStep()
    {
        if (!HasCodingPuzzle)
            StartCoroutine(FinishPuzzleByAnimation_COR());
        /*else
        {

        }*/
    }

    private IEnumerator FinishPuzzleByAnimation_COR()
    {
        var usageAnimation = GetComponent<PlayableDirector>();
        if (usageAnimation.playableAsset != null)
        {
            usageAnimation.Play();
            yield return new WaitForSeconds((float)usageAnimation.playableAsset.duration + 2);
        }
        OnPuzzleSolved.Invoke();
        FinishPuzzle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameManager.Player)
        {
            GetComponent<InteractiveItemMarker>().enabled = true;
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == gameManager.Player)
        {
            GetComponent<InteractiveItemMarker>().enabled = false;
            isPlayerClose = false;
        }
    }

    private void StartPuzzle()
    {
        uiManager.isExitToMenuAvailable = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<InteractiveItemMarker>().enabled = false;
        gameManager.Player.GetComponent<PlayerBehaviour>().FreezePlayer();
        gameManager.CurrentInteractiveObject = gameObject;
        GetComponentInChildren<CinemachineVirtualCamera>().enabled = true;
        uiManager.Canvas.GetComponentInChildren<InventoryBehaviour>().ShowInventory_SolvePuzzle();
    }

    private void FinishPuzzle()
    {        
        gameManager.Player.GetComponent<PlayerBehaviour>().UnfreezePlayer();
        uiManager.isExitToMenuAvailable = true;
        GetComponentInChildren<CinemachineVirtualCamera>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerClose && !isPuzzleStarted)
            StartPuzzle();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPuzzleStarted)
        {
            FinishPuzzle();
            uiManager.Canvas.GetComponentInChildren<InventoryBehaviour>().HideInventory_SolvePuzzle();
            GetComponent<SphereCollider>().enabled = true;
            GetComponent<InteractiveItemMarker>().enabled = true;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }
}
