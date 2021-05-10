using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class UpgradeCreator : EditorWindow
{
    private GameObject prefabHolder;
    private string prefabHolderName = "[UpgradeCreator] Prefab Holder";
    private GameObject prefab;
    private ShopItem prefabShopItem;
    private SerializedObject serializedObject;
    private Dictionary<string, System.Type> upgradeProperties = new Dictionary<string, System.Type>();

    [MenuItem("PetTheDamnDog/Create Upgrade")]
    static public void spawnWindow()
    {
        GetWindow<UpgradeCreator>("Upgrade Creator");
    }

    private void initProperties()
    {
        upgradeProperties.Add("largeUpgrade", typeof(bool));
        upgradeProperties.Add("oneTime", typeof(bool));
        upgradeProperties.Add("totalClickPower", typeof(float));
        upgradeProperties.Add("totalPPS", typeof(float));
        upgradeProperties.Add("multiplier", typeof(float));
    }

    private void Awake()
    {
        initProperties();
        initPrefabObject();
    }

    private void OnGUI()
    {
        if (serializedObject == null || prefab == null)
        {
            initPrefabObject();
        }

		serializedObject.Update();

		SerializedProperty prop = serializedObject.GetIterator();
		if (prop.NextVisible(true))
		{
			do
			{
                // Hide specific properties we dont want to view.
                if(prop.name != "verbose" && prop.name != "m_Script")
                {
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(prop.name), true);
                }
            }
			while (prop.NextVisible(false));
		}
		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Create Upgrade"))
        {
            createUpgrade();
        }
    }

    private void OnDestroy()
    {
        if (prefab)
            DestroyImmediate(prefab);

        if (prefabHolder)
            DestroyImmediate(prefabHolder);
    }

    // Creates a upgrade with the properties of the displayed fields.
    private void createUpgrade()
    {
        // Automatically applies the properties of our serializedObject to our prefab component.
        foreach (var UpgradeProperty in upgradeProperties)
        {
            FieldInfo property = typeof(ShopItem).GetField(UpgradeProperty.Key); // Gets all the specific field by name. Identical to the way that you get the property in a serializedObject.

            // Set the value of the property depending on its type. Would be nicer if serializedProperties could be casted but this works for now :).
            if (UpgradeProperty.Value == typeof(bool))
                property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).boolValue);

            if (UpgradeProperty.Value == typeof(float))
                property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).floatValue);

            if (UpgradeProperty.Value == typeof(int))
                property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).intValue);

            if (UpgradeProperty.Value == typeof(double))
                property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).doubleValue);
        }

        // Save the asset.
        bool success = false;
        PrefabUtility.SaveAsPrefabAsset(prefab, Application.dataPath + "/Resources/Shop/Upgrades/" + prefabShopItem.upgradeName + ".prefab", out success);

        if (!success)
            Debug.LogError("Saving Prefab Failed! Please try again.");
    }

    private void initPrefabObject()
    {
        // Make sure we dont already have a object in the scene.
        if (prefab)
            DestroyImmediate(prefab);

        prefabHolder = GameObject.Find(prefabHolderName);
        if(!prefabHolder)
            prefabHolder = new GameObject(prefabHolderName);

        Transform[] oldPrefabs = prefabHolder.GetComponentsInChildren<Transform>();

        foreach (var item in oldPrefabs)
        {
            if (item.name != prefabHolderName)
                DestroyImmediate(item.gameObject);
        }

        prefab = new GameObject("PossibleUpgrade");
        prefab.transform.parent = prefabHolder.transform;
        prefab.tag = "[ShopUI]Item";
        prefab.layer = 5; // UI.
        prefab.AddComponent<RectTransform>();
        prefabShopItem = prefab.AddComponent<ShopItem>();
        serializedObject = new SerializedObject(prefabShopItem);
    }
}
