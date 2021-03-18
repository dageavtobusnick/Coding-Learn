using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel_Level_1_Behaviour : MonoBehaviour
{
    public int taskNumber = 0;
    private Text currentTaskTitle;
    private Text currentTaskDescription;
    private InputField codeField;
    private InputField resultField;
    private PadBehaviour pad;
    private List<string> taskTitles = new List<string>();
    private List<string> taskDescriptions = new List<string>();
    private List<string> taskStartCodes = new List<string>();

    private void FormTasks()
    {
        taskTitles.Add("Привет, мир!");
        taskDescriptions.Add("   Робот продолжает выполнять диагностику, но уже на улице. Нужно узнать, сколько энергии он тратит на определённое количество шагов.\n" +
                             "   В коде уже есть переменные, которые нужны для расчёта. Закончи программу!");
        taskStartCodes.Add("public int Execute()\n" +
                          "{\n" +
                          "    int energyPerStep = 2;\n" +
                          "    int stepsCount = 8;\n" +
                          "    return ...\n" +
                          "}");

        taskTitles.Add("Огибаем препятствия");
        taskDescriptions.Add("    Впереди кусты. Но не беда - тропинка идёт в обход! Заодно узнаем, как тратится энергия на движение с поворотами.\n" +
                             "    Нужно переписать формулу расчёта, прибавив затраты на повороты. Переменные готовы, вот только в код закрались ошибки и пропуски. Исправь их, чтобы программа заработала!");
        taskStartCodes.Add("public double Execute()\n" +
                          "{\n" +
                          "    itn energyPerStep = 2\n" +
                          "    int stepsCount := 8;\n" +
                          "    ... energyPerRotation = 1.5;\n" +
                          "    int rotationsCount = 3;\n" +
                          "    return ...\n" +
                          "}");

        taskTitles.Add("Покидая дом");
        taskDescriptions.Add("   Диагностика почти завершена. К слову, она не прошла впустую - робот смог существенно оптимизировать использование энергии: 1 шаг теперь требует одну треть единицы, а поворот и вовсе проходит без потерь.\n" +
                             "   Сейчас наш железный друг хочет провести последний тест: ему интересно узнать, хватит ли небольшого кусочка энергии, чтобы дойти до конца тропы. Нам нужно посчитать, на сколько шагов её хватит. Допиши стоимость одного шага и формулу для расчёта.\n" +
                             "   Подсказка: для деления и записи дробей используй /. Чтобы дробная часть не пропала, числитель запиши в виде double.");
        taskStartCodes.Add("public double Execute()\n" +
                           "{\n" +
                           "    double energyPerMovement = ...\n" +
                           "    int availableEnergy = 4;\n" +
                           "    return ...\n" +
                           "}");
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (gameObject.name)
        {
            case "Cube_TaskTrigger_1":
                taskNumber = 1;
                break;
            case "Cube_TaskTrigger_2":
                taskNumber = 2;
                break;
        }
        currentTaskTitle.text = taskTitles[taskNumber];
        currentTaskDescription.text = taskDescriptions[taskNumber];
        pad.startCode = taskStartCodes[taskNumber];
        codeField.text = pad.startCode;
        resultField.text = "";
    }

    private void Start()
    {
        currentTaskTitle = GameObject.Find("TaskTitle").GetComponent<Text>();
        currentTaskDescription = GameObject.Find("TaskDescription").GetComponent<Text>();
        pad = GameObject.Find("Main Camera").GetComponent<PadBehaviour>();
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        FormTasks();
        currentTaskTitle.text = taskTitles[0];
        currentTaskDescription.text = taskDescriptions[0];
        pad.startCode = taskStartCodes[0];
    }
}
