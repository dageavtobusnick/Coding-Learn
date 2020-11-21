using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButtonTaskLevel3Behaviour : MonoBehaviour
{
    private int taskNumber;
    private List<List<string>> expectedAnswers;
    private Text resultText;
    private Color wrongAnswerColor = new Color(1f, 0f, 0f, 1f);
    private Color correctAnswerColor = new Color(0f, 1f, 0f, 1f);
    private bool[] isKeysEarned = new bool[4];

    public void CheckAnswers_Task1()
    {
        taskNumber = 1;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "a + b", "b + a", "a+b", "b+a" });
        expectedAnswers.Add(new List<string>() { "tempA * b", "b * tempA", "tempA*b", "b*tempA" });
        expectedAnswers.Add(new List<string>() { "a > b", "b < a", "a>b", "b<a" });
        expectedAnswers.Add(new List<string>() { "a * b", "b * a", "a*b", "b*a" });
        expectedAnswers.Add(new List<string>() { "tempA + b", "b + tempA", "tempA+b", "b+tempA" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task2()
    {
        taskNumber = 2;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "&&" });
        expectedAnswers.Add(new List<string>() { "рубль" });
        expectedAnswers.Add(new List<string>() { "&&" });
        expectedAnswers.Add(new List<string>() { "&&" });
        expectedAnswers.Add(new List<string>() { "рубля" });
        expectedAnswers.Add(new List<string>() { "рублей" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task3()
    {
        taskNumber = 3;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "<=" });
        expectedAnswers.Add(new List<string>() { "a * a", "a*a" });
        expectedAnswers.Add(new List<string>() { "b * b", "b*b" });
        expectedAnswers.Add(new List<string>() { "else" });
        expectedAnswers.Add(new List<string>() { "a" });
        expectedAnswers.Add(new List<string>() { "(-1)" });
        expectedAnswers.Add(new List<string>() { "c * (-1)", "(-1) * c", "c*(-1)", "(-1)*c)" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task4()
    {
        taskNumber = 4;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "&&" });
        expectedAnswers.Add(new List<string>() { "a1" });
        expectedAnswers.Add(new List<string>() { "else if" });
        expectedAnswers.Add(new List<string>() { "||" });
        expectedAnswers.Add(new List<string>() { "&&" });
        expectedAnswers.Add(new List<string>() { "a2" });
        expectedAnswers.Add(new List<string>() { "a3" });
        CheckAnswers(expectedAnswers);
    }

    private void CheckAnswers(List<List<string>> expectedAnswers)
    {
        resultText = GameObject.Find("ResultText_Task" + taskNumber).GetComponent<Text>();
        var isCorrect = true;
        for (var i = 0; i < expectedAnswers.Count; i++)
        {
            var newAnswer = GameObject.Find("AnswerTask" + taskNumber + "_" + (i + 1)).GetComponent<InputField>();
            for (var j = 0; j < expectedAnswers[i].Count; j++)
            {
                if (!expectedAnswers[i].Contains(newAnswer.text))
                {
                    isCorrect = false;
                    newAnswer.textComponent.color = wrongAnswerColor;
                }
                else newAnswer.textComponent.color = correctAnswerColor;
            }
        }
        if (isCorrect)
        {
            resultText.text = "Задание выполнено!";
            if (!isKeysEarned[taskNumber - 1])
            {
                GameObject.Find("KeyCounter").GetComponent<KeyCounterBehaviour>().keyCount++;
                isKeysEarned[taskNumber - 1] = true;
            }
        }
        else resultText.text = "Где-то есть ошибки. Исправь их и попробуй ещё раз!";
    }
}
