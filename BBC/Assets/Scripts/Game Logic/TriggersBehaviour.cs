using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggersBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [Header("Триггеры заданий")]
    public GameObject TaskTriggers;
    [Header("Триггеры входа в помещение")]
    public GameObject EnterTriggers;
    [Header("Триггеры смены сцены")]
    public GameObject ChangeSceneTriggers;
    [Header("Триггеры активации сценарного момента")]
    public GameObject ScenarioTriggers;
    [Header("Триггер завершения уровня")]
    public GameObject FinishTrigger;
    [Header("Диалоговые персонажи с триггерами")]
    public GameObject DialogCharacters;

    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;
    private GameData gameData;

    public void DeleteActionButton() => StartCoroutine(DeleteActionButton_COR());

    public void ActivateTrigger_Any(GameObject trigger) => ActivateTrigger(trigger);

    public void ActivateTrigger_Task(int triggerNumber) => ActivateTrigger(TaskTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_ScriptMoment(int triggerNumber) => ActivateTrigger(ScenarioTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_EnterToMiniScene(int triggerNumber) => ActivateTrigger(EnterTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_ChangeScene(int triggerNumber) => ActivateTrigger(ChangeSceneTriggers.transform.GetChild(triggerNumber - 1).gameObject);

    public void ActivateTrigger_Dialogue(int npcOrderNumber) => ActivateTrigger(DialogCharacters.transform.GetChild(npcOrderNumber - 1).GetChild(1).gameObject);

    public void ActivateTrigger_Finish() => ActivateTrigger(FinishTrigger);

    private void ActivateTrigger(GameObject trigger)
    {
        trigger.SetActive(true);
        trigger.GetComponent<TargetWaypointBehaviour>().Waypoint.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        var triggerData = other.gameObject.GetComponent<TriggerData>();
        if (triggerData != null)
        {
            switch (triggerData.TriggerPurpose)
            {
                case TriggerData.Purpose.Task:
                    if(!gameData.HasTasksCompleted[triggerData.Task_TaskNumber - 1])
                        gameData.CurrentTaskNumber = triggerData.Task_TaskNumber;
                    break;
                case TriggerData.Purpose.Dialog:
                    
                    break;
            }
            Canvas.GetComponent<ActionButtonBehaviour>().ActivatedTrigger = triggerData;
            ActivateButton(triggerData.ActionButtonText);
        }
        else if (other.name.StartsWith("Coin"))
            StartCoroutine(PickCoinUp_COR(other));
    }

    private void OnTriggerExit(Collider other)
    {
        if (UI.ActionButton.IsActive() && other.GetComponent<TriggerData>() != null)
            StartCoroutine(DeleteActionButton_COR());
    }

    private void ActivateButton(string buttonText)
    {
        if (!UI.ActionButton.IsActive())
        {
            UI.ActionButton.gameObject.SetActive(true);     
            StartCoroutine(UIAnimations.ShowActionButton_COR());
        }
        UI.ActionButton.GetComponentInChildren<Text>().text = buttonText;
    }

    private IEnumerator DeleteActionButton_COR()
    {
        yield return StartCoroutine(UIAnimations.HideActionButton_COR());
        UI.ActionButton.gameObject.SetActive(false);
    }

    private IEnumerator PickCoinUp_COR(Collider coin)
    {
        coin.GetComponentInChildren<Animator>().Play("PickCoinUp");
        yield return new WaitForSeconds(1f);
        coin.gameObject.SetActive(false);
        gameData.CoinsCount++;
        Canvas.GetComponent<PadBehaviour>().UpdatePadData();
    }
    
    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        gameData = Canvas.GetComponent<GameData>();
        UI.ActionButton.gameObject.SetActive(false);
    } 
}
