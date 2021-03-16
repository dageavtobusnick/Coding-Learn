using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeButtonLevel2Behaviour : MonoBehaviour
{
    private GameObject rubyCounter;
    private InputField exchangeStatus;
    private Button themePart1Button;
    private Button themePart2Button;
    private Button themePart3Button;
    private int spentRubyCount = 0;
    private string temporaryText = "    ???";
    private string themePart1ButtonText;
    private string themePart2ButtonText;
    private string themePart3ButtonText;

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
                    break;
                case 2:
                    themePart2Button.GetComponentInChildren<Text>().text = themePart2ButtonText;
                    themePart2Button.interactable = true;
                    break;
                case 3:
                    themePart3Button.GetComponentInChildren<Text>().text = themePart3ButtonText;
                    themePart3Button.interactable = true;
                    break;
            }
            exchangeStatus.text = "Отлично! В твоём справочнике появилась новая информация!";
        }
        else exchangeStatus.text = "Недостаточно рубинов. Возвращайся позже!";

        if (spentRubyCount == 3)
            Destroy(GameObject.Find("OldmanButton"));
    }

    private void Start()
    {
        rubyCounter = GameObject.Find("RubyCounter");
        exchangeStatus = GameObject.Find("Panel_RubyToTheoryExchangeStatus").GetComponent<InputField>();
        exchangeStatus.transform.position = GameObject.Find("UI_Collector").transform.position;

        themePart1Button = GameObject.Find("Theme2_Part1Button").GetComponent<Button>();
        themePart1ButtonText = themePart1Button.GetComponentInChildren<Text>().text;
        themePart1Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart1Button.interactable = false;

        themePart2Button = GameObject.Find("Theme2_Part2Button").GetComponent<Button>();
        themePart2ButtonText = themePart2Button.GetComponentInChildren<Text>().text;
        themePart2Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart2Button.interactable = false;

        themePart3Button = GameObject.Find("Theme2_Part3Button").GetComponent<Button>();
        themePart3ButtonText = themePart3Button.GetComponentInChildren<Text>().text;
        themePart3Button.GetComponentInChildren<Text>().text = temporaryText;
        themePart3Button.interactable = false;
    }
}
