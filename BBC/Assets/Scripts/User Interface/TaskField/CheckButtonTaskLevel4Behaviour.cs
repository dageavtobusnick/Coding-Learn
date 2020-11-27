using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckButtonTaskLevel4Behaviour : MonoBehaviour
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
        expectedAnswers.Add(new List<string>() { "n>0", "n > 0", "0<n", "0 < n" });
        expectedAnswers.Add(new List<string>() { "0" });
        expectedAnswers.Add(new List<string>() { "%" });
        expectedAnswers.Add(new List<string>() { "//" });
        expectedAnswers.Add(new List<string>() { "sum" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task2()
    {
        taskNumber = 2;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "hile" });
        expectedAnswers.Add(new List<string>() { ">0", "> 0", ">=1", ">= 1" });
        expectedAnswers.Add(new List<string>() { "%" });
        expectedAnswers.Add(new List<string>() { "mult" });
        expectedAnswers.Add(new List<string>() { "%" });
        expectedAnswers.Add(new List<string>() { "//" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task3()
    {
        taskNumber = 3;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "var", "int" });
        expectedAnswers.Add(new List<string>() { "i < 20", "i<20", "i <= 19", "i<=19" });
        expectedAnswers.Add(new List<string>() { "2" });
        expectedAnswers.Add(new List<string>() { "i" });
        expectedAnswers.Add(new List<string>() { "var", "int" });
        expectedAnswers.Add(new List<string>() { "i < 20", "i<20", "i <= 19", "i<=19" });
        expectedAnswers.Add(new List<string>() { "2" });
        expectedAnswers.Add(new List<string>() { "i * i", "i*i" });
        CheckAnswers(expectedAnswers);
    }

    public void CheckAnswers_Task4()
    {
        taskNumber = 4;
        expectedAnswers = new List<List<string>>();
        expectedAnswers.Add(new List<string>() { "int", "var" });
        expectedAnswers.Add(new List<string>() { "i < 10", "i<10", "i <= 9", "i<=9" });
        expectedAnswers.Add(new List<string>() { "i++", "i+=1", "i += 1" });
        expectedAnswers.Add(new List<string>() { "int", "var" });
        expectedAnswers.Add(new List<string>() { "1" });
        expectedAnswers.Add(new List<string>() { "j++", "j+=1", "j += 1" });
        expectedAnswers.Add(new List<string>() { "i*j", "i * j", "j*i", "j * i" });
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
