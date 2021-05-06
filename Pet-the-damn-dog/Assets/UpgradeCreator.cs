using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UpgradeCreator : EditorWindow
{
    public int test;
    static private GameObject potentialObject;
    static private ShopItem shopItem;
    static public SerializedObject serializedObject;

    private void Awake()
    {
        potentialObject = new GameObject();
        shopItem = potentialObject.AddComponent<ShopItem>();

        serializedObject = new SerializedObject(shopItem);
    }

    [MenuItem("PetTheDamnDog/Create Upgrade")]
    static public void spawnWindow()
    {
        GetWindow<UpgradeCreator>("Upgrade Creator");
    }

    private void OnGUI()
    {
		serializedObject.Update();

		SerializedProperty prop = serializedObject.GetIterator();
		if (prop.NextVisible(true))
		{
			do
			{
                if(prop.name != "verbose")
                {
                    Debug.Log(prop.name);
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                }
            }
			while (prop.NextVisible(false));
		}
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Create Upgrade"))
        {
            // TODO: Create upgrade prefab inside resource folder.

        }
    }
}
