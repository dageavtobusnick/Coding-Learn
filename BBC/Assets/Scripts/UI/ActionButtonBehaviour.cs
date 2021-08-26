using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ActionButtonBehaviour : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas;
    public TriggerData ActivatedTrigger;
    public bool IsPressed;

    private PlayerBehaviour robotBehaviour;
    private TriggersBehaviour triggersBehaviour;
    private InterfaceElements UI;
    private GameData gameData;

    public void MakeAction() => StartCoroutine(MakeAction_COR());

    public IEnumerator ActivateTask_COR(bool hasActivateButton = true)
    {
        var currentTaskNumber = gameData.CurrentTaskNumber;
        Canvas.GetComponent<TaskPanelBehaviour>().TaskNumber = currentTaskNumber;
        gameData.IsTaskStarted = true;
        UI.ShowIDEButton.interactable = true;
        robotBehaviour.FreezePlayer();
        if (gameData.SceneIndex != 0)
        {
            ActivatedTrigger.gameObject.SetActive(false);
            yield return StartCoroutine(TurnOnTaskCamera_COR(currentTaskNumber, hasActivateButton));
        }
        Canvas.GetComponent<TaskPanelBehaviour>().ShowTask();
    }

    public void ActivateScenarioMoment()
    {
        switch (gameData.SceneIndex)
        {
            case 3:
                StartCoroutine(ActivateScenarioMoment_Level_3_COR(ActivatedTrigger.ScriptMoment_TriggerNumber));
                break;
        }
        ActivatedTrigger.gameObject.SetActive(false);
    }

    public void ActivateDialog() => StartCoroutine(ActivateDialog_COR());

    public void FinishLevel() => StartCoroutine(FinishLevel_COR());

    private IEnumerator MakeAction_COR()
    {
        UI.HideUI();
        if (Canvas.GetComponent<PadBehaviour>().IsPadCalled)
        {
            UI.Pad.GetComponentInParent<Animator>().Play("MoveRight_Pad");
            yield return new WaitForSeconds(0.667f);
        }
        UI.ChangeCallAvailability(false);
        ActivatedTrigger.GetComponent<TargetWaypointBehaviour>().Waypoint.gameObject.SetActive(false);
        switch (ActivatedTrigger.TriggerPurpose)
        {
            case TriggerData.Purpose.Task:
                StartCoroutine(ActivateTask_COR());
                yield break;

            case TriggerData.Purpose.EnterToMiniScene:
                StartCoroutine(EnterToMiniScene_COR());
                yield break;

            case TriggerData.Purpose.ScriptMoment:
                ActivateScenarioMoment();
                yield break;

            case TriggerData.Purpose.SaveGame:
                StartCoroutine(SaveGame_COR());
                yield break;

            case TriggerData.Purpose.Dialog:
                ActivateDialog();
                yield break;

            case TriggerData.Purpose.ChangeLevel:
                StartCoroutine(ChangeLevel_COR());
                yield break;

            case TriggerData.Purpose.FinishLevel:
                FinishLevel();
                yield break;
        }
    }  

    private IEnumerator ActivateDialog_COR()
    {
        ActivatedTrigger.transform.GetChild(0).gameObject.SetActive(false);
        var dialogue = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger;
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());      
        Canvas.GetComponent<VIDEUIManager1>().Interact(dialogue);
    }

    private IEnumerator ChangeLevel_COR()
    {
        //Canvas.GetComponent<SaveLoad>().Save_NextLevel();
        UI.BlackScreen.transform.localScale = new Vector3(1, 1, 1);
        UI.BlackScreen.GetComponentInChildren<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        SceneManager.LoadScene(ActivatedTrigger.ChangeLevel_NextLevelIndex);
    }

    private IEnumerator SaveGame_COR()
    {
        UI.ActionButton.interactable = false;
        Canvas.GetComponent<SaveLoad>().Save();
        ActivatedTrigger.GetComponentInChildren<Animator>().Play("Saving");
        var buttonText = UI.ActionButton.GetComponentInChildren<Text>();
        for (var i = 1; i <= 3; i++)
        {
            buttonText.text = "Сохранение" + new string('.', i);
            yield return new WaitForSeconds(0.667f);
        }
        buttonText.text = "Игра сохранена.";
        yield return new WaitForSeconds(1.5f);
        buttonText.text = "Сохранить игру";
        UI.ActionButton.interactable = true;
        UI.ChangeCallAvailability(true);
    }

    private IEnumerator TurnOnTaskCamera_COR(int currentTaskNumber, bool hasActivateButton)
    {
        if (hasActivateButton)
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());
        gameData.CurrentSceneCamera.GetComponent<PlayableDirector>().playableAsset = Resources.Load<PlayableAsset>("Timelines/Tasks/Level " + gameData.SceneIndex + "/MoveToTask_" + currentTaskNumber);
        gameData.CurrentSceneCamera.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(2f);    
    } 

    private IEnumerator EnterToMiniScene_COR()
    {
        robotBehaviour.FreezePlayer();
        ActivatedTrigger.gameObject.SetActive(false);
        var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(2.5f);
        UI.Minimap.SetActive(ActivatedTrigger.EnterToMiniScene_IsMinimapShouldActive);   
        gameData.Player.transform.position = ActivatedTrigger.EnterToMiniScene_DestinationTrigger.transform.position;
        ActivatedTrigger.gameObject.SetActive(true);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(2f);
        UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        robotBehaviour.UnfreezePlayer();
        UI.ChangeCallAvailability(true);
    }    

    private IEnumerator FinishLevel_COR()
    {
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());
        var finishMessage = gameData.FinishMessages[gameData.SceneIndex];
        UI.CloseExtendedTaskButton.gameObject.SetActive(false);
        UI.NextLevelButton.gameObject.SetActive(true);
        Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
        UI.ExtendedTaskTitle.text = finishMessage.Title;
        UI.ExtendedTaskDescription.text = finishMessage.Description;
    }

    private IEnumerator ActivateScenarioMoment_Level_3_COR(int triggerNumber)
    {
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideActionButton_COR());
        switch (triggerNumber)
        {
            case 1:
                var message = gameData.ScenarioMessages[0];
                UI.ExtendedTaskTitle.text = message.Title;
                UI.ExtendedTaskDescription.text = message.Description;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().IsTaskMessage = false;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
                Canvas.GetComponent<TargetPanelBehaviour>().TargetText = "Найти ключи, чтобы открыть ворота (0/3)";
                triggersBehaviour.ActivateTrigger_Task(8);
                triggersBehaviour.ActivateTrigger_EnterToMiniScene(1);
                triggersBehaviour.ActivateTrigger_EnterToMiniScene(3);
                break;
            case 2:
                var playableDirector = gameData.CurrentSceneCamera.GetComponent<PlayableDirector>();
                playableDirector.playableAsset = Resources.Load<PlayableAsset>("Timelines/Cutscenes/Level 3/OpenGates");
                playableDirector.Play();
                yield return new WaitForSeconds((float)playableDirector.playableAsset.duration);
                robotBehaviour.UnfreezePlayer();
                triggersBehaviour.ActivateTrigger_Finish();
                break;
        }
        UI.ChangeCallAvailability(true);
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (UI.ActionButton.IsActive() && !IsPressed)
            {
                IsPressed = true;
                StartCoroutine(MakeAction_COR());
            }
            else if (gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger)
                Canvas.GetComponent<VIDEUIManager1>().Interact(gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger);
        }     
    }

    private void Awake()
    {
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<PlayerBehaviour>();
        triggersBehaviour = gameData.Player.GetComponentInChildren<TriggersBehaviour>();
        UI = Canvas.GetComponent<InterfaceElements>();
    }
}
