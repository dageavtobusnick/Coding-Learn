using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButtonTaskLevel1Behaviour : MonoBehaviour
{
    private int taskNumber;
    private List<List<string>> expectedAnswers;
    private Text resultText;
    private Color wrongAnswerColor = new Color(1f, 0f, 0f, 1f);
    private Color correctAnswerColor = new Color(0f, 1f, 0f, 1f);
    private bool isKeyEarned = false;

    public void CheckAnswers_Task1()
    {
        taskNumber = 1;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "2" });
        expectedAnswers.Add(new List<string>() { "3" });
        expectedAnswers.Add(new List<string>() { "3" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task2()
    {
        taskNumber = 2;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "Main" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task3()
    {
        taskNumber = 3;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "1" });
        expectedAnswers.Add(new List<string>() { "2" });
        expectedAnswers.Add(new List<string>() { "4" });
        expectedAnswers.Add(new List<string>() { "3" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task4()
    {
        taskNumber = 4;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "int a;" });
        expectedAnswers.Add(new List<string>() { "a = 45;", "a=45;" });
        expectedAnswers.Add(new List<string>() { "double b = 3.14;", "double b=3.14;" });
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
            if (!isKeyEarned)
            {
                GameObject.Find("KeyCounter").GetComponent<KeyCounterBehaviour>().keyCount++;
                isKeyEarned = true;
            }
        }
        else resultText.text = "Где-то есть ошибки. Исправь их и попробуй ещё раз!";
    }
}
