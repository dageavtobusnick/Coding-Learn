using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeButtonLevel1Behaviour : MonoBehaviour
{
    private GameObject rubyCounter;
    private InputField exchangeStatus;
    private Button themePart1Button;
    private Button themePart2Button;
    private Button themePart3Button;
    private Button themePart4Button;
    private Button themePart5Button;
    private Button themePart6Button;
    private Button themePart7Button;
    private Button themePart8Button;
    private int spentRubyCount = 0;
    private string temporaryText = "    ???";
    private string themePart1ButtonText;
    private string themePart2ButtonText;
    private string themePart3ButtonText;
    private string themePart4ButtonText;
    private string themePart5ButtonText;
    private string themePart6ButtonText;
    private string themePart7ButtonText;
    private string themePart8ButtonText;

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
                    themePart1Button.GetComponentInChildren<Text>().text = themePart1ButtonText;
                    themePart1Button.interactable = true;
                    themePart2Button.GetComponentInChildren<Text>().text = themePart2ButtonText;
                    themePart2Button.interactable = true;
                    break;
                case 2:
                    themePart3Button.GetComponentInChildren<Text>().text = themePart3ButtonText;
                    themePart3Button.interactable = true;
                    themePart4Button.GetComponentInChildren<Text>().text = themePart4ButtonText;
                    themePart4Button.interactable = true;
                    break;
                case 3:
                    themePart5Button.GetComponentInChildren<Text>().text = themePart5ButtonText;
                    themePart5Button.interactable = true;
                    themePart6Button.GetComponentInChildren<Text>().text = themePart6ButtonText;
                    themePart6Button.interactable = true;
                    themePart7Button.GetComponentInChildren<Text>().text = themePart7ButtonText;
                    themePart7Button.interactable = true;
                    break;
                case 4:
                    themePart8Button.GetComponentInChildren<Text>().text = themePart8ButtonText;
                    themePart8Button.interactable = true;
                    break;
            }
            exchangeStatus.text = "Отлично! В твоём справочнике появилась новая информация!";
        }
        else exchangeStatus.text = "Недостаточно рубинов. Возвращайся позже!";

        if (spentRubyCount == 4)
            Destroy(GameObject.Find("OldmanButton"));
    }

    private void Start()
    {
        rubyCounter = GameObject.Find("RubyCounter");
        exchangeStatus = GameObject.Find("Panel_RubyToTheoryExchangeStatus").GetComponent<InputField>();
        exchangeStatus.transform.position = GameObject.Find("UI_Collector").transform.position;

        themePart1Button = GameObject.Find("Theme1_Part1Button").GetComponent<Button>();
        themePart1ButtonText = themePart1Button.GetComponentInChildren<Text>().text;
        themePart1Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart1Button.interactable = false;

        themePart2Button = GameObject.Find("Theme1_Part2Button").GetComponent<Button>();
        themePart2ButtonText = themePart2Button.GetComponentInChildren<Text>().text;
        themePart2Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart2Button.interactable = false;

        themePart3Button = GameObject.Find("Theme1_Part3Button").GetComponent<Button>();
        themePart3ButtonText = themePart3Button.GetComponentInChildren<Text>().text;
        themePart3Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart3Button.interactable = false;

        themePart4Button = GameObject.Find("Theme1_Part4Button").GetComponent<Button>();
        themePart4ButtonText = themePart4Button.GetComponentInChildren<Text>().text;
        themePart4Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart4Button.interactable = false;

        themePart5Button = GameObject.Find("Theme1_Part5Button").GetComponent<Button>();
        themePart5ButtonText = themePart5Button.GetComponentInChildren<Text>().text;
        themePart5Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart5Button.interactable = false;

        themePart6Button = GameObject.Find("Theme1_Part6Button").GetComponent<Button>();
        themePart6ButtonText = themePart6Button.GetComponentInChildren<Text>().text;
        themePart6Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart6Button.interactable = false;

        themePart7Button = GameObject.Find("Theme1_Part7Button").GetComponent<Button>();
        themePart7ButtonText = themePart7Button.GetComponentInChildren<Text>().text;
        themePart7Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart7Button.interactable = false;

        themePart8Button = GameObject.Find("Theme1_Part8Button").GetComponent<Button>();
        themePart8ButtonText = themePart8Button.GetComponentInChildren<Text>().text;
        themePart8Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart8Button.interactable = false;
    }
}
