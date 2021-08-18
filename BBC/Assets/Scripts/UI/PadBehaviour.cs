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
    [Header("Количество доступных тем в справочнике")]
    public int AvailableThemesCount;
    [Header("Стоимость одной подсказки")]
    public int TipPrice = 3;
    [Header("Стоимость нескольких подсказок")]
    public int ManyTipsPrice = 8;
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

    public void OpenSubThemesList(int mainThemeNumber) => StartCoroutine(OpenSubThemesList_COR(mainThemeNumber));

    public void OpenSubTheme(int subThemeNumber) => StartCoroutine(OpenSubTheme_COR(subThemeNumber));

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
        var buttonToUnlock = UI.SubThemeButtons.transform.GetChild(AvailableThemesCount - 1).GetChild(chapterNumber - 1).gameObject.GetComponent<Button>();
        buttonToUnlock.interactable = true;
        buttonToUnlock.GetComponentInChildren<Text>().text = gameData.HandbookLetters[AvailableThemesCount - 1][chapterNumber - 1].Title;
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
        gameData.CoinsCount -= TipPrice;
        UpdatePadData();
    }

    public void BuyManyTips()
    {
        gameData.TipsCount += 3;
        gameData.CoinsCount -= ManyTipsPrice;
        UpdatePadData();
    }

    public void UpdatePadData()
    {
        UI.CoinsCounter.text = UI.CoinsMenuCounter.text = gameData.CoinsCount.ToString();
        UI.TipsCounter.text = UI.TipsMenuCounter.text = gameData.TipsCount.ToString();
        UI.BuyTipButton.interactable = gameData.CoinsCount >= TipPrice;
        UI.BuyManyTipsButton.interactable = gameData.CoinsCount >= ManyTipsPrice;
        if (gameData.SceneIndex > 0 && taskNumber > 0 && taskNumber < AvailableTipsCounts.Count)
            UI.ShowTipButton.GetComponentInChildren<Text>().text = "Получить подсказку (Осталось: " + AvailableTipsCounts[taskNumber - 1] + ")";
        UI.ShowTipButton.interactable = gameData.TipsCount > 0 && AvailableTipsCounts[taskNumber - 1] > 0;
    }

    private IEnumerator OpenSubThemesList_COR(int themeNumber)
    {
        this.themeNumber = themeNumber;
        UI.ThemeButtonsContainer.GetComponent<Animator>().Play("MoveButtons_MiddleToLeft");
        yield return new WaitForSeconds(0.583f);
        UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(true);
        yield return StartCoroutine(MoveSubThemeButtons_COR(true, "MoveButtons_RightToMiddle"));    
        Mode = PadMode.Handbook_SubThemes;
    }

    private IEnumerator OpenSubTheme_COR(int subThemeNumber)
    {
        var handbookLetter = gameData.HandbookLetters[themeNumber - 1][subThemeNumber - 1];
        UI.ProgrammingInfo.text = handbookLetter.Description;
        UI.ProgrammingInfoTitle.text = handbookLetter.Title;
        yield return StartCoroutine(MoveSubThemeButtons_COR(false, "MoveButtons_MiddleToLeft"));
        UI.HandbookButtons.SetActive(false);
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
        else
        {
            UI.Pad.transform.parent.parent.gameObject.GetComponent<Animator>().Play("SwitchToHandbookMode_NoLatency");
            yield return new WaitForSeconds(1.5f);
        }
        Mode = PadMode.Handbook_MainThemes;
        if (UI.ThemeButtonsContainer.GetComponentInChildren<Scrollbar>() != null)
            UI.ThemeButtonsContainer.GetComponentInChildren<Scrollbar>().value = 1;
        UI.ThemeButtonsContainer.GetComponent<Animator>().Play("MoveButtons_LeftToMiddle");
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
        UI.HandbookButtons.SetActive(true);
        for (var i = 0; i < UI.SubThemeButtons.transform.childCount; i++)
            UI.SubThemeButtons.transform.GetChild(i).gameObject.SetActive(false);
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
                yield return StartCoroutine(MoveSubThemeButtons_COR(false, "MoveButtons_MiddleToRight"));
                UI.ThemeButtonsContainer.GetComponent<Animator>().Play("MoveButtons_LeftToMiddle");
                UI.PreviousHandbookPageButton.transform.parent.gameObject.SetActive(false);
                Mode = PadMode.Handbook_MainThemes;
                break;
            case PadMode.Handbook_ProgrammingInfo:
                UI.InfoPanel_BlackScreen.SetActive(true);
                UI.InfoPanel_BlackScreen.GetComponent<Animator>().Play("HideProgrammingInfo");
                yield return new WaitForSeconds(1f);
                UI.HandbookButtons.SetActive(true);
                yield return StartCoroutine(MoveSubThemeButtons_COR(true, "MoveButtons_LeftToMiddle"));
                Mode = PadMode.Handbook_SubThemes;
                break;
        }
    }  

    private IEnumerator MoveSubThemeButtons_COR(bool willBeShown, string animation)
    {
        var subThemesList = UI.SubThemeButtons.transform.GetChild(themeNumber - 1).gameObject;
        if (willBeShown)
        {
            subThemesList.SetActive(true);
            subThemesList.GetComponent<Animator>().Play(animation);
            yield return new WaitForSeconds(0.583f);
        }
        else
        {
            subThemesList.GetComponent<Animator>().Play(animation);
            yield return new WaitForSeconds(0.583f);
            subThemesList.SetActive(false);
        }
    }

    private void FillHandbook()
    {
        var themeButtons = UI.ThemeButtonsContainer.transform.GetChild(0).GetChild(0);
        var subThemeButtons = UI.SubThemeButtons;
        var themeButtonPrefab = UI.ThemeButtonPrefab;
        var subThemeButtonsContainerPrefab = UI.SubThemeButtonsContainerPrefab;
        for (var i = 0; i < AvailableThemesCount; i++)
        {
            var themeButton = Instantiate(themeButtonPrefab, themeButtons);
            var themeNumber = i + 1;
            themeButton.GetComponentInChildren<Text>().text = gameData.ThemeTitles[i].Title;
            themeButton.GetComponent<Button>().onClick.AddListener(() => OpenSubThemesList(themeNumber));

            var subThemeContainer = Instantiate(subThemeButtonsContainerPrefab, subThemeButtons.transform);
            var parentPosition = subThemeButtons.transform.position;
            for (var j = 0; j < gameData.HandbookLetters[i].Length; j++)
            {
                var subThemeButton = Instantiate(themeButtonPrefab, subThemeContainer.transform.GetChild(0).GetChild(0));
                var subThemeNumber = j + 1;
                if (i == AvailableThemesCount - 1)
                {
                    subThemeButton.GetComponentInChildren<Text>().text = "???";
                    subThemeButton.GetComponent<Button>().interactable = false;
                }
                else subThemeButton.GetComponentInChildren<Text>().text = gameData.HandbookLetters[i][j].Title;
                subThemeButton.GetComponent<Button>().onClick.AddListener(() => OpenSubTheme(subThemeNumber));
            }
            subThemeContainer.SetActive(false);
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

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.P) && IsCallAvailable)
            StartCoroutine(CallPad());
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        robotBehaviour = gameData.Player.GetComponent<RobotBehaviour>();
        Mode = PadMode.Normal;
        IsPadCalled = false;
        IsCallAvailable = true;
        UI.ShowIDEButton.interactable = gameData.SceneIndex == 0;
        FillHandbook();
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
