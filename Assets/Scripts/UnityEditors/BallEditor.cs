using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(BallBehavior))]
public class BallEditor : Editor
{
    private BallBehavior BBehav;
    private float launchDirX;
    private float launchDirY;
    private bool showButtons = false;
    

    private void OnEnable()
    {
        BBehav = target as BallBehavior;

    }
    public override void OnInspectorGUI()
    {
        //draw the default inspector
        base.OnInspectorGUI();
        GUILayout.Space(4);
        BBehav.moveSpeed = EditorGUILayout.FloatField("Speed", BBehav.moveSpeed);
        GUILayout.Space(4);
        
        GUILayout.Space(4);

        showButtons = EditorGUILayout.Foldout(showButtons, "Launching Buttons");
        if (showButtons)
        {
            EditorGUILayout.LabelField("X and Y direction for launching the ball, in that order"
                , EditorStyles.boldLabel);
            launchDirX = EditorGUILayout.Slider(launchDirX, -1, 1);
            launchDirY = EditorGUILayout.Slider(launchDirY, -1, 1);
            
            GUILayout.Space(4);

            if (GUILayout.Button("Launch in the direction specified above"))
            {
                BBehav.Launch(new Vector2(launchDirX, launchDirY));
            }
            GUILayout.Space(4);

            if (GUILayout.Button("Launch In Random Direction"))
            {
                BBehav.Launch();
            }
            GUILayout.Space(4);

            if (GUILayout.Button("Stop Ball"))
            {
                BBehav.StopBall();
            }
            
        }
        
        
    }
}
