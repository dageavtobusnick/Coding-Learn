using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ActionButtonBehaviour : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public TriggerData ActivatedTrigger;

    private RobotBehaviour robotBehaviour;
    private GameObject taskTriggers;
    private GameObject enterTriggers;
    private InterfaceElements UI;
    private GameData gameData;

    public void MakeAction() => StartCoroutine(MakeAction_COR());

    public IEnumerator ActivateTask_COR(bool hasActivateButton = true)
    {
        var currentTaskNumber = gameData.CurrentTaskNumber;
        Canvas.GetComponent<TaskPanelBehaviour>().taskNumber = currentTaskNumber;
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
                StartCoroutine(ActivateScenarioMoment_Level_3_COR(ActivatedTrigger.TriggerNumber));
                break;
        }
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
        SceneManager.LoadScene(ActivatedTrigger.NextLevelIndex);
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
        gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToTask_" + currentTaskNumber);
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
        var triggerBehaviour = ActivatedTrigger.GetComponent<SwitchSceneBehaviour>();
        triggerBehaviour.PreviousCamera.enabled = false;
        triggerBehaviour.NextCamera.enabled = true;
        gameData.CurrentSceneCamera = triggerBehaviour.NextCamera;
        UI.Minimap.SetActive(triggerBehaviour.IsMinimapShouldActive);   
        gameData.Player.transform.position = triggerBehaviour.DestinationTrigger.transform.position;
        ActivatedTrigger.gameObject.SetActive(true);
        ActivatedTrigger.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
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
        ActivatedTrigger.gameObject.SetActive(false);
        switch (triggerNumber)
        {
            case 1:
                var message = gameData.ScenarioMessages[0];
                UI.ExtendedTaskTitle.text = message.Title;
                UI.ExtendedTaskDescription.text = message.Description;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().isTaskMessage = false;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
                ActivateTrigger(taskTriggers, 7);
                ActivateTrigger(enterTriggers, 0);
                ActivateTrigger(enterTriggers, 2);
                break;
            case 2:
                gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToTask_9_SceneCamera_14");
                yield return new WaitForSeconds(2f);
                var gatesKeys = GameObject.Find("GatesKeys");
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(true);
                    key.GetComponentInChildren<Animator>().Play("TurnKey");
                }
                yield return new WaitForSeconds(3f);
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(false);
                }
                var blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
                UI.BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
                yield return new WaitForSeconds(1.5f);
                GameObject.Find("LeftGate").GetComponent<Animator>().Play("OpenLeftGate");
                GameObject.Find("RightGate").GetComponent<Animator>().Play("OpenRightGate");
                yield return new WaitForSeconds(2f);
                blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
                yield return new WaitForSeconds(1.4f);
                UI.BlackScreen.transform.localScale = new Vector3(0, 0, 0);
                gameData.CurrentSceneCamera.GetComponent<Animator>().Play("MoveToScene_TaskCamera_9");
                yield return new WaitForSeconds(2f);
                robotBehaviour.UnfreezePlayer();
                break;
        }
        UI.ChangeCallAvailability(true);
    } 

    private void ActivateTrigger(GameObject triggersGroup, int childNumber)
    {
        var childTrigger = triggersGroup.transform.GetChild(childNumber).gameObject;
        childTrigger.SetActive(true);
        childTrigger.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
    }

    private void Awake()
    {
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        UI = Canvas.GetComponent<InterfaceElements>();
    }

    private void Start()
    {            
        if (gameData.SceneIndex != 0)
        {
            taskTriggers = gameData.Player.GetComponentInChildren<TriggersBehaviour>().TaskTriggers;
            enterTriggers = gameData.Player.GetComponentInChildren<TriggersBehaviour>().EnterTriggers;
        }
    }
}
