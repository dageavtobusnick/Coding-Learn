using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioButtonBehaviour : MonoBehaviour
{
    [Header("Èםעונפויס")]
    public GameObject Canvas;

    private InterfaceElements UI;
    private RobotBehaviour robotBehaviour;
    private GameObject scenarioTriggers;
    private GameObject taskTriggers;
    private GameObject enterTriggers;
    private int sceneIndex;

    public void ActivateScenarioMoment()
    {
        var triggerNumber = Canvas.GetComponent<GameData>().currentScenarioTriggerNumber;       
        switch (sceneIndex)
        {
            case 3:
                StartCoroutine(ActivateScenarioMoment_Level_3_COR(triggerNumber));
                break;
        }
    }

    private IEnumerator ActivateScenarioMoment_Level_3_COR(int triggerNumber)
    {
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideScenarioButton_COR());
        scenarioTriggers.transform.GetChild(triggerNumber - 1).gameObject.SetActive(false);
        switch (triggerNumber)
        {
            case 1:
                var message = Canvas.GetComponent<GameData>().ScenarioMessages[0];
                UI.ExtendedTaskTitle.text = message.Title;
                UI.ExtendedTaskDescription.text = message.Description;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().isTask = false;
                Canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription_Special();
                ActivateTrigger(taskTriggers, 7);
                ActivateTrigger(enterTriggers, 0);
                ActivateTrigger(enterTriggers, 2);
                break;
            case 2:
                Canvas.GetComponent<GameData>().currentSceneCamera.GetComponent<Animator>().Play("MoveToTask_9_SceneCamera_14");
                yield return new WaitForSeconds(2f);
                var gatesKeys = GameObject.Find("GatesKeys");
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(true);
                    key.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("TurnKey");
                }
                yield return new WaitForSeconds(3f);
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                {
                    var key = gatesKeys.transform.GetChild(i).gameObject;
                    key.SetActive(false);
                }
                var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
                Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
                yield return new WaitForSeconds(1.5f);
                GameObject.Find("LeftGate").GetComponent<Animator>().Play("OpenLeftGate");
                GameObject.Find("RightGate").GetComponent<Animator>().Play("OpenRightGate");
                yield return new WaitForSeconds(2f);
                blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
                yield return new WaitForSeconds(1.4f);
                Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
                Canvas.GetComponent<GameData>().currentSceneCamera.GetComponent<Animator>().Play("MoveToScene_TaskCamera_9");
                yield return new WaitForSeconds(2f);
                robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
                robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
                break;
        }
    }

    private void ActivateTrigger(GameObject triggersGroup, int childNumber)
    {
        var childTrigger = triggersGroup.transform.GetChild(childNumber).gameObject;
        childTrigger.SetActive(true);
        childTrigger.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private void Awake()
    {
        robotBehaviour = Canvas.GetComponent<GameData>().Player.GetComponent<RobotBehaviour>();
    }

    private void Start()
    {
        sceneIndex = Canvas.GetComponent<GameData>().SceneIndex;
        UI = Canvas.GetComponent<InterfaceElements>();
        if (sceneIndex != 0)
        {
            scenarioTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TriggersBehaviour>().ScenarioTriggers;
            taskTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TriggersBehaviour>().TaskTriggers;
            enterTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TriggersBehaviour>().EnterTriggers;
        }
    }
}
