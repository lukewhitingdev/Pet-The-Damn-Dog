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
    public static object addData<T>(string id, object data)
    
    // Used in loading functions to either get the already existing data or add it to be saved if it doesnt exist.
    public static object getOrAddData<T>(string id, object potentialData)
    
    // Used when updating data. adds the data to the save stack if it doesnt already exist from the load stack.
    public static void updateOrAddData<T>(string id, object overwriteData)
    
    // Used to get data from the load stack.
    public static object getData<T>(string id)
    
    // Used to check if data exists in load stack.
    public static bool checkIfDataExists<T>(string id)
```


## References
Dog sprites from: https://angryelk.itch.io/animated-corgi-sprite
