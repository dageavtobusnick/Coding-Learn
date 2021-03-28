using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel_Training_Behaviour : MonoBehaviour
{
    public int taskNumber;
    public bool isNextTaskButtonAvailable;
    private GameObject canvas;
    private Button nextTaskButton;
    private Text currentTaskTitle;
    private Text currentTaskDescription;
    private Text currentExtendedTaskTitle;
    private Text currentExtendedTaskDescription;
    private InputField codeField;
    private InputField resultField;
    private InputField outputField;
    private Button nextLevelButton;
    private PadBehaviour pad;
    private List<string> taskTitles = new List<string>();
    private List<string> taskDescriptions = new List<string>();
    private List<string> taskExtendedDescriptions = new List<string>();
    private List<string> taskStartCodes = new List<string>();

    public void ChangeTask()
    {
        if (taskNumber <= taskTitles.Count)
        {
            currentTaskTitle.text = taskTitles[taskNumber - 1];
            currentExtendedTaskTitle.text = taskTitles[taskNumber - 1];
            currentTaskDescription.text = taskDescriptions[taskNumber - 1];
            currentExtendedTaskDescription.text = taskExtendedDescriptions[taskNumber - 1];
            pad.startCode = taskStartCodes[taskNumber - 1];
            codeField.text = pad.startCode;
            resultField.text = "";
            outputField.text = "";
            if (taskNumber > 1)
                canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
            isNextTaskButtonAvailable = false;
        }
        else 
        {
            GameObject.Find("CloseTaskButton").SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            currentExtendedTaskTitle.text = "Поздравляем!";
            currentExtendedTaskDescription.text = "     Вы отлично справились с первыми заданиями. Это не может не радовать! Рад и наш друг <name>, который надеется на нашу помощь в поиске сокровищ. Отправимся же на улицу и применим знания на железном друге. Во время путешествия мы освоим новые возможности " +
                                          "языка и отточим наши навыки.\n" +
                                          "     Если вы готовы, тогда поехали!";
            canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
        }
    }

    private void FormTasks()
    {
        taskTitles.Add("Начало");
        taskDescriptions.Add("  - В программе между фигурных скобок напиши\n return 2 + 2;");
        taskExtendedDescriptions.Add("     Привет, игрок! Добро пожаловать в игру Coding Learn! Здесь ты сможешь получить базовые навыки программирования на C# - одном из самых популярных и востребованных языков. Не будет больше терять ни минуты. Приступим!\n" +
                                     "     Пару дней назад мальчик по имени <name>, гуляя по лесу, нашёл коробку с различными железяками. Он принёс её домой, осмотрел и нашёл инструкцию. В ней были чертежи робота, а также записка о том, что собрав его, можно найти " +
                                     "сокровища, спрятанные в этой местности. <name>, жаждущий приключений, смог по инструкции собрать робота и скачал секретное приложение для управления им. Но возникла проблема: <name> может спокойно управлять движением, однако чтобы " +
                                     "использовать остальные функции, нужно писать небольшие программы на языке C#.\n" +
                                     "     Давайте поможем <name> освоить управление роботом и добраться до сокровищ. Полученные знания будут полезны и нам с вами, не так ли?\n" +
                                     "     Итак, начнём!\n" +
                                     "     Программы, написанные на языке C# (да и на других языках тоже), состоят из <b><color=green>инструкций</color></b>. Каждая инструкция выполняет какое-то действие (складывает числа, умножает их и т.д.).\n" +
                                     "     Чтобы решить какую-то более сложную задачу, в программе " +
                                     "пишут <b><color=green>функции</color></b> (в C# принято называть их <b><color=green>методами</color></b>), которые включают в себя целый набор различных инструкций.\n" +
                                     "     Попробуем написать свою первую программу! Для этого используем планшет <name>, он расположен в правом нижнем углу экрана. В левом верхнем углу ты можешь увидеть краткое описание задания. Если хочешь узнать больше подробностей, нажми на кнопку рядом с заданием.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "\n" +
                           "\n" +
                           "}");

        taskTitles.Add("Знакомимся с переменными");
        taskDescriptions.Add(" - Перед строчкой с return объяви целочисленную переменную stepsCount:\n int stepsCount; \n" +
                             " - Ниже присвой переменной значение 0:\n stepsCount = 0;\n" +
                             " - В return замени 2 + 2 на имя созданной переменной");
        taskExtendedDescriptions.Add("     Поздравляем! Это ваш первый шаг на пути к званию настоящего программиста. Не будем останавливаться на достигнутом, а лучше разберём, что означал наш код.\n" +
                                     "     В самой первой строчке находится заголовок метода. Первое слово нам сейчас не интересно (мы поговорим о нём несколько позже, пока просто будем писать его). Второе означает <b><color=green>тип возвращаемого значения</color></b>. Что это значит?\n" +
                                     "     Каждый метод в программе может как бы <i>отдавать</i> результат его работы в другие части программы для дальнейшего использования. Это мы делаем в строчке с return. Например, ваша программа <i>вернула</i> сумму 2 и 2, т.е. 4. Четыре - это целое число, этот " +
                                     "тип данных обозначается <b><color=green>int</color></b>. Его мы и видим в заголовке метода.\n" +
                                     "     Далее идёт название метода. Названия принято задавать в виде глагола (или чтобы он начинался с глагола), обозначающее действие, которое выполняет метод. Это делает код понятным для других разработчиков.\n" +
                                     "     В круглых скобочках пишутся <b><color=green>аргументы</color></b> - входные данные, которые метод может использовать для выполнения задачи. Их наличие необязательно (у нас их и вовсе нет).\n" +
                                     "     Наконец, в фигурных скобках находятся все инструкции метода. Каждая отделяется друг от друга точкой с запятой на конце.\n" +
                                     "     Чтобы сохранять данные, в языках программирования используют <b><color=green>переменные</color></b>. Каждая переменная - это, по сути, ячейка памяти, которой мы даём имя для удобного поиска данных. Переменные бывают разных типов. Один из них - знакомый нам " +
                                     "<b><color=green>int</color></b>. Посмотрим, как работают с переменными на практике!");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "    return 2 + 2;\n" +
                           "}");

        taskTitles.Add("Каково моё (предна)значение?");
        taskDescriptions.Add("  - Удали вторую строчку программы.\n" +
                             "  - Присвой переменной stepsCount значение 2, написав:\n" +
                             "    int stepsCount = 2;\n" +
                             "    <i>Это поможет немного расшевелить робота.</i>");
        taskExtendedDescriptions.Add("     Отлично! Итак, что же мы сделали?\n" +
                                     "     В первой строчке мы <i><color=green>объявили</color></i> переменную, т.е. попросили программу зарезервировать память для нашых данных. Данными, в нашем случае, является целое число, это мы указываем в начале с помощью типа данных int.\n" +
                                     "     Далее мы даём нашей переменной имя, по которому мы сможем обращаться к этой ячейке памяти. Имена лучше давать осмысленные, чтобы было понятно, что за данные мы храним в переменной. Имя stepsCount будет означать количество шагов, которое должен сделать робот.\n" +
                                     "     Далее мы <i><color=green>присвоили</color></i> переменной значение 0, т.е. теперь в этой ячейке памяти хранится значение 'ноль'. На самом деле, всё это можно сделать короче. Сейчас покажем, как.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "    int stepsCount;\n" +
                           "    stepsCount = 0;\n" +
                           "    return stepsCount;\n" +
                           "}");

        taskTitles.Add("Не int-ом единым");
        taskDescriptions.Add("  - Ниже, после переменной stepsCount, создайте переменную g типа float и присвойте ей значение 9.8\n" +
                             "  - Ниже, по аналогии с g, создайте переменную pi типа double со значением 3.14159\n" +
                             "  - Ниже создайте переменную billion типа long со значением 1000000000\n" +
                             "  - В return вместо stepsCount напишите: stepsCount + g + pi + billion");
        taskExtendedDescriptions.Add("     Супер! Мы научились сохранять в памяти целые числа. Но мы ведь знаем, что числа бывают разные и далеко не только целые. Может, для них есть другие типы данных?\n" +
                                     "     Да! Например, для очень длинных целых чисел есть \"длинный int\" - <b><color=green>long</color></b>. Если нужно хранить не только целые, но и дробные числа, используют <b><color=green>float</color></b>, но чаще - тип <b><color=green>double</color></b>. Они отличаются тем, " +
                                     "что в double могут храниться числа куда большой точности.\n" +
                                     "     Есть и другие типы, но пока нам хватит и этих. Поработаем с ними в следующем задании!");
        taskStartCodes.Add("public double Execute()\n" +
                           "{\n" +
                           "    int stepsCount = 2;\n" +
                           "    return stepsCount;\n" +
                           "}");
    }

    private void Update()
    {
        if (isNextTaskButtonAvailable)
            nextTaskButton.gameObject.SetActive(true);
        else nextTaskButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        currentTaskTitle = GameObject.Find("TaskTitle").GetComponent<Text>();
        currentTaskDescription = GameObject.Find("TaskDescription").GetComponent<Text>();
        currentExtendedTaskTitle = GameObject.Find("TaskTitle_Extended").GetComponent<Text>();
        currentExtendedTaskDescription = GameObject.Find("TaskDescription_Extended").GetComponent<Text>();
        pad = GameObject.Find("Pad").GetComponent<PadBehaviour>();
        codeField = GameObject.Find("CodeField").GetComponent<InputField>();
        resultField = GameObject.Find("ResultField").GetComponent<InputField>();
        outputField = GameObject.Find("OutputField").GetComponent<InputField>();
        nextTaskButton = GameObject.Find("NextTaskButton").GetComponent<Button>();
        nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        nextTaskButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        isNextTaskButtonAvailable = false;
        taskNumber = 1;
        FormTasks();
        ChangeTask();
    }
}
