using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Soul))]
public class SoulEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Soul soul = (Soul)target;

        if (GUILayout.Button("Init"))
        {
            soul.UpdateAnimator();
        }
    }
}
