using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RollbackManager))]
public class RollbackEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RollbackManager dataContainer = (RollbackManager)target;

        int count = 0;
        foreach(List<ReverseAction> PlayerAction in dataContainer.ReverseActions)
        {
            GUILayout.Label("Player Move : " + count);
            foreach (ReverseAction action in PlayerAction)
            {
                switch(action.Type)
                {
                    case ReverseActionType.PlayerMove: // FAIT ?
                        if (action.ValueTarget != 0) GUILayout.Label("Player as " + action.ValueTarget + " Power.");
                        else{
                            GUILayout.Label("Player as Moved from " + action.PositionTarget);
                        }
                        break;
                    case ReverseActionType.PlayerTp: //FAIT
                        GUILayout.Label("Player as teleported " + action.PositionTarget);
                        break;
                    case ReverseActionType.MorePower: //FAIT
                        GUILayout.Label("Player got more power");
                        break;
                    case ReverseActionType.SeekerMove: //FAIT
                        if (action.ValueTarget != 0) GUILayout.Label("The seeker as " + action.ValueTarget + " Power.");
                        else GUILayout.Label("The seeker moved " + action.PositionTarget);
                        break;
                    case ReverseActionType.SoulDamage: //FAIT
                        GUILayout.Label("A soul as taken damage");
                        break;
                    case ReverseActionType.SoulTaken: //FAIT
                        GUILayout.Label("Player got trapped");
                        break;
                    case ReverseActionType.SoulTake: //FAIT
                        GUILayout.Label("Player Took a Soul");
                        break;
                    case ReverseActionType.Purify: //FAIT
                        GUILayout.Label("Player as Purify a soul");
                        break;
                }
            }
            EditorGUILayout.Space();
            count++;
        }
    }
}
