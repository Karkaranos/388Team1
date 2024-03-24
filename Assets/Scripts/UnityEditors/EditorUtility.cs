using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorUtility : Editor
{
    public static void Show (SerializedProperty list, bool showListSize)
    {
        EditorGUILayout.PropertyField (list);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            for (int i = 0; i < list.arraySize; ++i)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
        }
        
        EditorGUI.indentLevel -= 1;
    }
}
