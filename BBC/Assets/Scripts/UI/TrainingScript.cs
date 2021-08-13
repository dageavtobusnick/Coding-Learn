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

    [Header("Èםעונפויס")]
    public GameObject Canvas;

    private InterfaceElements UI;
    private GameData gameData;
    private int currentTipNumber;
    private const string pointerAnimationName = "ShowAside";

    public void ShowTip(int tipNumber)
    {
        currentTipNumber = tipNumber;
        UI.TrainingPanel.SetActive(true);
        UI.TrainingPanelBackground.SetActive(true);
        if (tipNumber != 1)
            UI.TrainingPanel.transform.GetChild(tipNumber - 2).gameObject.SetActive(false);
        var newTip = UI.TrainingPanel.transform.GetChild(tipNumber - 1).gameObject;
        newTip.SetActive(true);
        newTip.GetComponentInChildren<Animator>().Play(pointerAnimationName);
        switch (tipNumber)
        {
            case 1:
                UI.TaskInfoButton.interactable = false;
                UI.ShowIDEButton.interactable = false;
                UI.ShowHandbookButton.interactable = false;
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

    public void TryShowTraining(PreviousAction previousAction)
    {
        switch (previousAction)
        {
            case PreviousAction.ExtendedTaskClosing:
                if (gameData.SceneIndex == 0 && gameData.CurrentTaskNumber == 1)
                    ShowTip(1);
                else if (gameData.SceneIndex == 1 && currentTipNumber == 10)
                    ShowTip(11);
                break;

            case PreviousAction.DevModeSwitching:
                if (gameData.SceneIndex == 0 && gameData.CurrentTaskNumber == 1)
                    ShowTip(5);
                break;

            case PreviousAction.PadCall:
                if (currentTipNumber == 12)
                    ShowTip(13);
                else if (currentTipNumber == 13)
                    ShowTip(14);
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
                UI.ShowIDEButton.interactable = true;
                UI.ShowHandbookButton.interactable = true;
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
