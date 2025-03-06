using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataLevelContainer))]
public class DataLevelEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DataLevelContainer dataContainer = (DataLevelContainer)target;

        if (GUILayout.Button("Reset levels data"))
        {
            dataContainer.ResetData();
        }
        if (GUILayout.Button("Complete levels data"))
        {
            dataContainer.Complete();
        }
    }
}
