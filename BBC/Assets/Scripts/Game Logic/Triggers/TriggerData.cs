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

    [Tooltip("��� ���� ������� ������������")]
    public Purpose TriggerPurpose;
    [Tooltip("����� ��� ������ ��������������")]
    public string ActionButtonText;

    [Tooltip("����� ��������")]
    public int ScriptMoment_TriggerNumber;

    [Tooltip("����� �������")]
    public int Task_TaskNumber;
    
    [Tooltip("������ ������, �� ������� ���������")]
    public int ChangeLevel_NextLevelIndex;

    [Tooltip("�������, � �������� ������������ �����")]
    public GameObject EnterToMiniScene_DestinationTrigger;
    [Tooltip("������ �� ����-����� ���� ��������")]
    public bool EnterToMiniScene_IsMinimapShouldActive;
}
