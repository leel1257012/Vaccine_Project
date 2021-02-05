using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelEditor))]
public class LevelInspector : Editor
{
    public LevelEditor current
    {
        get
        {
            return (LevelEditor)target;
        }
    }

    public override void OnInspectorGUI()
    {
        string temp = "";
        DrawDefaultInspector();

        if (GUILayout.Button("New Level")) current.NewLevel();
        if (GUILayout.Button("Reset Level")) current.resetLevel();
        if (GUILayout.Button("Save")) current.Save();
        if (GUILayout.Button("Load")) current.LoadLevel();
        GUILayout.TextField(temp);

        
        //if(GUI.changed) 
    }
}
