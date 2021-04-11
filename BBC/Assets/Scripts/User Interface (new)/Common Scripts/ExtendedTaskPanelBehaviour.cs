using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExtendedTaskPanelBehaviour : MonoBehaviour
{
    public bool isTask = true;
    private int sceneIndex;
    private Vector3 taskPanelPosition;
    private Vector3 extendedTaskPanelPosition;
    private GameObject player;
    private GameObject UICollector;
    private GameObject pad;
    private GameObject taskPanel;
    private GameObject extendedTaskPanel;
    private GameObject canvas;
    private Scrollbar scrollbar;

    public void OpenTaskExtendedDescription()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        taskPanel.transform.position = UICollector.transform.position;
        pad.transform.position = UICollector.transform.position;
        extendedTaskPanel.transform.position = extendedTaskPanelPosition;
        scrollbar.value = 1;
    }

    public void CloseTaskExtendedDescription()
    {
        player.GetComponent<Rigidbody>().constraints = ~RigidbodyConstraints.FreezePosition;
        extendedTaskPanel.transform.position = UICollector.transform.position;
        if (isTask)
        {
            taskPanel.transform.position = taskPanelPosition;
            pad.transform.position = pad.GetComponent<PadBehaviour>().padPosition;
        }
        else isTask = true;
    }

    public void GoToNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch (currentSceneIndex)
        {
            case 1:
                SceneManager.LoadScene(2);
                break;
            case 2:
                SceneManager.LoadScene(3);
                break;
            case 3:
                SceneManager.LoadScene(4);
                break;
            case 4:
                SceneManager.LoadScene(5);
                break;
            case 6:
                SceneManager.LoadScene(1);
                break;
        }
    }

    private void Start()
    {
        player = GameObject.Find("robot1");
        pad = GameObject.Find("Pad");
        taskPanel = GameObject.Find("TaskPanel");
        extendedTaskPanel = GameObject.Find("TaskPanel_Extended");
        UICollector = GameObject.Find("UI_Collector");
        canvas = GameObject.Find("Canvas");
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        taskPanelPosition = taskPanel.transform.position;
        taskPanel.transform.position = UICollector.transform.position;
        pad.GetComponent<PadBehaviour>().padPosition = pad.transform.position;
        pad.transform.position = UICollector.transform.position;
        extendedTaskPanelPosition = extendedTaskPanel.transform.position;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(sceneIndex)
        {
            case 1:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_1();
                break;
            case 2:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_2();
                break;
            case 3:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_3();
                break;
            case 4:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_4();
                break;
            case 5:
                canvas.GetComponent<TaskPanelBehaviour>().ShowIntroduction_Level_5();
                break;
        }
        OpenTaskExtendedDescription();
        isTask = sceneIndex == 6 ? true : false;
    }
}
