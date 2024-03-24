using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Unity.VisualScripting.Member;

[CanEditMultipleObjects]
[CustomEditor(typeof(BrickBehavior))]
public class BrickBehaviorEditor : Editor
{
    private BrickBehavior brickBehav;
    private bool showRefrences = false;
    private bool showDebug = false;
    private bool showButtons = false;

    private SerializedProperty KeyBrickSprite;
    private SerializedProperty NormalBrickSprite;

    private void OnEnable()
    {
        brickBehav = target as BrickBehavior;
        KeyBrickSprite = serializedObject.FindProperty("KeyBrickSprite");
        NormalBrickSprite = serializedObject.FindProperty("NormalBrickSprite");
    }
    public override void OnInspectorGUI()
    {
        //draw the default inspector
        //base.OnInspectorGUI();

        brickBehav.heldPowerup = (GameManager.PowerupType)EditorGUILayout.EnumPopup("Powerup held in this brick", brickBehav.heldPowerup);

        showButtons = EditorGUILayout.Foldout(showButtons,
            "Buttons");
        if (showButtons)
        {
            if (GUILayout.Button("Destroy this brick"))
            {
                brickBehav.DestroyThisBrick();
            }
            GUILayout.Space(4);

            if (GUILayout.Button("Drop current powerup"))
            {
                brickBehav.DropPowerup();
            }
        }

        GUILayout.Space(4);

        showRefrences = EditorGUILayout.Foldout(showRefrences,
            "Refrences");
        if (showRefrences)
        {
            brickBehav.KeyBrickSprite = (Sprite)EditorGUILayout.ObjectField("Key Brick Sprite", brickBehav.KeyBrickSprite, typeof(Sprite), 
                false, GUILayout.Height(EditorGUIUtility.singleLineHeight));

            brickBehav.NormalBrickSprite = (Sprite)EditorGUILayout.ObjectField("Normal Brick Sprite", brickBehav.NormalBrickSprite, typeof(Sprite),
                false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        }

        GUILayout.Space(4);

        showDebug = EditorGUILayout.Foldout(showDebug,
            "Debug");
        if (showDebug)
        {
            brickBehav.isKey = EditorGUILayout.Toggle("isKey",brickBehav.isKey);
        }
    }
}
