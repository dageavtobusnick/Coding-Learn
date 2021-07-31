using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceElements : MonoBehaviour
{
    [Header ("Кнопки")]
    [Tooltip ("Кнопка выполнения действия (активация задания, смена сцены и т.д.)")]
    public Button ActionButton;

    [Header("Панель обучения")]
    public GameObject TrainingPanel;
    [Tooltip("Фон для панели обучения")]
    public GameObject TrainingPanelBackground;

    [Header ("Панель задания")]
    [Tooltip("Панель задания")]
    public GameObject TaskPanel;
    [Tooltip("Название задания")]
    public Text TaskTitle;
    [Tooltip("Описание задания")]
    public Text TaskDescription;
    [Tooltip("Скроллбар для прокрутки задания")]
    public Scrollbar TaskDescriptionScrollbar;
    [Tooltip("Кнопка для получения полной доп. информации о задании")]
    public Button TaskInfoButton;
    [Tooltip("Кнопка для закрытия задания")]
    public Button CloseTaskButton;

    [Header("Панель задания (расширенная)")]
    [Tooltip("Панель задания (расширенная)")]
    public GameObject ExtendedTaskPanel;
    [Tooltip("Название задания (расширенное)")]
    public Text ExtendedTaskTitle;
    [Tooltip("Описание задания (расширенное)")]
    public Text ExtendedTaskDescription;
    [Tooltip("Скроллбар для прокрутки описания")]
    public Scrollbar ExtendedTaskDescriptionScrollbar;
    [Tooltip("Кнопка перехода на следующий уровень")] 
    public Button NextLevelButton;
    [Tooltip("Кнопка для закрытия расширенного описания задания")]
    public Button CloseExtendedTaskButton;

    [Header("Планшет (главное меню)")]
    [Tooltip("Счетчик подсказок в меню")]
    public Text TipsMenuCounter;
    [Tooltip("Счетчик монет в меню")]
    public Text CoinsMenuCounter;
    [Tooltip("Кнопка перехода в режим разработки")]
    public Button IDEButton;
    [Tooltip("Кнопка перехода в режим справочника")]
    public Button HandbookButton;

    [Header("Планшет (режим разработки)")]
    [Tooltip("Планшет")]
    public GameObject Pad;
    [Tooltip("Поле для ввода кода")]
    public InputField CodeField;
    [Tooltip("Поле для вывода результата выполнения задания")]
    public Text ResultField;
    [Tooltip("Поле для вывода выхода программы (корректный или нет)")]
    public Text OutputField;
    [Tooltip("Кнопка запуска программы")]
    public Button StartButton;
    [Tooltip("Кнопка сброса кода к начальному состоянию")]
    public Button ResetButton;
    [Tooltip("Кнопка выключения планшета")]
    public Button PowerButton;
    [Tooltip("Кнопка включения панели подсказок")]
    public Button TipButton;
    [Tooltip("Кнопка возврата в меню из режима разработки")]
    public Button ExitDevModeButton;

    [Header("Планшет (панель подсказок)")]
    [Tooltip("Панель подсказок")]
    public GameObject HelpPanel;
    [Tooltip("Кнопка показа подсказки")]
    public Button ShowTipButton;
    [Tooltip("Кнопка покупки подсказки")]
    public Button BuyTipButton;
    [Tooltip("Кнопка покупки нескольких подсказок")]
    public Button BuyManyTipsButton;
    [Tooltip("Поле с текстом подсказки")]
    public Text Tip;
    [Tooltip("Счётчик подсказок")]
    public Text TipsCounter;
    [Tooltip("Счётчик монет")]
    public Text CoinsCounter;
    [Tooltip("Текст-филлер (заполняется место подсказки, пока она недоступна)")]
    public Text TipFiller;

    [Header("Планшет (режим справочника)")]
    [Tooltip("Кнопки разделов по программированию")]
    public GameObject ThemeButtons;
    [Tooltip("Кнопки подразделов для каждого раздела")]
    public GameObject SubThemeButtons;
    [Tooltip("Поле для информации по программрованию")]
    public Text ProgrammingInfo;
    [Tooltip("Заголовок для раздела по программрованию")]
    public Text ProgrammingInfoTitle;
    [Tooltip("Кнопка для перехода на предыдущую страницу")]
    public Button PreviousHandbookPageButton;

    [Header("Мини-карта")]
    [Tooltip("Мини-карта")]
    public GameObject Minimap;

    [Header("Панель текущей цели")]
    [Tooltip("Панель текущей цели")]
    public GameObject TargetPanel;
    [Tooltip("Панель текущей цели")]
    public GameObject TargetPanelBackground;
    [Tooltip("Надпись Цель")]
    public Text TargetLabel;
    [Tooltip("Текстовое описание цели")]
    public Text TargetText;

    [Header("Панель выхода в меню")]
    [Tooltip("Панель выхода в меню")]
    public GameObject ExitToMenuPanel;

    [Header("Чёрный экран (контейнер)")]
    [Tooltip("Чёрный экран (контейнер)")]
    public GameObject BlackScreen;

    private TargetPanelBehaviour targetPanelBehaviour;

    public void ChangeCallAvailability(bool isCallAvailable)
    {
        targetPanelBehaviour.IsCallAvailable = isCallAvailable;
        Pad.GetComponent<PadBehaviour>().IsCallAvailable = isCallAvailable;
    }

    public void HideUI()
    {
        if (targetPanelBehaviour.IsShown)
            targetPanelBehaviour.HideTarget();
        targetPanelBehaviour.IsCallAvailable = false;
        Minimap.SetActive(false);
    }

    public void ShowUI()
    {
        targetPanelBehaviour.IsCallAvailable = true;
        Minimap.SetActive(true);
    }

    private void Awake()
    {
        targetPanelBehaviour = gameObject.GetComponent<TargetPanelBehaviour>();
        ChangeCallAvailability(false);
    }
}
