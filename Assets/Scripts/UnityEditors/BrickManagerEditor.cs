/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BrickManagers))]
public class BrickManagerEditor : Editor
{
    BrickManagers bm;

    private void OnEnable()
    {
        bm = target as BrickManagers;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Get Comet"))
        {
            FindObjectOfType<GameManager>().ObtainedPowerup(GameManager.LastingPowerupType.Comet);
        }
        if (GUILayout.Button("Get Pierce"))
        {
            FindObjectOfType<GameManager>().ObtainedPowerup(GameManager.LastingPowerupType.Piercing);
        }
        if (GUILayout.Button("Get Kickbox"))
        {
            FindObjectOfType<GameManager>().ObtainedPowerup(GameManager.LastingPowerupType.BiggerKickbox);
        }
        if (GUILayout.Button("Next Level"))
        {
            FindObjectOfType<GameManager>().BeatLevel();
        }
    }
}*/
