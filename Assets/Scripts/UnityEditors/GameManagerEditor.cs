/*using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager gm;

    private GameManager.LastingPowerupType type;

    private void OnEnable()
    {
        gm = target as GameManager;
    }
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        gm.LoseConditionIsPlayerLivesIfTrue = EditorGUILayout.Toggle("PlayerLives is lose condition",
            gm.LoseConditionIsPlayerLivesIfTrue);
        GUILayout.Space(4);

        if (gm.LoseConditionIsPlayerLivesIfTrue)
        {
            gm.PlayerLives = EditorGUILayout.IntField("Player Lives", gm.PlayerLives);
        }
        else
        {
            gm.MaxHitCounter = EditorGUILayout.IntField("Max Number of Hits", gm.MaxHitCounter);
            gm.currentHitCounter = EditorGUILayout.IntField("Current Hits", gm.currentHitCounter);
        }
        GUILayout.Space(4);

        EditorGUILayout.PropertyField(serializedObject.FindProperty("obtainedPowerups"));
        //serializedObject.FindProperty("obtainedPowerups");
        //EditorUtility.Show(serializedObject.FindProperty("obtainedPowerups"), false);

        GUILayout.Space(4);
        type = (GameManager.LastingPowerupType)EditorGUILayout.EnumPopup("Type of powerup to obtain", type);
        GUILayout.Space(4);
        if (GUILayout.Button("Gain the above powerup"))
        {
            gm.ObtainedPowerup(type);
        }
    }
}
*/