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
    [Header("Диалоговые персонажи с триггерами")]
    public GameObject DialogCharacters;

    private InterfaceElements UI;
    private InterfaceAnimations UIAnimations;
    private GameData gameData;

    public void DeleteActionButton() => StartCoroutine(DeleteActionButton_COR());

    private void OnTriggerEnter(Collider other)
    {
        var triggerData = other.gameObject.GetComponent<TriggerData>();
        if (triggerData != null)
        {
            switch (triggerData.TriggerPurpose)
            {
                case TriggerData.Purpose.Task:
                    if(!gameData.HasTasksCompleted[triggerData.TaskNumber - 1])
                        gameData.CurrentTaskNumber = triggerData.TaskNumber;
                    break;
                case TriggerData.Purpose.Dialog:
                    Canvas.GetComponentInChildren<DialogActions>().CurrentNPC = gameData.Player.GetComponent<VIDEDemoPlayer>().inTrigger;
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

    private void RotateMarks(GameObject triggers)
    {
        if (triggers != null)
        {
            for (var i = 0; i < triggers.transform.childCount; i++)
            {
                var currentChild = triggers.transform.GetChild(i);
                currentChild.GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
            }
        }
    }

    private void ActivateNpcMarks()
    {
        if (DialogCharacters != null)
        {
            for (var i = 0; i < DialogCharacters.transform.childCount; i++)
            {
                if (DialogCharacters.transform.GetChild(i).childCount > 1)
                    DialogCharacters.transform.GetChild(i).GetChild(1).GetComponentInChildren<Animator>().Play(TriggerData.MarkerAnimation);
            }
        }
    }
    
    void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        gameData = Canvas.GetComponent<GameData>();
        UI.ActionButton.gameObject.SetActive(false);
        RotateMarks(TaskTriggers);
        RotateMarks(EnterTriggers);
        RotateMarks(ScenarioTriggers);
        ActivateNpcMarks();
    } 
}
