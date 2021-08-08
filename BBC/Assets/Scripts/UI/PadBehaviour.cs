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
    [Header("Стоимость одной подсказки")]
    public int TipPrice = 3;
    [Header("Стоимость нескольких подсказок")]
    public int ManyTipsPrice = 8;
    [Header("Первая заблокированная тема")]
    [Tooltip("Начиная с этого номера, все темы в справочнике будут заблокированы")]
    public int FirstThemeToLockNumber;
    [HideInInspector]
    public string StartCode;
    [HideInInspector]
    public PadMode Mode;   
    [HideInInspector]
    public List<int> AvailableTipsCounts;
    [HideInInspector]
    public bool IsPadCalled;
    [HideInInspector]
    public bool IsCallAvailable;

    private InterfaceElements UI;
    private RobotBehaviour robotBehaviour;
    private GameData gameData;
    private int themeNumber;
    private int taskNumber;

    public void ResetCode() => UI.CodeField.text = StartCode;

    public void SwitchToDevMode() => StartCoroutine(SwitchToDevMode_COR());
    
    public void SwitchToHandbookMode() => StartCoroutine(SwitchToHandbookMode_COR());

    public void ReturnToMenuFromDevMode() => StartCoroutine(ReturnToMenuFromDevMode_COR());

    public void ReturnToMenuFromHandbookMode() => StartCoroutine(ReturnToMenuFromHandbookMode_COR());

    #region Выбор темы из главного меню справочника

    public void ShowSubThemes_Theme_1_1() => StartCoroutine(ShowSubThemes_COR(1));

    public void ShowSubThemes_Theme_1_2() => StartCoroutine(ShowSubThemes_COR(2));

    public void ShowSubThemes_Theme_2() => StartCoroutine(ShowSubThemes_COR(3));

    public void ShowSubThemes_Theme_3() => StartCoroutine(ShowSubThemes_COR(4));

    public void ShowSubThemes_Theme_4() => StartCoroutine(ShowSubThemes_COR(5));

    public void ShowSubThemes_Theme_5() => StartCoroutine(ShowSubThemes_COR(6));

    #endregion

    #region Выбор раздела в каждой из тем, представленных в справочнике

    public void ShowProgrammingInfo_SubTheme_1() => StartCoroutine(ShowProgrammingInfo_COR(1));

    public void ShowProgrammingInfo_SubTheme_2() => StartCoroutine(ShowProgrammingInfo_COR(2));

    public void ShowProgrammingInfo_SubTheme_3() => StartCoroutine(ShowProgrammingInfo_COR(3));

    public void ShowProgrammingInfo_SubTheme_4() => StartCoroutine(ShowProgrammingInfo_COR(4));

    public void ShowProgrammingInfo_SubTheme_5() => StartCoroutine(ShowProgrammingInfo_COR(5));

    public void ShowProgrammingInfo_SubTheme_6() => StartCoroutine(ShowProgrammingInfo_COR(6));

    public void ShowProgrammingInfo_SubTheme_7() => StartCoroutine(ShowProgrammingInfo_COR(7));

    public void ShowProgrammingInfo_SubTheme_8() => StartCoroutine(ShowProgrammingInfo_COR(8));

    public void ShowProgrammingInfo_SubTheme_9() => StartCoroutine(ShowProgrammingInfo_COR(9));

    #endregion

    public void ShowHelpPanel()
    {
        taskNumber = gameData.CurrentTaskNumber;
        UpdatePadData();
        if (AvailableTipsCounts[taskNumber - 1] == gameData.Tips[taskNumber - 1].Length)
            UI.Tip.text = "";
        else
        {
            var tipNumber = gameData.Tips[taskNumber - 1].Length - AvailableTipsCounts[taskNumber - 1] - 1;
            UI.Tip.text = gameData.Tips[taskNumber - 1][tipNumber].Tip;
        }
        UI.HelpPanel.GetComponent<Animator>().Play("ScaleUp"); 
    }

    public void CloseHelpPanel() => UI.HelpPanel.GetComponent<Animator>().Play("ScaleDown");

    public void ReturnToPreviousPage() => StartCoroutine(ReturnToPreviousPage_COR());

    public void UnlockProgrammingInfo(int chapterNumber)
    {
        var buttonToUnlock = UI.SubThemeButtons.transform.GetChild(FirstThemeToLockNumber - 2).GetChild(chapterNumber - 1).gameObject.GetComponent<Button>();
        buttonToUnlock.interactable = true;
        buttonToUnlock.GetComponentInChildren<Text>().text = gameData.HandbookLetters[FirstThemeToLockNumber - 1][chapterNumber - 1].Title;
    }

    public void ShowTip()
    {
        gameData.TipsCount--;
        if (UI.TipFiller.IsActive())
            UI.TipFiller.gameObject.SetActive(false);
        var tipNumber = gameData.Tips[taskNumber - 1].Length - AvailableTipsCounts[taskNumber - 1];
        UI.Tip.text = gameData.Tips[taskNumber - 1][tipNumber].Tip;
        AvailableTipsCounts[taskNumber - 1]--;
        UpdatePadData();
    }

    public void BuyTip()
    {
        gameData.TipsCount++;
        gameData.CoinsCount -= 3;
        UpdatePadData();
    }

    public void BuyManyTips()
    {
        gameData.TipsCount += 3;
        gameData.CoinsCount -= 8;
        UpdatePadData();
    }

    private IEnumerator ShowSubThemes_COR(int themeNumber)
    {
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(true);
        this.themeNumber = themeNumber;
        Mode = PadMode.Handbook_SubThemes;
    }

    private IEnumerator ShowProgrammingInfo_COR(int subThemeNumber)
    {
        var handbookLetter = gameData.HandbookLetters[themeNumber - 1][subThemeNumber - 1];
        UI.ProgrammingInfo.text = handbookLetter.Description;
        UI.ProgrammingInfoTitle.text = handbookLetter.Title;
        UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleDown");
        yield return new WaitForSeconds(0.45f);
        UI.ProgrammingInfoScrollBar.value = 1;
        UI.InfoPanel_BlackScreen.GetComponent<Animator>().Play("ShowProgrammingInfo");
        yield return new WaitForSeconds(1f);
        UI.InfoPanel_BlackScreen.SetActive(false);
        Mode = PadMode.Handbook_ProgrammingInfo;
    }

    private IEnumerator SwitchToDevMode_COR()
    {
        UI.Pad.GetComponentInParent<Animator>().Play("SwitchToDevMode");
        Mode = PadMode.Development;
        yield return new WaitForSeconds(1.5f);
        UI.GetComponent<TrainingScript>().TryShowTraining(TrainingScript.PreviousAction.DevModeSwitching);
    }    

    private IEnumerator SwitchToHandbookMode_COR()
    {
        if (Mode == PadMode.Normal)
            UI.HideUI();
        UI.CloseTaskButton.transform.localScale = new Vector3(0, 0, 0);
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
        if (gameData.IsTaskStarted)
        {
            UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToHandbookMode");
            UI.TaskPanel.GetComponent<Animator>().Play("MoveLeft_TaskPanel");
            yield return new WaitForSeconds(0.7f);
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().EraseTaskPanelBackground_COR());
        }
        else UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToHandbookMode_NoLatency");
        Mode = PadMode.Handbook_MainThemes;
    }

    private IEnumerator ReturnToMenuFromDevMode_COR()
    {
        UI.Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromDevMode");
        yield return new WaitForSeconds(1f);
        UI.HelpPanel.GetComponent<Animator>().Play("ScaleDown_Quick");
        Mode = PadMode.Normal;
    }

    private IEnumerator ReturnToMenuFromHandbookMode_COR()
    {
        if (gameData.IsTaskStarted)
            UI.Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromHandbookMode");
        else UI.Pad.GetComponentInParent<Animator>().Play("ReturnToMenuFromHandbookMode_NoLatency");
        yield return new WaitForSeconds(0.83f);
        UI.ThemeButtons.GetComponent<Animator>().Play("ScaleUp");
        for (var i = 0; i < UI.SubThemeButtons.transform.childCount; i++)
            UI.SubThemeButtons.transform.GetChild(i).gameObject.GetComponent<Animator>().Play("ScaleDown");
        UI.InfoPanel_BlackScreen.SetActive(true);
        UI.InfoPanel_BlackScreen.GetComponent<Animator>().Play("HideProgrammingInfo");
        if (gameData.IsTaskStarted)
        {
            yield return StartCoroutine(Canvas.GetComponent<InterfaceAnimations>().DrawTaskPanelBackground_COR());
            UI.TaskPanel.GetComponent<Animator>().Play("MoveRight_TaskPanel");
            yield return new WaitForSeconds(0.7f);
            UI.CloseTaskButton.transform.localScale = new Vector3(1, 1, 1);
        }
        else UI.ShowUI();
        Mode = PadMode.Normal;
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
                UI.InfoPanel_BlackScreen.SetActive(true);
                UI.InfoPanel_BlackScreen.GetComponent<Animator>().Play("HideProgrammingInfo");
                yield return new WaitForSeconds(1f);              
                UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject.GetComponent<Animator>().Play("ScaleUp");
                Mode = PadMode.Handbook_SubThemes;
                break;
        }
    }  

    private void LockThemes()
    {       
        for (var i = FirstThemeToLockNumber; i < UI.ThemeButtons.transform.childCount; i++)
        {
            var themeButton = UI.ThemeButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            themeButton.interactable = false;
            themeButton.GetComponentInChildren<Text>().text = "???";
        }
        var newProgrammingInfoButtons = UI.SubThemeButtons.transform.GetChild(FirstThemeToLockNumber - 1).gameObject;
        for (var i = 0; i < newProgrammingInfoButtons.transform.childCount; i++)
        {
            var programmingInfoButton = newProgrammingInfoButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
            programmingInfoButton.interactable = false;
            programmingInfoButton.GetComponentInChildren<Text>().text = "???";
        }
        for (var i = 0; i < FirstThemeToLockNumber - 1; i++)
        {
            for (var j = 0; j < UI.SubThemeButtons.transform.GetChild(i).childCount; j++)
                UI.SubThemeButtons.transform.GetChild(i).GetChild(j).GetComponentInChildren<Text>().text = gameData.HandbookLetters[i][j].Title;
        }
    }

    private IEnumerator CallPad()
    {
        if (!IsPadCalled)
        {
            robotBehaviour.FreezePlayer();
            UI.Pad.GetComponentInParent<Animator>().Play("MoveLeft_Pad"); 
            IsPadCalled = !IsPadCalled;
        }
        else
        {
            if (Mode == PadMode.Normal)
            {
                robotBehaviour.UnfreezePlayer();
                UI.Pad.GetComponentInParent<Animator>().Play("MoveRight_Pad");
                IsPadCalled = !IsPadCalled;
            }
        }
        yield return new WaitForSeconds(0.667f);
        UI.GetComponent<TrainingScript>().TryShowTraining(TrainingScript.PreviousAction.PadCall);
    }

    private void UpdatePadData()
    {
        UI.CoinsCounter.text = UI.CoinsMenuCounter.text = gameData.CoinsCount.ToString();
        UI.TipsCounter.text = UI.TipsMenuCounter.text = gameData.TipsCount.ToString();
        UI.BuyTipButton.interactable = gameData.CoinsCount >= TipPrice;
        UI.BuyManyTipsButton.interactable = gameData.CoinsCount >= ManyTipsPrice;
        if (gameData.SceneIndex > 0 && taskNumber > 0 && taskNumber < AvailableTipsCounts.Count)
            UI.ShowTipButton.GetComponentInChildren<Text>().text = "Получить подсказку (Осталось: " + AvailableTipsCounts[taskNumber - 1] + ")";
        UI.ShowTipButton.interactable = gameData.TipsCount > 0 && AvailableTipsCounts[taskNumber - 1] > 0;
    }

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.P) && IsCallAvailable)
            StartCoroutine(CallPad());
        UpdatePadData();
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        Mode = PadMode.Normal;
        IsPadCalled = false;
        IsCallAvailable = true;
        UI.IDEButton.interactable = gameData.SceneIndex == 0;
        //LockThemes();
        UpdatePadData();
        AvailableTipsCounts = new List<int>();
        if (gameData.SceneIndex > 0)
        {
            for (var i = 0; i < gameData.TaskTexts.Length; i++)
                AvailableTipsCounts.Add(gameData.Tips[i].Length);
        }
        if (PlayerPrefs.HasKey("PositionX"))
        {
            for (var i = 0; i < AvailableTipsCounts.Count; i++)
                AvailableTipsCounts[i] = PlayerPrefs.GetInt("Available Tips Count (Task " + (i + 1) + ")");
        }
        if (gameData.SceneIndex != 0 && gameData.SceneIndex <= 4)
            Canvas.GetComponent<SaveLoad>().Save();
    }
}
