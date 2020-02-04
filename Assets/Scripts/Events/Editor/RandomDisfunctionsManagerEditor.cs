using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomDisfunctionsManager))]
public class RandomDisfunctionsManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var manager = (RandomDisfunctionsManager) target;
        manager.targetModules = EditorGUILayout.MaskField("Target Modules", manager.targetModules, manager.options);
        base.DrawDefaultInspector();
    }
}
