using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButtonBehaviour : MonoBehaviour
{
    [Header("Èםעונפויס")]
    public GameObject Canvas;

    private RobotBehaviour robotBehaviour;
    private GameObject enterTriggers;

    public void ChangeScene()
    {
        var triggerNumber = Canvas.GetComponent<GameData>().currentChangeSceneTriggerNumber;
        robotBehaviour.currentMoveSpeed = robotBehaviour.freezeSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.freezeSpeed;
        if (Canvas.GetComponent<GameData>().SceneIndex != 0)
            enterTriggers.transform.GetChild(triggerNumber - 1).gameObject.SetActive(false);
        StartCoroutine(ChangeScene_COR(triggerNumber));
    }

    private IEnumerator ChangeScene_COR(int triggerNumber)
    {
        var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(2.5f);
        var currentTrigger = enterTriggers.transform.GetChild(triggerNumber - 1).gameObject;
        var previousCamera = currentTrigger.GetComponent<SwitchSceneBehaviour>().PreviousCamera;
        var nextCamera = currentTrigger.GetComponent<SwitchSceneBehaviour>().NextCamera;
        previousCamera.enabled = false;
        nextCamera.enabled = true;
        Canvas.GetComponent<GameData>().currentSceneCamera = nextCamera;
        var destinationTriggerNumber = currentTrigger.GetComponent<SwitchSceneBehaviour>().destinationTriggerNumber;
        Canvas.GetComponent<GameData>().Player.transform.position = enterTriggers.transform.GetChild(destinationTriggerNumber - 1).position;
        currentTrigger.SetActive(true);
        currentTrigger.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(2f);
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
    }

    private void Awake()
    {
        robotBehaviour = Canvas.GetComponent<GameData>().Player.GetComponent<RobotBehaviour>();
    }

    private void Start()
    {
        if (Canvas.GetComponent<GameData>().SceneIndex != 0)
            enterTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TriggersBehaviour>().EnterTriggers;
    }
}
