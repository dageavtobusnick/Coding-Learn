using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerData))]
public class TriggerComponentEditor : Editor
{
    private TriggerData component;
    private SerializedProperty compPurpose;
    private SerializedProperty compTriggerNumber;
    private SerializedProperty compTaskNumber;
    private SerializedProperty compNextLevelIndex;
    private SerializedProperty compActionButtonText;

    private void OnEnable()
    {
        component = target as TriggerData;
        compPurpose = serializedObject.FindProperty("TriggerPurpose");
        compTriggerNumber = serializedObject.FindProperty("TriggerNumber");
        compTaskNumber = serializedObject.FindProperty("TaskNumber");
        compNextLevelIndex = serializedObject.FindProperty("NextLevelIndex");
        compActionButtonText = serializedObject.FindProperty("ActionButtonText");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(compPurpose);
        EditorGUILayout.PropertyField(compActionButtonText);
        switch (component.TriggerPurpose)
        {
            case TriggerData.Purpose.Task:
                EditorGUILayout.PropertyField(compTaskNumber);
                break;
            case TriggerData.Purpose.ChangeLevel:
                EditorGUILayout.PropertyField(compNextLevelIndex);
                break;
            case TriggerData.Purpose.ScriptMoment:
                EditorGUILayout.PropertyField(compTriggerNumber);
                break;
        }
        serializedObject.ApplyModifiedProperties();
    }
}
