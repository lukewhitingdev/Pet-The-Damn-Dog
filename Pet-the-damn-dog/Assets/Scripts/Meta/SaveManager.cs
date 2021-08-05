using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Events;

public static class SaveManager
{

    public static bool verbose = false;                                             // Dictate whether debug logs are printed.
    static public UnityEvent onLoad = new UnityEvent();

    [Serializable]
    struct Data
    {
        public Data(string id, Type type, ref object data)
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

    public static object addData<T>(string id, object data)
    {
        // Check if the item already exists.
        if(loadedDataList.Exists(x => x.identifier == id && x.saveData.GetType() == typeof(T)))
        {
            if(verbose)
                Debug.LogError("[AddData] Tried to add data for id that already exists! Please dont do this");

            return null;
        }

        //Debug.LogFormat("Added data! | id: {0}, data: {1}", id, data);

        Data saveData = new Data(id, typeof(T), ref data);
        saveDataList.Add(saveData);

        if (verbose)
            Debug.LogFormat("[AddData] Added data! | id: {0}, data: {1}", id, data);

        return saveData.saveData;
    }

    public static object getOrAddData<T>(string id, object potentialData)
    {
        if (checkIfDataExists<T>(id))
        {
            return getData<T>(id);
        }
        else
        {
            return addData<T>(id, potentialData);
        }
    }

    public static void updateOrAddData<T>(string id, object overwriteData)
    {
        bool exists = false;
        for (int i = 0; i < saveDataList.Count; i++)
        {
            if (saveDataList[i].identifier == id && saveDataList[i].saveData.GetType() == typeof(T))
            {
                exists = true;
                saveDataList[i] = new Data(id, typeof(T), ref overwriteData);
                if (verbose)
                    Debug.LogFormat("[UpdateData] Updated data! | id: {0}, data: {1}", id, saveDataList[i].saveData);

                break;
            }
        }

        if (!exists)
        {
            addData<T>(id, overwriteData);
            return;
        }
    }

    public static object getData<T>(string id)
    {
        if (verbose)
            Debug.LogFormat("[GetData] Got data! | id: {0}, data: {1}", id, loadedDataList.Find(x => x.identifier == id && x.saveData.GetType() == typeof(T)).saveData);

        return loadedDataList.Find(x => x.identifier == id && x.saveData.GetType() == typeof(T)).saveData;
    }

    public static bool checkIfDataExists<T>(string id)
    {
        return loadedDataList.Exists(x => x.identifier == id && x.saveData.GetType() == typeof(T));
    }

    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.ptdd";

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveDataList);

        if (verbose)
            Debug.Log("[SaveData] Saving!");

        if (verbose)
        {
            foreach (var item in saveDataList)
            {
                Debug.LogFormat("[SaveData] Saving data | id: {0}, data: {1} |", item.identifier, item.saveData);
            }
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

            Debug.Log("[LoadData] Loading!");

            if (verbose)
            {
                foreach (var item in loadedDataList)
                {
                    Debug.LogFormat("[LoadData] Loading data | id: {0}, data: {1} |", item.identifier, item.saveData);
                }
            }

            saveDataList = loadedDataList;

            onLoad.Invoke();

            return true;
        }
        else
        {
            throw new Exception("[LoadData] Save file not found when loading! Path: " + path);
        }
    }
}
