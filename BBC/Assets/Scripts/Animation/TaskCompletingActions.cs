using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskCompletingActions : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas; 
    [HideInInspector]
    public List<bool> isTasksCompleted = new List<bool>();

    private int sceneIndex;

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

    private void Start()
    {
        sceneIndex = Canvas.GetComponent<GameData>().SceneIndex;
        for (var i = 0; i < 9; i++)
            isTasksCompleted.Add(false);
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
}
