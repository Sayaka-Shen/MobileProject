using log4net.Core;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor
{
    static bool _isInScene = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Level level = (Level)target;

        EditorGUILayout.Space();

        if (!Application.isPlaying)
        {
            GUILayout.Label("Start Game to Edit");
            _isInScene = false;
            return;
        }
        else if (!_isInScene)
        {
            if (GUILayout.Button("Instantiate Level"))
            {
                _isInScene = true;
                PrefabUtility.InstantiatePrefab(level);
            }
            return;
        }
        else if (level.AsChoose) GUILayout.Label("Selected : " + level.ChoosenName);
        else GUILayout.Label("Selected : Nothing");

        EditorGUILayout.Space();
        if (GUILayout.Button("Select Obstacle"))
        {
            level.ChooseObstacle();
        }
        if (GUILayout.Button("Select OneWay"))
        {
            level.ChooseOneWay();
        }
        if (GUILayout.Button("Select River"))
        {
            level.ChooseRiver();
        }
        if (GUILayout.Button("Select Portal"))
        {
            level.ChoosePortal();
        }
        if (GUILayout.Button("Select Altar"))
        {
            level.ChooseAltar();
        }
        if (GUILayout.Button("Select AddMorePower"))
        {
            level.ChooseAddMorePower();
        }
        if (GUILayout.Button("Select Soul"))
        {
            level.ChooseSoul();
        }
        if (GUILayout.Button("Select SoulSeeker"))
        {
            level.ChooseSoulSeeker();
        }
        if (GUILayout.Button("Select SoulTrapper"))
        {
            level.ChooseSoulTrapper();
        }
        if (GUILayout.Button("Select Player"))
        {
            level.ChoosePlayer();
        }

        EditorGUILayout.Space();
        if (GUILayout.Button("Unselect"))
        {
            level.ChooseNothing();
        }
    }
}