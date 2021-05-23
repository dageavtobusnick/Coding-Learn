using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAnimations : MonoBehaviour
{
    public GameObject ActivateTaskButtonBackground;
    public GameObject ChangeSceneButtonBackground;
    public GameObject ScenarioButtonBackground;
    public GameObject TaskPanelBackground;
    public GameObject ExtendedTaskPanelBackground;
    public GameObject PadBackground;
    public GameObject Canvas;

    private InterfaceElements UI;

    public IEnumerator ShowTaskPanel_COR()
    {
        UI.TaskDescriptionScrollbar.value = 1;
        TaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("DrawBackground");
        PadBackground.transform.GetChild(0).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        TaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("DrawBackground");
        PadBackground.transform.GetChild(1).GetComponent<Animator>().Play("DrawBackground");
        TaskPanelBackground.transform.GetChild(2).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
        UI.Pad.transform.parent.gameObject.GetComponent<Animator>().Play("MoveLeft_Pad");
        yield return new WaitForSeconds(0.7f);
    }

    public IEnumerator HideTaskPanel_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        UI.Pad.transform.parent.gameObject.GetComponent<Animator>().Play("MoveRight_Pad");
        yield return new WaitForSeconds(0.7f);
        PadBackground.transform.GetChild(1).GetComponent<Animator>().Play("EraseBackground");
        TaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("EraseBackground");
        TaskPanelBackground.transform.GetChild(2).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
        TaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("EraseBackground");
        PadBackground.transform.GetChild(0).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator ShowExtendedTaskPanel_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.ExtendedTaskDescriptionScrollbar.value = 1;
        ExtendedTaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        ExtendedTaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveUp_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
    }

    public IEnumerator HideExtendedTaskPanel_COR()
    {
        UI.ExtendedTaskPanel.GetComponent<Animator>().Play("MoveDown_TaskPanel_Extended");
        yield return new WaitForSeconds(0.7f);
        ExtendedTaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
        ExtendedTaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator ShowActivateTaskButton_COR() => ShowButton_COR(UI.ActivateTaskButton, ActivateTaskButtonBackground);

    public IEnumerator HideActivateTaskButton_COR() => HideButton_COR(UI.ActivateTaskButton, ActivateTaskButtonBackground);

    public IEnumerator ShowChangeSceneButton_COR() => ShowButton_COR(UI.ChangeSceneButton, ChangeSceneButtonBackground);

    public IEnumerator HideChangeSceneButton_COR() => HideButton_COR(UI.ChangeSceneButton, ChangeSceneButtonBackground);

    public IEnumerator ShowScenarioButton_COR() => ShowButton_COR(UI.ScenarioButton, ScenarioButtonBackground);

    public IEnumerator HideScenarioButton_COR() => HideButton_COR(UI.ScenarioButton, ScenarioButtonBackground);

    public IEnumerator ShowButton_COR(Button button, GameObject buttonBackground)
    {
        buttonBackground.GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        button.GetComponent<Animator>().Play("ScaleInterfaceUp");
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator HideButton_COR(Button button, GameObject buttonBackground)
    {
        button.GetComponent<Animator>().Play("CollapseInterface");
        yield return new WaitForSeconds(0.15f);
        buttonBackground.GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
    }

    private void Awake()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
    }
}
