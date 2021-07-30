using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingScript : MonoBehaviour
{
    public enum PreviousAction
    {
        TargetCall,
        PadCall,
        ExtendedTaskClosing,
        DevModeSwitching
    }

    [Header("Интерфейс")]
    public GameObject Canvas;

    private InterfaceElements UI;
    private GameData gameData;
    private int currentTipNumber;

    #region Вызовы обучающих сообщений
    public void ShowTip_1() => ShowTip(1, "ShowAside");

    public void ShowTip_2() => ShowTip(2, "ShowAside");

    public void ShowTip_3() => ShowTip(3, "ShowAside");

    public void ShowTip_4() => ShowTip(4, "ShowAside");

    public void ShowTip_5() => ShowTip(5, "ShowAside");

    public void ShowTip_6() => ShowTip(6, "ShowAside");

    public void ShowTip_7() => ShowTip(7, "ShowAside");

    public void ShowTip_8() => ShowTip(8, "ShowAside");

    public void ShowTip_9() => ShowTip(9, "ShowAside");

    public void ShowTip_10() => ShowTip(10, "ShowAside");

    public void ShowTip_11() => ShowTip(11, "ShowAside");

    public void ShowTip_12() => ShowTip(12, "ShowAside");

    public void ShowTip_13() => ShowTip(13, "ShowAside");

    public void ShowTip_14() => ShowTip(14, "ShowAside");

    public void ShowTip_15() => ShowTip(15, "ShowAside");

    public void ShowTip_16() => ShowTip(16, "ShowAside");

    public void ShowTip_17() => ShowTip(17, "ShowAside");

    public void ShowTip_18() => ShowTip(18, "ShowAside");

    public void ShowTip_19() => ShowTip(19, "ShowAside");

    public void ShowTip_20() => ShowTip(20, "ShowAside");
    #endregion

    public void TryShowTraining(PreviousAction previousAction)
    {
        switch (previousAction)
        {
            case PreviousAction.ExtendedTaskClosing:
                if (gameData.SceneIndex == 0 && gameData.CurrentTaskNumber == 1)
                    ShowTip_1();
                else if (gameData.SceneIndex == 1 && currentTipNumber == 10)
                    ShowTip_11();
                break;

            case PreviousAction.DevModeSwitching:
                if (gameData.SceneIndex == 0 && gameData.CurrentTaskNumber == 1)
                    ShowTip_5();
                break;

            case PreviousAction.PadCall:
                if (currentTipNumber == 12)
                    ShowTip_13();
                else if (currentTipNumber == 13)
                    ShowTip_14();
                break;

            case PreviousAction.TargetCall:
                if (currentTipNumber == 14)
                    CloseTraining();
                break;
        }
    }

    public void CloseTraining()
    {
        UI.TrainingPanel.transform.GetChild(currentTipNumber - 1).gameObject.SetActive(false);
        UI.TrainingPanel.SetActive(false);
        UI.TrainingPanelBackground.SetActive(false);
        switch (currentTipNumber)
        {
            case 4:
                UI.TaskInfoButton.interactable = true;
                UI.IDEButton.interactable = true;
                UI.HandbookButton.interactable = true;
                break;
            case 10:
                UI.StartButton.interactable = true;
                UI.ResetButton.interactable = true;
                UI.TipButton.gameObject.SetActive(false);
                UI.TipButton.interactable = true;
                UI.ExitDevModeButton.interactable = true;
                break;
            case 14:
                gameData.Player.GetComponent<RobotBehaviour>().UnfreezePlayer();
                break;
        }
    }

    private void ShowTip(int tipNumber, string animationName)
    {
        currentTipNumber = tipNumber;
        UI.TrainingPanel.SetActive(true);
        UI.TrainingPanelBackground.SetActive(true);
        if (tipNumber != 1)
            UI.TrainingPanel.transform.GetChild(tipNumber - 2).gameObject.SetActive(false);
        var newTip = UI.TrainingPanel.transform.GetChild(tipNumber - 1).gameObject;
        newTip.SetActive(true);
        newTip.GetComponentInChildren<Animator>().Play(animationName);
        switch (tipNumber)
        {
            case 1:
                UI.TaskInfoButton.interactable = false;
                UI.IDEButton.interactable = false;
                UI.HandbookButton.interactable = false;
                break;
            case 5:
                UI.StartButton.interactable = false;
                UI.ResetButton.interactable = false;
                UI.TipButton.gameObject.SetActive(true);
                UI.TipButton.interactable = false;
                UI.ExitDevModeButton.interactable = false;
                break;
            case 11:
                gameData.Player.GetComponent<RobotBehaviour>().FreezePlayer();
                UI.TargetPanel.SetActive(false);
                break;
            case 14:
                UI.TargetPanel.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        switch (gameData.SceneIndex)
        {
            case 1:
                currentTipNumber = 10;
                break;
        }
    }
}
