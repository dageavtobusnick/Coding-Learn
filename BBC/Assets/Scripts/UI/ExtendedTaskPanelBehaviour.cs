﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtendedTaskPanelBehaviour : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public bool isTaskMessage;

    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;
    private GameObject blackScreen;
    private GameData gameData;
    private LoadLevel levelLoader;

    public void OpenTaskExtendedDescription() => StartCoroutine(OpenTaskExtendedDescription_COR());

    public void OpenTaskExtendedDescription_Special() => StartCoroutine(UIAnimations.ShowExtendedTaskPanel_COR());

    public void CloseTaskExtendedDescription() => StartCoroutine(CloseTaskExtendedDescription_COR());

    public void GoToNextLevel() => StartCoroutine(GoToNextLevel_COR());   

    private IEnumerator OpenTaskExtendedDescription_COR()
    {  
        yield return StartCoroutine(UIAnimations.HideTaskPanel_COR());
        StartCoroutine(UIAnimations.ShowExtendedTaskPanel_COR());
    }

    private IEnumerator CloseTaskExtendedDescription_COR()
    {
        yield return StartCoroutine(UIAnimations.HideExtendedTaskPanel_COR());
        if (isTaskMessage)
            yield return StartCoroutine(UIAnimations.ShowTaskPanel_COR());
        else
        {
            UI.Minimap.SetActive(true);
            UI.ChangeCallAvailability(true);
            isTaskMessage = true;
        }
        Canvas.GetComponent<TrainingScript>().TryShowTraining(TrainingScript.PreviousAction.ExtendedTaskClosing);
    }

    private IEnumerator GoToNextLevel_COR()
    {
        Canvas.GetComponent<SaveLoad>().Save_NextLevel();
        gameData.UpdatePlayerData();
        gameData.IsTimerStopped = true;
        yield return StartCoroutine(UIAnimations.HideExtendedTaskPanel_COR());
        UI.BlackScreen.transform.localScale = new Vector3(1, 1, 1);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        if (gameData.SceneIndex == 0)
            StartCoroutine(levelLoader.LoadLevelAsync_COR(2));
        StartCoroutine(levelLoader.LoadLevelAsync_COR(gameData.SceneIndex + 1));
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        gameData = Canvas.GetComponent<GameData>();
        levelLoader = Canvas.GetComponent<LoadLevel>();
        blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        isTaskMessage = gameData.SceneIndex == 0;
        if (isTaskMessage)
        {
            gameData.CurrentTaskNumber = 1;
            StartCoroutine(Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask_COR(false));
        }
        else
        {
            var startMessage = gameData.StartMessages[gameData.SceneIndex - 1];
            UI.ExtendedTaskTitle.text = startMessage.Title;
            UI.ExtendedTaskDescription.text = startMessage.Description;
            UI.Minimap.SetActive(false);
            OpenTaskExtendedDescription_Special();
        }      
    }
}
