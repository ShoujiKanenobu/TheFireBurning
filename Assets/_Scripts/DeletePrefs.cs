using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DeletePrefs : EditorWindow
{
    [MenuItem("Window/Prefs")]
    public static void ShowWindow()
    {
        GetWindow<DeletePrefs>("DeletePrefs");
        
    }

    private void OnGUI()
    {
        GUILayout.Label("Player Prefs Reset", EditorStyles.boldLabel);
        
        if(GUILayout.Button("Reset High Score"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
