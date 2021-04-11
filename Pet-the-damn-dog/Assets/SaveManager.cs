using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager
{
    [Serializable]
    struct Data
    {
        public Data(string id, Type type, object data)
        {
            identifier = id;
            dataType = type;
            saveData = data;
        }
        public string identifier;
        public Type dataType;
        public object saveData;
    }

    private static List<Data> saveDataList = new List<Data>();
    private static List<Data> loadedDataList = new List<Data>();

    public static void addData<T>(string id, object data)
    {
        // Check if the item already exists.
        foreach (var item in saveDataList)
        {
            if(item.identifier == id)
            {
                throw new Exception("Tried to add data to saveData list with a identifier(" + id + ") that already exists!");
            }
        }

        //Debug.LogFormat("Added data! | id: {0}, data: {1}", id, data);

        Data saveData = new Data(id, typeof(T), data);
        saveDataList.Add(saveData);
    }

    public static void updateData<T>(string id, object overwriteData)
    {
        bool exists = false;
        for (int i = 0; i < saveDataList.Count; i++)
        {
            if (saveDataList[i].identifier == id)
            {
                exists = true;
                saveDataList[i] = new Data(id, typeof(T), overwriteData);
                Debug.LogFormat("Updated data! | id: {0}, data: {1}", id, saveDataList[i].saveData);
            }
        }

        if (!exists)
        {
            throw new Exception("Tried to update data in saveData list with a identifier(" + id + ") that doesnt exists!");
        }
    }

    public static object getData<T>(string id)
    {
        //Debug.Log("Getting data | ID: " + id);
        return loadedDataList.Find(x => x.identifier == id).saveData;
    }

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.ptdd";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveDataList);

        Debug.Log("Saving!");

        foreach (var item in saveDataList)
        {
            Debug.LogFormat("Saving data | id: {0}, data: {1} |", item.identifier, item.saveData);
        }

        stream.Close();
    }

    public static bool Load()
    {
        string path = Application.persistentDataPath + "/playerData.ptdd";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            loadedDataList = formatter.Deserialize(stream) as List<Data>;

            Debug.Log("Loading!");

            foreach (var item in loadedDataList)
            {
                Debug.LogFormat("Loading data | id: {0}, data: {1} |", item.identifier, item.saveData);
            }

            return true;
        }
        else
        {
            throw new Exception("Save file not found when loading! Path: " + path);
        }
    }
}
