using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskCompletingActions : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public List<bool> isTasksCompleted = new List<bool>();

    private int sceneIndex;
    private GameObject taskTriggers;
    private GameObject enterTriggers;
    private GameObject scenarioTriggers;
    private RobotBehaviour robotBehaviour;

    #region Служебные методы (не трогать!)
    public void MakeActions(int taskNumber)
    {
        if (!isTasksCompleted[taskNumber - 1])
        {
            if (sceneIndex == 0)
                Invoke("MakeActions_Level_Training", 0f);
            else StartCoroutine("MakeActions_Level_" + sceneIndex + "_Task_" + taskNumber);
            isTasksCompleted[taskNumber - 1] = true;
        }
    }

    private void MakeActions_Level_Training()
    {
        Canvas.GetComponent<TaskPanelBehaviour>().CloseTask();
        Canvas.GetComponent<GameData>().currentTaskNumber++;
        Canvas.GetComponent<InterfaceElements>().ActivateTaskButton.GetComponent<ActivateTaskButtonBehaviour>().ActivateTask();
    }

    private void CloseTask() => Canvas.GetComponent<TaskPanelBehaviour>().CloseTask();

    private void ReturnToScene() => Canvas.GetComponent<TaskPanelBehaviour>().ReturnToScene();

    private void Start()
    {
        sceneIndex = Canvas.GetComponent<GameData>().SceneIndex;
        robotBehaviour = Canvas.GetComponent<GameData>().Player.GetComponent<RobotBehaviour>();
        if (sceneIndex != 0)
        {
            taskTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TaskTriggersBehaviour>().TaskTriggers;
            enterTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TaskTriggersBehaviour>().EnterTriggers;
            scenarioTriggers = Canvas.GetComponent<GameData>().Player.GetComponent<TaskTriggersBehaviour>().ScenarioTriggers;
        }
        for (var i = 0; i < 9; i++)
            isTasksCompleted.Add(false);
        switch (sceneIndex)
        {
            case 3:
                taskTriggers.transform.GetChild(1).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(2).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(4).gameObject.SetActive(false);
                taskTriggers.transform.GetChild(7).gameObject.SetActive(false);
                enterTriggers.transform.GetChild(0).gameObject.SetActive(false);
                enterTriggers.transform.GetChild(2).gameObject.SetActive(false);
                scenarioTriggers.transform.GetChild(1).gameObject.SetActive(false);
                var boards = GameObject.Find("ScriptingBoards");
                var stumps = GameObject.Find("ScriptingStumps");
                var newBridge = GameObject.Find("NewBridge");
                var unearthedItems = GameObject.Find("UnearthedItems");
                var gatesKeys = GameObject.Find("GatesKeys");
                for (var i = 0; i < boards.transform.childCount; i++)
                    boards.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < stumps.transform.childCount; i++)
                    stumps.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < newBridge.transform.childCount; i++)
                    newBridge.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < unearthedItems.transform.childCount; i++)
                    unearthedItems.transform.GetChild(i).gameObject.SetActive(false);
                for (var i = 0; i < gatesKeys.transform.childCount; i++)
                    gatesKeys.transform.GetChild(i).gameObject.SetActive(false);
                break;
        }
    }
    #endregion

    #region Пример
    private IEnumerator MakeActions_Level_НомерУровня_Task_НомерЗадания()             // Шаблон для названия методов
    {
        GameObject.Find("Имя объекта").GetComponent<Animator>().Play("Имя анимации"); // Находим объект по имени как в Иерархии и запускаем анимацию (по имени из Animator Controller-а)
        yield return new WaitForSeconds(2f);                                          // Ставим задержку, равную времени анимации в строчке выше, чтобы анимации проигрывали по очереди. Если надо одновременно - не ставим (задержка в секундах, тип float)
        GameObject.Find("Имя объекта").GetComponent<Animator>().Play("Имя анимации");
        yield return new WaitForSeconds(3f);
        CloseTask();                                                                  // После проигрывания всех анимаций завершаем задание методом CloseTask
                                                                                      // В данном примере будет проиграна некая анимация, через 2 секунды - другая анимация, а ещё через 3 секунды задание завершится
    }
    #endregion

    #region Анимации для 1-го уровня
    private IEnumerator MakeActions_Level_1_Task_1()
    {
        for (var i = 1; i <= 6; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_2()
    {
        var mushroom = GameObject.Find("Mushroom");
        mushroom.GetComponent<Animator>().Play("PickUp");
        yield return new WaitForSeconds(1.95f);
        mushroom.SetActive(false);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_3()
    {
        for (var i = 7; i <= 10; i++)
        {
            GameObject.Find("Flower_" + i).GetComponent<Animator>().Play("Move_Flower_" + i);
            yield return new WaitForSeconds(2.2f);
        }
        GameObject.Find("Flower_" + 11).GetComponent<Animator>().Play("Move_Flower_" + 11);
        yield return new WaitForSeconds(5f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_1_Task_4()
    {
        for (var i = 1; i <= 8; i++)
        {
            GameObject.Find("Rock_" + i).GetComponent<Animator>().Play("Rock_ToUp");
            yield return new WaitForSeconds(1.9f);
        }
        CloseTask();
    }
    #endregion

    #region Анимации для 2-го уровня
    private IEnumerator MakeActions_Level_2_Task_1()
    {
        GameObject.Find("GreenLight_1").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_2()
    {
        GameObject.Find("ScriptingMemoryTree_1").GetComponent<Animator>().Play("ScaleBigTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_2").GetComponent<Animator>().Play("ScaleSmallTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_3").GetComponent<Animator>().Play("ScaleBigTreeUp");
        yield return new WaitForSeconds(2f);
        GameObject.Find("ScriptingMemoryTree_4").GetComponent<Animator>().Play("ScaleSmallTreeUp");
        yield return new WaitForSeconds(2f);
        for (var i = 1; i <= 3; i++)
        {
            GameObject.Find("MemoryTreeLog_" + i).GetComponent<Animator>().Play("PickLogUp");
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        for (var i = 1; i <= 3; i++)
            GameObject.Find("MemoryTreeLog_" + i).SetActive(false);
        GameObject.Find("ScriptingMemoryTree_1").GetComponent<Animator>().Play("ScaleBigTreeDown");
        GameObject.Find("ScriptingMemoryTree_2").GetComponent<Animator>().Play("ScaleSmallTreeDown");
        GameObject.Find("ScriptingMemoryTree_3").GetComponent<Animator>().Play("ScaleBigTreeDown");
        GameObject.Find("ScriptingMemoryTree_4").GetComponent<Animator>().Play("ScaleSmallTreeDown");
        yield return new WaitForSeconds(2f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_3()
    {
        GameObject.Find("GreenLight_2").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_4()
    {
        for (var i = 1; i <= 9; i++)
        {
            var mushroom = GameObject.Find("ScriptingMushroom_" + i).transform.GetChild(0);
            if (i % 2 == 1)
            {
                mushroom.GetComponent<Animator>().Play("MoveMushroom");
                yield return new WaitForSeconds(2f);
            }
            else
            {
                mushroom.GetComponent<Animator>().Play("PutDown_Mushroom_" + i);
                yield return new WaitForSeconds(3f);
            }
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_5()
    {
        GameObject.Find("ScriptingTree").GetComponent<Animator>().Play("ToUp_BrokenTree");
        yield return new WaitForSeconds(2f);
        GameObject.Find("RedLight_1").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        GameObject.Find("ScriptingWagon").GetComponent<Animator>().Play("ToUp_Wagon");
        yield return new WaitForSeconds(2f);
        GameObject.Find("RedLight_2").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        GameObject.Find("GreenLight_3").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_6()
    {
        for (var i = 1; i <= 5; i++)
        {
            GameObject.Find("BridgeFence_" + i).GetComponent<Animator>().Play("Move_BridgeFence_" + i);
            yield return new WaitForSeconds(2.7f);
        }
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_7()
    {
        GameObject.Find("GreenLight_5").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }

    private IEnumerator MakeActions_Level_2_Task_8()
    {
        GameObject.Find("RedLight_3").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_4").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("RedLight_5").GetComponent<Animator>().Play("LightTurnOn");
        GameObject.Find("GreenLight_4").GetComponent<Animator>().Play("LightTurnOn");
        yield return new WaitForSeconds(4f);
        CloseTask();
    }
    #endregion

    private IEnumerator MakeActions_Level_3_Task_1()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        var boxes = GameObject.Find("Boxes");
        for (var i = 0; i < boxes.transform.childCount; i++)
        {
            var box = boxes.transform.GetChild(i);
            box.GetChild(0).GetComponent<Animator>().Play("BreakBox");
            yield return new WaitForSeconds(1.833f);
            box.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(1).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_2()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        var boxes = GameObject.Find("Boxes");
        for (var i = 0; i < 2; i++)
        {
            var box = boxes.transform.GetChild(i).GetChild(0);
            for (var j = 1; j < 3; j++)
            {
                box.GetChild(j).GetChild(0).gameObject.GetComponent<Animator>().Play("PickUpItem");
                yield return new WaitForSeconds(1f);
            }
        }
        GameObject.Find("Hatchet").GetComponent<Animator>().Play("PickUpAxe");
        yield return new WaitForSeconds(2f);
        GameObject.Find("Hatchet").SetActive(false);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(2).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(2).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_3()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        var axe = GameObject.Find("Box_3_Parent").transform.GetChild(0).GetChild(1).gameObject;
        axe.SetActive(true);
        axe.GetComponent<Animator>().Play("UseAxe_Vertical");
        yield return new WaitForSeconds(4f);
        var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        axe.SetActive(false);
        var logs = GameObject.Find("ScriptingLogs");
        for (var i = 0; i < logs.transform.childCount; i++)
            logs.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        ReturnToScene();
    }

    private IEnumerator MakeActions_Level_3_Task_4()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        var axe = GameObject.Find("Box_3_Parent").transform.GetChild(0).GetChild(1).gameObject;
        axe.SetActive(true);
        axe.GetComponent<Animator>().Play("UseAxe_Horizontal");
        yield return new WaitForSeconds(4f);
        var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        axe.SetActive(false);
        var boards = GameObject.Find("ScriptingBoards");
        var stumps = GameObject.Find("ScriptingStumps");
        var trees = GameObject.Find("ScriptingTrees");
        for (var i = 0; i < boards.transform.childCount; i++)
            boards.transform.GetChild(i).gameObject.SetActive(true);
        for (var i = 0; i < stumps.transform.childCount; i++)
            stumps.transform.GetChild(i).gameObject.SetActive(true);
        for (var i = 0; i < trees.transform.childCount; i++)
            trees.transform.GetChild(i).gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        ReturnToScene();
        yield return new WaitForSeconds(2f);
        taskTriggers.transform.GetChild(4).gameObject.SetActive(true);
        taskTriggers.transform.GetChild(4).GetChild(0).GetChild(0).gameObject.GetComponent<Animator>().Play("RotateExclamationMark");
    }

    private IEnumerator MakeActions_Level_3_Task_5()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        var boards = GameObject.Find("ScriptingBoards");
        for (var i = 0; i < 3; i++)
        {
            boards.transform.GetChild(i).gameObject.GetComponent<Animator>().Play("UseBoard_" + (i + 1));
            yield return new WaitForSeconds(1.5f);
        }
        boards.transform.GetChild(3).gameObject.GetComponent<Animator>().Play("UseBoard_4");
        var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.5f);
        GameObject.Find("BrokenBridge").SetActive(false);
        GameObject.Find("ScriptingBoards").SetActive(false);
        var newBridge = GameObject.Find("NewBridge");
        for (var i = 0; i < newBridge.transform.childCount; i++)
            newBridge.transform.GetChild(i).gameObject.SetActive(true);
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("lever").GetComponent<Animator>().Play("ActivateLever");
        yield return new WaitForSeconds(1f);
        newBridge.GetComponent<Animator>().Play("GetBridgeDown");
        yield return new WaitForSeconds(1.5f);
        ReturnToScene();
    }

    private IEnumerator MakeActions_Level_3_Task_6()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        Canvas.GetComponent<GameData>().currentSceneCamera.GetComponent<Animator>().Play("CheckAllPlaces");
        yield return new WaitForSeconds(10.5f);
        GameObject.Find("Key_MiniScene_1").GetComponent<Animator>().Play("PickUpKey_1");
        yield return new WaitForSeconds(2f);
        GameObject.Find("Key_MiniScene_1").SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Canvas.GetComponent<GameData>().taskItemsCount++;
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_7()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        Canvas.GetComponent<GameData>().currentSceneCamera.GetComponent<Animator>().Play("CheckAllChests");
        yield return new WaitForSeconds(2f);

        var chest1 = GameObject.Find("Chest_1");
        chest1.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest1.transform.childCount; i++)
            chest1.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        yield return new WaitForSeconds(3f);
        chest1.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        var chest2 = GameObject.Find("Chest_2");
        chest2.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest2.transform.childCount; i++)
            chest2.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        yield return new WaitForSeconds(3f);
        chest2.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        var chest3 = GameObject.Find("Chest_3");
        chest3.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("OpenChest");
        yield return new WaitForSeconds(1.5f);
        for (var i = 2; i < chest3.transform.childCount - 1; i++)
            chest3.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Animator>().Play("CheckItem");
        var key = chest3.transform.GetChild(chest3.transform.childCount - 1).GetChild(0).gameObject;
        key.GetComponent<Animator>().Play("PickUpKey_2");
        yield return new WaitForSeconds(4f);
        key.SetActive(false);
        chest3.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("CloseChest");
        yield return new WaitForSeconds(4f);

        Canvas.GetComponent<GameData>().taskItemsCount++;
        robotBehaviour.currentMoveSpeed = robotBehaviour.moveSpeed;
        robotBehaviour.currentRotateSpeed = robotBehaviour.rotateSpeed;
        TurnOnScenarioTrigger2_Level_3();
    }

    private IEnumerator MakeActions_Level_3_Task_8()
    {
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().HideTaskPanel_COR());
        GameObject.Find("robot3").GetComponent<Animator>().Play("ActivateDigger");
        yield return new WaitForSeconds(3.8f);
        var blackScreen = Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.GetChild(0).gameObject;
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(3f);
        var buriedItems = GameObject.Find("BuriedItems");
        var unearthedItems = GameObject.Find("UnearthedItems");
        for (var i = 0; i < buriedItems.transform.childCount; i++)
        {
            buriedItems.transform.GetChild(i).gameObject.SetActive(false);
            unearthedItems.transform.GetChild(i).gameObject.SetActive(true);
        }
        blackScreen.GetComponent<Animator>().Play("HideBlackScreen");
        yield return new WaitForSeconds(1.4f);
        Canvas.GetComponent<InterfaceElements>().BlackScreen.transform.localScale = new Vector3(0, 0, 0);
        var key = unearthedItems.transform.GetChild(0).gameObject;
        key.GetComponent<Animator>().Play("PickUpKey_3");
        yield return new WaitForSeconds(2f);
        key.SetActive(false);
        ReturnToScene();
        Canvas.GetComponent<GameData>().taskItemsCount++;
        TurnOnScenarioTrigger2_Level_3();
    }

    private void TurnOnScenarioTrigger2_Level_3()
    {
        if (Canvas.GetComponent<GameData>().taskItemsCount == 3)
        {
            var scenarioTrigger1 = scenarioTriggers.transform.GetChild(0).gameObject;
            var scenarioTrigger2 = scenarioTriggers.transform.GetChild(1).gameObject;
            scenarioTrigger1.SetActive(false);
            scenarioTrigger2.SetActive(true);
            scenarioTrigger2.transform.GetChild(0).GetChild(0).GetComponent<Animator>().Play("RotateExclamationMark");
        }
    }
}
