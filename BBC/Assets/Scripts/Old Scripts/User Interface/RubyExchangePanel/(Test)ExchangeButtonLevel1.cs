using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeButtonLevel1 : MonoBehaviour
{
    private Button[] themePartButtons;
    private GameObject rubyCounter;
    private InputField exchangeStatus;
    private int spentRubyCount = 0;
    private string temporaryText = "    ???";
    private string[] themePartTexts;

    public void ExchangeRubyToTheory()
    {
        exchangeStatus.transform.position = GameObject.Find("Panel_RubyToTheoryExchange").transform.position;
        if (rubyCounter.GetComponent<RubyCounterBehaviour>().rubyCount > 0)
        {
            rubyCounter.GetComponent<RubyCounterBehaviour>().rubyCount--;
            spentRubyCount++;
            switch (spentRubyCount)
            {
                case 1:
                    OpenAccessToThemeParts(new[] { 1, 2 });
                    break;
                case 2:
                    OpenAccessToThemeParts(new[] { 3, 4 });
                    break;
                case 3:
                    OpenAccessToThemeParts(new[] { 5, 6, 7 });
                    break;
                case 4:
                    OpenAccessToThemeParts(new[] { 8 });
                    break;
            }
            exchangeStatus.text = "Отлично! В твоём справочнике появилась новая информация!";
        }
        else exchangeStatus.text = "Недостаточно рубинов. Возвращайся позже!";

        if (spentRubyCount == 4)
            Destroy(GameObject.Find("OldmanButton"));
    }

    private void OpenAccessToThemeParts(int[] partNumbers)
    {
        foreach (var number in partNumbers)
        {
            themePartButtons[number-1].GetComponentInChildren<Text>().text = themePartTexts[number-1];
            themePartButtons[number-1].interactable = true;
        }
    }

    private void Start()
    {
        rubyCounter = GameObject.Find("RubyCounter");
        exchangeStatus = GameObject.Find("Panel_RubyToTheoryExchangeStatus").GetComponent<InputField>();
        exchangeStatus.transform.position = GameObject.Find("UI_Collector").transform.position;
        var sourceButtons = GameObject.Find("ExchangeButton").GetComponent<ThemePartButtonsBehaviour>().themePartButtons;
        themePartButtons = new Button[sourceButtons.Length];
        themePartTexts = new string[themePartButtons.Length];
        Array.Copy(sourceButtons, themePartButtons, sourceButtons.Length);
        for (var i = 0; i < themePartButtons.Length; i++)
        {
            themePartTexts[i] = String.Copy(themePartButtons[i].GetComponentInChildren<Text>().text);
            //themePartTexts[i] = themePartButtons[i].GetComponentInChildren<Text>().text;
            themePartButtons[i].GetComponentInChildren<Text>().text = temporaryText;
            themePartButtons[i].interactable = false;
        }
    }
}
