using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradeCreator))]
public class UpgradeCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //UpgradeCreator myTarget = (UpgradeCreator)target;
        EditorGUILayout.HelpBox("This creates a prefab in the resources folder.", MessageType.Info);

        if (GUILayout.Button("Create"))
        {
            Debug.Log("Create upgrade");
        }
    }
}
