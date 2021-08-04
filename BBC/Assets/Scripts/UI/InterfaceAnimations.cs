using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceAnimations : MonoBehaviour
{
    public GameObject ActionButtonBackground;
    public GameObject TaskPanelBackground;
    public GameObject ExtendedTaskPanelBackground;
    public GameObject PadBackground;
    public GameObject Canvas;

    private InterfaceElements UI;

    public IEnumerator ShowTaskPanel_COR()
    {
        UI.TaskDescriptionScrollbar.value = 1;
        yield return StartCoroutine(DrawTaskPanelBackground_COR());
        UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
        PlayPadMoveAnimation("MoveLeft_Pad", "MoveLeft_Pad_DevMode");
        yield return new WaitForSeconds(0.7f);
    }

    public IEnumerator HideTaskPanel_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        PlayPadMoveAnimation("MoveRight_Pad", "MoveRight_Pad_DevMode");
        yield return new WaitForSeconds(0.7f);
        UI.HelpPanel.GetComponent<Animator>().Play("ScaleDown_Quick");
        yield return StartCoroutine(EraseTaskPanelBackground_COR());
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

    public IEnumerator ShowActionButton_COR() => ShowButton_COR(UI.ActionButton, ActionButtonBackground);

    public IEnumerator HideActionButton_COR() => HideButton_COR(UI.ActionButton, ActionButtonBackground);

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

    public IEnumerator DrawTaskPanelBackground_COR()
    {
        TaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("DrawBackground");
        PadBackground.transform.GetChild(0).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
        TaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("DrawBackground");
        PadBackground.transform.GetChild(1).GetComponent<Animator>().Play("DrawBackground");
        TaskPanelBackground.transform.GetChild(2).GetComponent<Animator>().Play("DrawBackground");
        yield return new WaitForSeconds(0.15f);
    }

    public IEnumerator EraseTaskPanelBackground_COR()
    {
        PadBackground.transform.GetChild(1).GetComponent<Animator>().Play("EraseBackground");
        TaskPanelBackground.transform.GetChild(1).GetComponent<Animator>().Play("EraseBackground");
        TaskPanelBackground.transform.GetChild(2).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
        TaskPanelBackground.transform.GetChild(0).GetComponent<Animator>().Play("EraseBackground");
        PadBackground.transform.GetChild(0).GetComponent<Animator>().Play("EraseBackground");
        yield return new WaitForSeconds(0.15f);
    }

    private void PlayPadMoveAnimation(string normalAnimation, string devAnimation)
    {
        var padMode = UI.Pad.GetComponent<PadBehaviour>().Mode;
        var padAnimator = UI.Pad.GetComponentInParent<Animator>();
        if (padMode == PadBehaviour.PadMode.Normal)
            padAnimator.Play(normalAnimation);
        else if (padMode == PadBehaviour.PadMode.Development)
            padAnimator.Play(devAnimation);
    }

    private void Awake()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
    }
}
