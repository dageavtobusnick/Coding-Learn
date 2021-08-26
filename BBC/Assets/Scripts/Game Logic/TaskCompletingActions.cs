﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TaskCompletingActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;

    private int sceneIndex;
    private GameObject player;
    private TargetPanelBehaviour targetPanelBehaviour;
    private TriggersBehaviour triggersBehaviour;
    private GameData gameData;
    private InterfaceAnimations UIAnimations;

    public void MakeActions(int taskNumber)
    {
        if (!gameData.HasTasksCompleted[taskNumber - 1])
        {
            if (sceneIndex == 0)
                StartCoroutine(MakeActions_Level_Training());
            else StartCoroutine("MakeActions_Level_" + sceneIndex + "_Task_" + taskNumber);
            gameData.HasTasksCompleted[taskNumber - 1] = true;
        }
    }

    private IEnumerator WaitAndHideTaskPanel_COR()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(UIAnimations.HideTaskPanel_COR());
    }

    private IEnumerator ReturnToScene_COR()
    {
        gameData.Player.GetComponentInChildren<TriggersBehaviour>().DeleteActionButton();
        yield return StartCoroutine(Canvas.GetComponent<TaskPanelBehaviour>().ReturnToScene_COR());
    }

    private void Start()
    {
        gameData = Canvas.GetComponent<GameData>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
        sceneIndex = gameData.SceneIndex;
        player = gameData.Player;
        targetPanelBehaviour = Canvas.GetComponent<TargetPanelBehaviour>();
        triggersBehaviour = player.GetComponentInChildren<TriggersBehaviour>();
    }

    private void ActivateTrigger_Task(int triggerNumber) => triggersBehaviour.ActivateTrigger_Task(triggerNumber);

    private void ActivateTrigger_ScriptMoment(int triggerNumber) => triggersBehaviour.ActivateTrigger_ScriptMoment(triggerNumber);

    private void ActivateTrigger_Finish() => triggersBehaviour.ActivateTrigger_Finish();

    private IEnumerator PlayPostTaskAnimation_COR(string animationOwnerName)
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        var playableDirector = GameObject.Find(animationOwnerName).GetComponent<PlayableDirector>();
        playableDirector.Play();
        yield return new WaitForSeconds((float)playableDirector.playableAsset.duration + 0.5f);
    }

    #region Действия для обучающего уровня
    private IEnumerator MakeActions_Level_Training()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        if (gameData.CurrentTaskNumber == gameData.TaskTexts.Length)
            Canvas.GetComponent<ActionButtonBehaviour>().FinishLevel();
        else
        {
            gameData.CurrentTaskNumber++;
            StartCoroutine(Canvas.GetComponent<ActionButtonBehaviour>().ActivateTask_COR());
        }   
    }
    #endregion

    #region Действия для 1-го уровня
    private IEnumerator MakeActions_Level_1_Task_1()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_1"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Пройти подготовку (1/4)");
        ActivateTrigger_Task(2);
    }

    private IEnumerator MakeActions_Level_1_Task_2()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_2"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Пройти подготовку (2/4)");
        ActivateTrigger_Task(3);
    }

    private IEnumerator MakeActions_Level_1_Task_3()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_3"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Пройти подготовку (3/4)");
        ActivateTrigger_Task(4);
    }

    private IEnumerator MakeActions_Level_1_Task_4()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_4"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Завершить подготовку)");
        ActivateTrigger_Finish();
    }
    #endregion

    #region Действия для 2-го уровня
    private IEnumerator MakeActions_Level_2_Task_1()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_1"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Двигаться вглубь леса");
        ActivateTrigger_Task(2);
    }

    private IEnumerator MakeActions_Level_2_Task_2()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_2"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Вернуться к развилке и двигаться другим путём");
        ActivateTrigger_Task(3);
    }

    private IEnumerator MakeActions_Level_2_Task_3()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_3"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Продолжить путь по левой тропе");
        ActivateTrigger_Task(4);
    }

    private IEnumerator MakeActions_Level_2_Task_4()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_4"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Вернуться к развилке и двигаться другим путём");
        ActivateTrigger_Task(5);
    }

    private IEnumerator MakeActions_Level_2_Task_5()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_5"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Двигаться вглубь леса");
        ActivateTrigger_Task(6);
    }

    private IEnumerator MakeActions_Level_2_Task_6()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_6"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Пересечь реку и продолжить движение");
        ActivateTrigger_Task(7);
    }

    private IEnumerator MakeActions_Level_2_Task_7()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_7"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Двигаться по правой тропе");
        ActivateTrigger_Task(8);
    }

    private IEnumerator MakeActions_Level_2_Task_8()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_8"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Покинуть лес");
        ActivateTrigger_Finish();
    }
    #endregion

    #region Действия для 3-го уровня
    private IEnumerator MakeActions_Level_3_Task_1()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_1"));
        yield return StartCoroutine(ReturnToScene_COR());
        ActivateTrigger_Task(2);
    }

    private IEnumerator MakeActions_Level_3_Task_2()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_2"));
        yield return StartCoroutine(ReturnToScene_COR());
        ActivateTrigger_Task(3);
    }

    private IEnumerator MakeActions_Level_3_Task_3()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_3"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Двигаться к поселению");
        ActivateTrigger_Task(4);
    }

    private IEnumerator MakeActions_Level_3_Task_4()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_4"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Починить мост");
        ActivateTrigger_Task(5);
    }

    private IEnumerator MakeActions_Level_3_Task_5()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_5"));
        yield return StartCoroutine(ReturnToScene_COR());
        targetPanelBehaviour.ChangeTarget("Двигаться к поселению");
        ActivateTrigger_ScriptMoment(1);
    }

    private IEnumerator MakeActions_Level_3_Task_6()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_6"));
        gameData.TaskItemsCount++;
        yield return StartCoroutine(ReturnToScene_COR());
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_7()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_7"));
        gameData.TaskItemsCount++;
        yield return StartCoroutine(ReturnToScene_COR());
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_8()
    {
        yield return StartCoroutine(PlayPostTaskAnimation_COR("AnimatedItems_Task_8"));
        gameData.TaskItemsCount++;
        yield return StartCoroutine(ReturnToScene_COR());
        TurnOnScenarioTrigger2_Level_3();
    }

    private void TurnOnScenarioTrigger2_Level_3()
    {
        if (gameData.TaskItemsCount == 3)
        {
            triggersBehaviour.ActivateTrigger_ScriptMoment(2);
            targetPanelBehaviour.ChangeTarget("Открыть ворота и покинуть поселение");
        }
        else targetPanelBehaviour.ChangeTarget("Найти ключи, чтобы открыть ворота (" + gameData.TaskItemsCount + "/3)");
    }
    #endregion

    #region Действия для 4-го уровня
    private IEnumerator MakeActions_Level_4_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(11);
    }

    private IEnumerator MakeActions_Level_4_Task_2()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(3);
    }

    private IEnumerator MakeActions_Level_4_Task_3()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(10);
    }

    private IEnumerator MakeActions_Level_4_Task_4()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(10);
    }
    #endregion

    #region Действия для 5-го уровня
    private IEnumerator MakeActions_Level_5_Task_1()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(1);
    }

    private IEnumerator MakeActions_Level_5_Task_2()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(3);
    }

    private IEnumerator MakeActions_Level_5_Task_3()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
        triggersBehaviour.ActivateTrigger_Dialogue(4);

    }

    private IEnumerator MakeActions_Level_5_Task_4()
    {
        yield return StartCoroutine(WaitAndHideTaskPanel_COR());
        yield return StartCoroutine(ReturnToScene_COR());
    }
    #endregion
}
