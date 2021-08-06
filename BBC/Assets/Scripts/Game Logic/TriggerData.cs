using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerData : MonoBehaviour
{
    public enum Purpose
    {
        Task,
        Dialog,
        ScriptMoment,
        EnterToMiniScene,
        ChangeLevel,
        SaveGame,
        FinishLevel
    }

    [Tooltip("Для чего триггер предназначен")]
    public Purpose TriggerPurpose;
    [Tooltip("Номер триггера")]
    public int TriggerNumber;
    [Tooltip("Номер задания")]
    public int TaskNumber;
    [Tooltip("Текст для кнопки взаимодействия")]
    public string ActionButtonText;
    [Tooltip("Индекс уровня, на который переходим")]
    public int NextLevelIndex;

    public static readonly string MarkerAnimation = "RotateExclamationMark";
}
