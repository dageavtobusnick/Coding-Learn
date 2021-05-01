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
    private InterfaceAnimations UIAnimations;
    private GameObject blackScreen;
    private GameData gameData;

    public void OpenTaskExtendedDescription() => StartCoroutine(OpenTaskExtendedDescription_COR());

    public void CloseTaskExtendedDescription() => StartCoroutine(CloseTaskExtendedDescription_COR());

    public void GoToNextLevel() => StartCoroutine(GoToNextLevel_COR());

    public void OpenTaskExtendedDescription_Special() => StartCoroutine(UIAnimations.ShowExtendedTaskPanel_COR());  

    private IEnumerator OpenTaskExtendedDescription_COR()
    {  
        yield return StartCoroutine(UIAnimations.HideTaskPanel_COR());
        StartCoroutine(UIAnimations.ShowExtendedTaskPanel_COR());
    }

    private IEnumerator CloseTaskExtendedDescription_COR()
    {
        yield return StartCoroutine(UIAnimations.HideExtendedTaskPanel_COR());
        if (isTask)
        {
            yield return StartCoroutine(UIAnimations.ShowTaskPanel_COR());
            UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);
        }
        else isTask = true;
    }

    private IEnumerator GoToNextLevel_COR()
    {
        yield return StartCoroutine(UIAnimations.HideExtendedTaskPanel_COR());
        UI.BlackScreen.transform.localScale = new Vector3(1, 1, 1);
        blackScreen.GetComponent<Animator>().Play("AppearBlackScreen");
        yield return new WaitForSeconds(1.4f);
        if (gameData.SceneIndex == 0)
            SceneManager.LoadScene(2);
        SceneManager.LoadScene(gameData.SceneIndex + 1);
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        UIAnimations = Canvas.GetComponent<InterfaceAnimations>();
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
