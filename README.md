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
    // Adds the data to a list that is saved at the end of the session.
    public static void addData<T>(string id, object data);
    
    // Update a piece of data inside the list to be saved at the end of the session.
    public static void updateData<T>(string id, object overwriteData);
    
    // Returns the savedData within the loadedData list.
    public static object getData<T>(string id);
    
    // Returns the requested data from the Type (T) and identifier (id)
    public static object getData<T>(string id);
```


## References

Dog sprites from: https://angryelk.itch.io/animated-corgi-sprite
Hand sprite from: https://www.pinterest.co.uk/pin/329607266464141662/ (Dunno where this is actually from only link i could find was pintrest :(.)
