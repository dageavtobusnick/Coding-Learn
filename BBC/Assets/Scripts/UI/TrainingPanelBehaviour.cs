using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingPanelBehaviour : MonoBehaviour
{
    [Header("Панель обучения")]
    public GameObject TrainingPanel;
    public GameObject TrainingPanelBackground;

    [Header("Элементы UI для переключения состояния")]
    [SerializeField] private PadDevelopmentBehaviour Pad_Dev;

    private GameManager gameManager;
    private UIManager uiManager;
    private int currentTipNumber;
    private const string pointerAnimationName = "ShowAside";

    public void ShowTip(int tipNumber)
    {
        currentTipNumber = tipNumber;
        TrainingPanel.SetActive(true);
        TrainingPanelBackground.SetActive(true);
        if (tipNumber != 1)
            TrainingPanel.transform.GetChild(tipNumber - 2).gameObject.SetActive(false);
        var newTip = TrainingPanel.transform.GetChild(tipNumber - 1).gameObject;
        newTip.SetActive(true);
        newTip.GetComponentInChildren<Animator>().Play(pointerAnimationName);
        switch (tipNumber)
        {
            case 1:
                uiManager.TaskPanelBehaviour.TaskInfoButton.interactable = false;
                uiManager.PadMenuBehaviour.ShowIDEButton.interactable = false;
                uiManager.PadMenuBehaviour.ShowHandbookButton.interactable = false;
                break;
            case 5:
                Pad_Dev.StartButton.interactable = false;
                Pad_Dev.ResetButton.interactable = false;
                Pad_Dev.TipButton.gameObject.SetActive(true);
                Pad_Dev.TipButton.interactable = false;
                Pad_Dev.ExitDevModeButton.interactable = false;
                break;
            case 11:
                gameManager.Player.GetComponent<PlayerBehaviour>().FreezePlayer();
                uiManager.TargetPanelBehaviour.IsCallAvailable = false;
                break;
            case 14:
                uiManager.TargetPanelBehaviour.IsCallAvailable = true;
                break;
        }
    }

    public void TryShowTraining(PreviousAction previousAction)
    {
        switch (previousAction)
        {
            case PreviousAction.ExtendedTaskClosing:
                if (gameManager.SceneIndex == 0 && gameManager.CurrentTaskNumber == 1)
                    ShowTip(1);
                else if (gameManager.SceneIndex == 1 && currentTipNumber == 10)
                    ShowTip(11);
                break;

            case PreviousAction.DevModeSwitching:
                if (gameManager.SceneIndex == 0 && gameManager.CurrentTaskNumber == 1)
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
        TrainingPanel.transform.GetChild(currentTipNumber - 1).gameObject.SetActive(false);
        TrainingPanel.SetActive(false);
        TrainingPanelBackground.SetActive(false);
        switch (currentTipNumber)
        {
            case 4:
                uiManager.TaskPanelBehaviour.TaskInfoButton.interactable = true;
                uiManager.PadMenuBehaviour.ShowIDEButton.interactable = true;
                uiManager.PadMenuBehaviour.ShowHandbookButton.interactable = true;
                break;
            case 10:
                Pad_Dev.StartButton.interactable = true;
                Pad_Dev.ResetButton.interactable = true;
                Pad_Dev.TipButton.gameObject.SetActive(false);
                Pad_Dev.TipButton.interactable = true;
                Pad_Dev.ExitDevModeButton.interactable = true;
                break;
            case 14:
                gameManager.Player.GetComponent<PlayerBehaviour>().UnfreezePlayer();
                break;
        }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
        switch (gameManager.SceneIndex)
        {
            case 1:
                currentTipNumber = 10;
                break;
        }
    }
}
