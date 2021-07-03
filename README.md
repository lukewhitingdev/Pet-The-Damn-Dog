# PTDD
Pet the damn dog weekend prototype


## Highlight areas
### [Saving and loading system](https://github.com/lukewhitingdev/PTDD/blob/main/Pet-the-damn-dog/Assets/SaveManager.cs)
#### Reasoning
I wanted to create a saving system that was easy to use and didnt require you to create extra scripts just to save data.
I used generic type parameters for multiple reasons:
  1. To allow more freedom with id's. This is because the script checks if the id exists but also if the data that is attached to that id is of the same type as the comparison. So      you can have 2 identical id's that return two different pieces of data without accidental contamination.
  2. To give the user more upfront indication of what they are saving so when they are using the get() method they dont accidentally grab the wrong id because they used a incorect      type.
#### Code
```C#
// Used to add data to the save stack.
public static object addData<T>(string id, object data);

// Used in loading functions to either get the already existing data or add it to be saved if it doesnt exist.
public static object getOrAddData<T>(string id, object potentialData);

// Used when updating data. adds the data to the save stack if it doesnt already exist from the load stack.
public static void updateOrAddData<T>(string id, object overwriteData);

// Used to get data from the load stack.
public static object getData<T>(string id);

// Used to check if data exists in load stack.
public static bool checkIfDataExists<T>(string id);
```

### [Upgrade Creator](https://github.com/lukewhitingdev/PTDD/blob/be838ff4bd87854077fa099455afdcdded71c581/Pet-the-damn-dog/Assets/UpgradeCreator.cs)
#### Reasoning
I wanted a easy way to create the content for the idle game. Since idle games content mainly comes from the upgrade you can use to enrich your experience and progress through the game I thought that a easy way to create the content on the dev-side of things was a must.


#### Specific Code Snippets

This particular is used when spawning the new upgrade prefab. I wanted a way of allowing flexibility when extending the shopItem script that all upgrades use whilst also keeping the modification to this script minimal.

The way I found to do it was to use a serializedObject of the shopItem script and use Type.GetField() method to dynamically get all the fields that where inside the shopItem script. Since the serializedObject and the (to be created) prefab's shopItem script had the same fields I could just loop through them all and apply the values as we go. 

Allowing for the upgradeCreator script to dynamically handle any additions or deletions to shopItem fields.
```C#
// Automatically applies the properties of our serializedObject to our prefab component.
foreach (var UpgradeProperty in upgradeProperties)
{
    // Gets all the specific field by name. Identical to the way that you get the property in a serializedObject.
    FieldInfo property = typeof(ShopItem).GetField(UpgradeProperty.Key); 

    // Set the value of the property depending on its type. Would be nicer if serializedProperties could be casted but this works.
    if (UpgradeProperty.Value == typeof(bool))
        property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).boolValue);

    if (UpgradeProperty.Value == typeof(float))
        property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).floatValue);

    if (UpgradeProperty.Value == typeof(int))
        property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).intValue);

    if (UpgradeProperty.Value == typeof(double))
        property.SetValue(prefabShopItem, serializedObject.FindProperty(UpgradeProperty.Key).doubleValue);
}
```

## Resources Used.
Dog sprites from: https://angryelk.itch.io/animated-corgi-sprite

Unity File explorer from: https://github.com/gkngkc/UnityStandaloneFileBrowser
