﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuPanelBehaviour : MonoBehaviour
{
    [Header("Панель выхода в меню")]
    public GameObject ExitToMenuPanel;
    public GameObject BlackScreen;

    private GameObject blackScreenContent;
    private PlayerBehaviour playerBehaviour;
    private bool isPressed = false;

    public void ReturnToGame() => StartCoroutine(ReturnToGame_COR());

    public void ExitToMenu() => StartCoroutine(ExitToMenu_COR());

    private IEnumerator ReturnToGame_COR()
    {
        ExitToMenuPanel.GetComponent<Animator>().Play("ScaleExitToMenuPanelDown");
        yield return new WaitForSeconds(0.75f);
        isPressed = false;
        playerBehaviour.UnfreezePlayer();
    }

    private IEnumerator ExitToMenu_COR()
    {
        SaveManager.DeleteSavedDialogueData();
        ExitToMenuPanel.GetComponent<Animator>().Play("ScaleExitToMenuPanelDown");
        yield return new WaitForSeconds(0.75f);
        BlackScreen.transform.localScale = new Vector3(1, 1, 1);
        blackScreenContent.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isPressed && UIManager.Instance.isExitToMenuAvailable)
        {
            ExitToMenuPanel.GetComponent<Animator>().Play("ScaleExitToMenuPanelUp");
            isPressed = true;
            playerBehaviour.FreezePlayer();
        }
    }

    private void Start()
    {
        blackScreenContent = BlackScreen.transform.GetChild(0).gameObject;
        playerBehaviour = GameManager.Instance.Player.GetComponent<PlayerBehaviour>();
    }
}
