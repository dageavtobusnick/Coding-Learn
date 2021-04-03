using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel_Level_1_Behaviour : MonoBehaviour
{
    public int taskNumber;
    public bool isNextTaskButtonAvailable;
    private GameObject canvas;
    private GameObject startButton;
    private GameObject UICollector;
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
            canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
            isNextTaskButtonAvailable = false;
            startButton.GetComponent<StartButton_Level_1_Behaviour>().taskNumber = taskNumber;
        }
        else
        {
            GameObject.Find("CloseTaskButton").SetActive(false);
            nextLevelButton.gameObject.SetActive(true);
            currentExtendedTaskTitle.text = "Поздравляем!";
            currentExtendedTaskDescription.text = "     Вы прекрасно освоили операции с числовыми данными, а ваше приключение только начинается.\n" +
                                                  "     В путь!";
            canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
        }
    }

    public void CloseTask()
    {
        pad.transform.position = UICollector.transform.position;
        GameObject.Find("TaskPanel").transform.position = UICollector.transform.position;
        GameObject.Find("TaskCamera_" + taskNumber).GetComponent<Camera>().enabled = false;
        canvas.GetComponent<GameData>().currentSceneCamera.enabled = true;
    }

    public void ShowIntroduction()
    {
        currentExtendedTaskTitle.text = "Новые горизонты";
        currentExtendedTaskDescription.text = "     Итак, наше путешествие начинается!\n" +
                                              "     Пока не происходит ничего интересного. Можно спокойно полюбоваться природой... и продолжить осваивать программирование!\n" +
                                              "     Раз уж робот больше не заперт с нами в четырёх стенах, можно наконец-то поуправлять им. Нажимай клавиши <b><color=green>WASD</color></b> для передвижения и поворота. Когда появится что-нибудь интересное (например, задание), внизу появится подсказка." +
                                              "Нажми на нёё, и сможешь узнать что-то новое.\n" +
                                              "     Ну что ж, полный вперёд!";
        canvas.GetComponent<ExtendedTaskPanelBehaviour>().OpenTaskExtendedDescription();
        canvas.GetComponent<ExtendedTaskPanelBehaviour>().isTask = false;
    }

    private void FormTasks()
    {
        taskTitles.Add("Раз ромашка, два ромашка...");
        taskDescriptions.Add("  - Создай переменную redFlowersCount и запиши в неё количество красных цветков\n" +
                             "  - Создай переменную yellowFlowersCount и запиши в неё количество жёлтых цветков\n" +
                             "  - Верни сумму всех цветков на лужайке");
        taskExtendedDescriptions.Add("     Итак, вспомним, на чём мы остановились.\n" +
                                     "     В последней задаче в конце мы записали несколько переменных через знак \"+\" и получили их сумму. Да, мы можем выполнять <b><color=green>арифметические</color></b> (и многие другие) операции не только с числами, но и с <b><color=green>переменными</color></b>, " +
                                     "которые их содержат! Это одна из базовых функций любого языка " +
                                     "программирования.\n" +
                                     "     Познакомимся с простой арифметикой, и для начала - со сложением, хотя вы уже и знакомы с ним. Поработаем с ним ещё разок!\n" +
                                     "     На лужайке рядом с домом растёт много всего. Видите цветы? Посчитаем, сколько их тут растёт.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "\n" +
                           "\n" +
                           "}");

        taskTitles.Add("Геометрия на грибах");
        taskDescriptions.Add("  - Создайте переменную volume и в неё формулу для расчёта объёма. Формула для объёма полусферы:\n" +
                             "     V = (2/3) * pi * R^3\n" +
                             "  - В конце верните значение объёма");
        taskExtendedDescriptions.Add("     Отлично! Какие же ещё операции нам доступны?\n" +
                                     "     Конечно, остальные простейшие операции: <b><color=green>вычитание</color></b> (знак -), <b><color=green>умножение</color></b> (знак *) и <b><color=green>деление</color></b> (знак /). Знак ^ для возведения в степень здесь не работает, но его можно заменить " +
                                     "умножением числа на само себя. Используем эти знания в следующем задании!\n" +
                                     "     Путешествие начинается, конечно, очень скучно: ни сокровищ, ни опасностей, а вокруг только деревья да камни. Займём себя чем-нибудь и посчитаем... ну, например,... объём шляпки гриба! Да, а почему бы и нет)? Вообще-то, тут геометрию вспомнить надо, целая наука, это вам не " +
                                     "овец считать перед сном! Заодно посмотрим, как работают остальные арифметические операции.");
        taskStartCodes.Add("public double Execute()\n" +
                           "{\n" +
                           "    double pi = 3.14;\n" +
                           "    double R = 0.02;\n" +
                           "}");

        taskTitles.Add("Всё должно быть поровну");
        taskDescriptions.Add("  - Запишите в переменную flowersCount количество растущих цветков" +
                             "  - В конце верните остаток от их деления на 2");
        taskExtendedDescriptions.Add("     Красивые цветы! Вот бы нарвать их и подарить родителям и друзьям! Интересно, хватит ли их всем...\n" +
                                     "     Кстати, мы совсем забыли рассказать вам об ещё одном операции, которая тоже является очень важной - получение <b><color=green>остатка от деления</color></b> (обозначается знаком процента - %). Она может и не встречается в программах так часто, " +
                                     "но это не отменяет её важности. Порой без неё просто не обойтись. Как и нам с нами в следующем задании.\n" +
                                     "     Узнаем, получится ли разделить и подарить равное количество цветов маме и бабушке или останется лишний?");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "\n" +
                           "\n" +
                           "}");

        taskTitles.Add("Одним больше, одним меньше");
        taskDescriptions.Add("  - Увеличьте количество камней на 1, используя инкремент\n" +
                             "  - Ниже увеличьте их число втрое, используя сокращённую запись умножения" +
                             "  - В конце верните количество камней");
        taskExtendedDescriptions.Add("     Пока мы с вами считаем цветочки, робот всю дорогу считает камни. Давайте и мы поучаствуем! Только сначала - небольшой секрет.\n" +
                                     "     В языке C#, помимо обычных арифметических операций, есть ещё и <b><color=green>сокращённые</color></b>. Они позволяют выполнять те же действия и при этом писать меньше кода. Например,\n" +
                                     "     number += 3;  //То же самое, что number = number + 3;\n" +
                                     "     number *= 2;  //То же самое, что number = number * 2;\n" +
                                     "     number++;     //Увеличивает значение на 1\n" +
                                     "     number--;     //Уменьшает значение на 1\n" +
                                     "     Сокращённую запись можно применять ко всем операторам (+, -, *, /, %). Предлагаем и вам попробовать её использовать в следующем задании. Посчитаем камни.");
        taskStartCodes.Add("public int Execute()\n" +
                           "{\n" +
                           "    int rocksCount = 99;\n" +
                           "\n" +
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
        startButton = GameObject.Find("StartButton");
        UICollector = GameObject.Find("UI_Collector");
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
        FormTasks();
    }
}
