using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PadBehaviour : MonoBehaviour
{
    public enum PadMode
    {
        Normal,
        Development,
        Handbook_MainThemes,
        Handbook_SubThemes,
        Handbook_ProgrammingInfo
    }

    [Header("Интерфейс")]
    public GameObject Canvas;

    [HideInInspector]
    public string StartCode;

    [HideInInspector]
    public PadMode Mode;

    private InterfaceElements UI;
    private int themeNumber;

    public void ResetCode() => UI.CodeField.text = StartCode;

    public void SwitchToDevMode()
    {
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToDevMode");
        Mode = PadMode.Development;
    }

    public void ReturnToMenuFromDevMode()
    {
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ReturnToMenuFromDevMode");
        Mode = PadMode.Normal;
    }

    public void SwitchToHandbookMode() => StartCoroutine(SwitchToHandbookMode_COR());

    public void ReturnToMenuFromHandbookMode() => StartCoroutine(ReturnToMenuFromHandbookMode_COR());

    public void ReturnToPreviousPage() => StartCoroutine(ReturnToPreviousPage_COR());

    public void ShowSubThemes_Theme_1() => StartCoroutine(ShowSubThemes(1));

    public void ShowSubThemes_Theme_2() => StartCoroutine(ShowSubThemes(2));

    public void ShowSubThemes_Theme_3() => StartCoroutine(ShowSubThemes(3));

    public void ShowProgrammingInfo() => StartCoroutine(ShowProgrammingInfo_COR()); // Сделать для каждого подраздела

    private IEnumerator ShowSubThemes(int themeNumber)
    {
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(true);
        this.themeNumber = themeNumber;
        Mode = PadMode.Handbook_SubThemes;
    }

    private IEnumerator ShowProgrammingInfo_COR()
    {
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.ProgrammingInfo.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ScaleUp");
        Mode = PadMode.Handbook_ProgrammingInfo;
    }

    private IEnumerator ReturnToPreviousPage_COR()
    {
        switch (Mode)
        {
            case PadMode.Handbook_SubThemes:
                UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleDown");
                yield return new WaitForSeconds(0.45f);
                UI.ThemeButtons.GetComponent<Animator>().Play("ScaleUp");
                UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
                Mode = PadMode.Handbook_MainThemes;
                break;
            case PadMode.Handbook_ProgrammingInfo:
                UI.ProgrammingInfo.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ScaleDown");
                yield return new WaitForSeconds(0.45f);
                UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
                Mode = PadMode.Handbook_SubThemes;
                break;
        }
    }

    private IEnumerator SwitchToHandbookMode_COR()
    {
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToHandbookMode");
        UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().EraseTaskPanelBackground_COR());       
        Mode = PadMode.Handbook_MainThemes;
    }

    private IEnumerator ReturnToMenuFromHandbookMode_COR()
    {
        UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ReturnToMenuFromHandbookMode");
        yield return new WaitForSeconds(0.83f);
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleUp");
        for (var i = 0; i < UI.SubThemeButtons.transform.childCount; i++)
            UI.SubThemeButtons.transform.GetChild(i).gameObject.GetComponent<Animator>().Play("ScaleDown");
        UI.ProgrammingInfo.transform.parent.parent.gameObject.GetComponent<Animator>().Play("ScaleDown");
        yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().DrawTaskPanelBackground_COR());
        UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
        yield return new WaitForSeconds(0.7f);
        UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);     
        Mode = PadMode.Normal;
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        Mode = PadMode.Normal;
    }
}
