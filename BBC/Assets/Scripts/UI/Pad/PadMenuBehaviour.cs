using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PadMode
{
    Normal,
    Development,
    HandbookMainThemes,
    HandbookSubThemes,
    HandbookProgrammingInfo
}

public class PadMenuBehaviour : MonoBehaviour
{   
    [Header("Планшет (главное меню)")]
    [Tooltip("Планшет")]
    public GameObject Pad;
    [Tooltip("Счетчик подсказок в меню")]
    public Text TipsMenuCounter;
    [Tooltip("Счетчик монет в меню")]
    public Text CoinsMenuCounter;
    [Tooltip("Кнопка перехода в режим разработки")]
    public Button ShowIDEButton;
    [Tooltip("Кнопка перехода в режим справочника")]
    public Button ShowHandbookButton; 

    [HideInInspector] public List<int> AvailableTipsCounts;
    [HideInInspector] public bool IsPadCalled;
    [HideInInspector] public bool IsCallAvailable;

    private UIManager uiManager;
    private GameManager gameManager;
    private PlayerBehaviour playerBehaviour;

    public void UpdatePadData()
    {
        CoinsMenuCounter.text = gameManager.CoinsCount.ToString();
        TipsMenuCounter.text = gameManager.TipsCount.ToString();   
    }

    public void PlayPadMoveAnimation(string normalAnimation, string devAnimation)
    {
        var padMode = uiManager.PadMode;
        var padAnimator = Pad.GetComponentInParent<Animator>();
        if (padMode == PadMode.Normal)
            padAnimator.Play(normalAnimation);
        else if (padMode == PadMode.Development)
            padAnimator.Play(devAnimation);
    }  

    private IEnumerator CallPad()
    {
        if (!IsPadCalled)
        {
            playerBehaviour.FreezePlayer();
            Pad.GetComponentInParent<Animator>().Play("ShowPad"); 
            IsPadCalled = !IsPadCalled;
        }
        else
        {
            if (uiManager.PadMode == PadMode.Normal)
            {
                playerBehaviour.UnfreezePlayer();
                Pad.GetComponentInParent<Animator>().Play("HidePad");
                IsPadCalled = !IsPadCalled;
            }
        }
        yield return new WaitForSeconds(0.667f);
        uiManager.TrainingPanelBehaviour.TryShowTraining(PreviousAction.PadCall);
    }   

    private void Update()
    {      
        if (Input.GetKeyDown(KeyCode.P) && IsCallAvailable)
            StartCoroutine(CallPad());
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        gameManager = GameManager.Instance;
        playerBehaviour = gameManager.Player.GetComponent<PlayerBehaviour>();
        uiManager.PadMode = PadMode.Normal;
        IsPadCalled = false;
        IsCallAvailable = true;
        ShowIDEButton.interactable = gameManager.SceneIndex == 0;
        AvailableTipsCounts = gameManager.AvailableTipsCounts;
        UpdatePadData();      
    }
}
