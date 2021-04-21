using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExtendedTaskPanelBehaviour : MonoBehaviour
{
    [Header ("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public bool isTask = true;

    private InterfaceElements UI;
    private GameObject blackScreen;
    private GameData gameData;

    public void OpenTaskExtendedDescription() => StartCoroutine(OpenTaskExtendedDescription_COR());

    public void CloseTaskExtendedDescription() => StartCoroutine(CloseTaskExtendedDescription_COR());

    public void GoToNextLevel() => StartCoroutine(GoToNextLevel_COR());

    public void OpenTaskExtendedDescription_Special()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.ExtendedTaskDescriptionScrollbar.value = 1;
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");  
    }

    private IEnumerator OpenTaskExtendedDescription_COR()
    {
        UI.ExtendedTaskDescriptionScrollbar.value = 1;
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        UI.Pad.transform.parent.gameObject.GetComponent<Animator>().Play("MoveRight_Pad");
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");
        yield break;
    }

    private IEnumerator CloseTaskExtendedDescription_COR()
    {  
        if (isTask)
        {
            UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
            UI.Pad.transform.parent.gameObject.GetComponent<Animator>().Play("MoveLeft_Pad");
        }
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        if (isTask)
            UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);
        else isTask = true;
    }

    private IEnumerator GoToNextLevel_COR()
    {
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        GameObject.Find("BlackScreen_Container").transform.localScale = new Vector3(1, 1, 1);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (gameData.SceneIndex == SceneManager.sceneCountInBuildSettings - 1)
            SceneManager.LoadScene(2);
        SceneManager.LoadScene(gameData.SceneIndex + 1);
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        blackScreen = UI.BlackScreen.transform.GetChild(0).gameObject;
        if (gameData.SceneIndex == 0)
        {
            Canvas.GetComponent<GameData>().currentTaskNumber = 1;
            UI.ActivateTaskButton.GetComponent<ActivateTaskButtonBehaviour>().ActivateTask();
        }
        else
        {
            var startMessage = gameData.StartMessages[gameData.SceneIndex - 1];
            UI.ExtendedTaskTitle.text = startMessage.Title;
            UI.ExtendedTaskDescription.text = startMessage.Description;
        }
        OpenTaskExtendedDescription_Special();
        isTask = gameData.SceneIndex == 0 ? true : false;
    }
}
