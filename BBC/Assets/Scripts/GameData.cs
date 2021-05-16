using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [Serializable]
    public class TaskText
    {
        public int ID;
        public string Title;
        public string Description;
        public string ExtendedDescription;
        public string StartCode;
    }

    [Serializable]
    public class Test
    {
        public int TaskNumber;
        public string Code;
    }

    [Serializable]
    public class Message
    {
        public int LevelNumber;
        public string Title;
        public string Description; 
    }

    [Serializable]
    public class ScenarioMessage
    {
        public string Title;
        public string Description;
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
    
    [Header("Игрок")]
    public GameObject Player;
    [Header ("Текущая включенная камера на сцене")]
    public Camera currentSceneCamera;
    [Header("Номер текущего задания")]
    public int currentTaskNumber;
    [Header("Номер текущего триггера смены сцены")]
    public int currentChangeSceneTriggerNumber;
    [Header("Номер текущего сценарного триггера")]
    public int currentScenarioTriggerNumber;
    [Header("Индекс сцены")]
    public int SceneIndex;
    [HideInInspector]
    public TaskText[] TaskTexts;
    [HideInInspector]
    public Test[] Tests;
    [HideInInspector]
    public ScenarioMessage[] ScenarioMessages;
    [HideInInspector]
    public Message[] StartMessages;
    [HideInInspector]
    public Message[] FinishMessages;
    [HideInInspector]
    public int taskItemsCount;

    private void Awake()
    {
        taskItemsCount = 0;
        SceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (SceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneIndex = 0;
        var tasksFile = Resources.Load<TextAsset>("Tasks Level " + SceneIndex);
        var testsFile = Resources.Load<TextAsset>("Tests Level " + SceneIndex);
        var scenarioMessagesFile = Resources.Load<TextAsset>("Scenario Messages Level " + SceneIndex);
        var startMessagesFile = Resources.Load<TextAsset>("Start Messages");
        var finishMessagesFile = Resources.Load<TextAsset>("Finish Messages");
        TaskTexts = JsonHelper.FromJson<TaskText>(tasksFile.text);
        Tests = JsonHelper.FromJson<Test>(testsFile.text);
        StartMessages = JsonHelper.FromJson<Message>(startMessagesFile.text);
        FinishMessages = JsonHelper.FromJson<Message>(finishMessagesFile.text);
        if (SceneIndex > 2)
            ScenarioMessages = JsonHelper.FromJson<ScenarioMessage>(scenarioMessagesFile.text);

        #region Код для дебагга
        /*var tasksFile_0 = Resources.Load<TextAsset>("Tasks Level 0");
        Debug.Log(tasksFile_0);
        var tasksFile_1 = Resources.Load<TextAsset>("Tasks Level 1");
        Debug.Log(tasksFile_1);
        var tasksFile_2 = Resources.Load<TextAsset>("Tasks Level 2");
        Debug.Log(tasksFile_2);
        var testsFile_0 = Resources.Load<TextAsset>("Tests Level 0");
        Debug.Log(testsFile_0);
        var testsFile_1 = Resources.Load<TextAsset>("Tests Level 1");
        Debug.Log(testsFile_1);
        var testsFile_2 = Resources.Load<TextAsset>("Tests Level 2");
        Debug.Log(testsFile_2);
        var startMessagesFile = Resources.Load<TextAsset>("Start Messages");
        var finishMessagesFile = Resources.Load<TextAsset>("Finish Messages");
        var Tasks_0 = JsonHelper.FromJson<TaskText>(tasksFile_0.text);
        Debug.Log(Tasks_0);
        var Tasks_1 = JsonHelper.FromJson<TaskText>(tasksFile_1.text);
        Debug.Log(Tasks_1);
        var Tasks_2 = JsonHelper.FromJson<TaskText>(tasksFile_2.text);
        Debug.Log(Tasks_2);
        var Tests_0 = JsonHelper.FromJson<Test>(testsFile_0.text);
        Debug.Log(Tests_0);
        var Tests_1 = JsonHelper.FromJson<Test>(testsFile_1.text);
        Debug.Log(Tests_1);
        var Tests_2 = JsonHelper.FromJson<Test>(testsFile_2.text);
        Debug.Log(Tests_2);
        StartMessages = JsonHelper.FromJson<Message>(startMessagesFile.text);
        FinishMessages = JsonHelper.FromJson<Message>(finishMessagesFile.text);*/
        #endregion
    }

   /* private void OnGUI()
    {
        float fps = 1.0f / Time.deltaTime;
        GUILayout.Label("FPS = " + fps);
    }*/
}
