using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractiveEnvironmentLookAt : MonoBehaviour
{
    [TextArea]
    [SerializeField] private string description;
    [SerializeField] private UnityEvent onPlayerLookedAt;

    private GameManager gameManager;
    private UIManager uiManager;
    private bool isPlayerClose;

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

    private void Update()
    {
        if (isPlayerClose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponentInChildren<Camera>().enabled = true;
                gameManager.Player.GetComponent<PlayerBehaviour>().FreezePlayer();
                uiManager.isExitToMenuAvailable = false;
                GetComponent<InteractiveItemMarker>().enabled = false;
                GetComponentInChildren<Text>().text = description;

            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                GetComponentInChildren<Camera>().enabled = false;
                gameManager.Player.GetComponent<PlayerBehaviour>().UnfreezePlayer();
                StartCoroutine(uiManager.MakeExitToMenuAvailable_COR());
                GetComponent<InteractiveItemMarker>().enabled = true;
                onPlayerLookedAt.Invoke();
                onPlayerLookedAt.RemoveAllListeners();
            }
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }
}
