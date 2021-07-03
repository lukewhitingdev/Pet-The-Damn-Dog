using SFB;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioCreator : EditorWindow
{

    string[] paths; // Holds the paths from the explorer window.
    private int iterator;   // Used to loop through all the staged changes when giving new audio its id's
    private string currentID = "N/A";   // Used to give audios their id (defaulted to N/A so the user has to change it)
    private bool editingDone = false;   // Used to tell the GUI to move onto its saving display rather than keep looping.
    Dictionary<string, string> audiosToBeCreated = new Dictionary<string, string>();    // Used to hold all the audios that are going to be written into JSON once saved.

    ExtensionFilter[] extensions = new[] {
        new ExtensionFilter("Sound Files", "mp3", "wav")
    };

    public void Awake()
    {
        audiosToBeCreated.Clear();
        iterator = 0;
    }

    [MenuItem("PetTheDamnDog/Create Audio")]
    static public void spawnWindow()
    {
        GetWindow<AudioCreator>("Audio Creator").minSize = new Vector2(1280, 150);
    }

    private void OnGUI()
    {
        // Initialization Phase.
        if(paths == null)
        {
            EditorGUILayout.LabelField("Path to audio:");
            if (GUILayout.Button("Find in explorer"))
            {
                paths = findPathInExplorer();
            }
        }

        // Editing Phase.
        if (paths != null && editingDone == false)
        {
            EditorGUILayout.HelpBox(string.Format("Editing Phase. Editing {0} of {1} selections", iterator, paths.Length), MessageType.Info);
            EditorGUILayout.HelpBox("Please input a ID that is not N/A or used in previous iterations", MessageType.Error);

            string chopToResources = paths[iterator].Substring(paths[iterator].IndexOf("Resources"), paths[iterator].Length - paths[iterator].IndexOf("Resources"));
            int afterResourcesIndex = chopToResources.IndexOf(@"\");
            string currentPath = chopToResources.Substring(afterResourcesIndex + 1, chopToResources.Length - afterResourcesIndex - 1);
            currentPath = currentPath.Remove(currentPath.Length - 4, 4);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID:");
            EditorGUILayout.LabelField("Path:");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            currentID = EditorGUILayout.TextField(currentID);
            EditorGUILayout.LabelField(currentPath);
            EditorGUILayout.EndHorizontal();

            if(currentID != "N/A")
            {
                if (GUILayout.Button("Save audio settings"))
                {
                    audiosToBeCreated.Add(currentID, currentPath);
                    if(iterator < paths.Length - 1)
                    {
                        currentID = "N/A";   
                        iterator++;
                    }
                    else
                    {
                        editingDone = true;
                    }
                }
            }
        }

        // Saving Phase.
        if (audiosToBeCreated.Count > 0 && editingDone)
        {
            EditorGUILayout.HelpBox("Saving Phase. Please check over your inputs to make sure they are correct.", MessageType.Info);

            foreach (var audio in audiosToBeCreated)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ID:");
                EditorGUILayout.LabelField("Path:");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(audio.Key);
                EditorGUILayout.LabelField(audio.Value);
                EditorGUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Save audios"))
            {
                saveAudios();
            }
        }
    }

    private string[] findPathInExplorer()
    {
        return StandaloneFileBrowser.OpenFilePanel("Open Audio File", Application.dataPath + "/Resources/", extensions, true);
    }

    private void saveAudios()
    {
        AudioController audioController = FindObjectOfType<AudioController>();

        foreach (var item in audiosToBeCreated)
        {
            AudioController.Audio audio = new AudioController.Audio();
            audio.id = item.Key;
            audio.path = item.Value;
            audioController.audios.Add(audio);
        }

        audioController.saveAudios("Audio/" + audioController.savedAudioName + ".json");

        Close();
    }
}
