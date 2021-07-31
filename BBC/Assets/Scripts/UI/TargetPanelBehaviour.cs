using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPanelBehaviour : MonoBehaviour
{
    [Header("Интерфейс")]
    public GameObject Canvas;
    [HideInInspector]
    public string TargetText;
    [HideInInspector]
    public string TargetLabelText;
    [HideInInspector]
    public bool IsShown;
    [HideInInspector]
    public bool IsCallAvailable;

    private GameData gameData;
    private InterfaceElements UI;
    private int timer;  

    public void ChangeTarget(string newTargetText)
    {
        TargetText = newTargetText;
        StartCoroutine(ShowTarget_COR());
    }

    public void HideTarget()
    {
        for (var i = 0; i < 2; i++)
        {
            var background = UI.TargetPanelBackground.transform.GetChild(i).gameObject;
            background.GetComponent<Image>().fillAmount = 0f;
            background.SetActive(false);
        }
        UI.TargetLabel.text = "";
        UI.TargetLabel.gameObject.SetActive(false);
        UI.TargetText.text = "";
        UI.TargetText.gameObject.SetActive(false);
        IsShown = false;
    }

    private IEnumerator ShowTarget_COR()
    {
        IsShown = true;
        timer = 5;
        for (var i = 0; i < 2; i++)
        {
            var background = UI.TargetPanelBackground.transform.GetChild(i);
            background.gameObject.SetActive(true);
            background.GetComponent<Animator>().Play("DrawBackground");
        }
        yield return new WaitForSeconds(0.3f);
        UI.TargetLabel.gameObject.SetActive(true);
        for (var i = 0; i < TargetLabelText.Length; i++)
        {
            UI.TargetLabel.text += TargetLabelText[i];
            yield return new WaitForSeconds(0.01f);
        }
        UI.TargetText.gameObject.SetActive(true);
        for (var i = 0; i < TargetText.Length; i++)
        {
            UI.TargetText.text += TargetText[i];
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(HideTargetAfterTimer_COR());
    }

    private IEnumerator HideTargetAfterTimer_COR()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
        }
        HideTarget();
    }  

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && IsCallAvailable)
        {
            if (timer == 0)
                StartCoroutine(ShowTarget_COR());
            else timer = 5;
            UI.GetComponent<TrainingScript>().TryShowTraining(TrainingScript.PreviousAction.TargetCall);
        }
    }

    private void Start()
    {
        UI = Canvas.GetComponent<InterfaceElements>();
        gameData = Canvas.GetComponent<GameData>();
        TargetLabelText = "Цель:";
        TargetText = "Исследуйте локацию";
        timer = 0;
        HideTarget();
        IsCallAvailable = false;
    }
}
