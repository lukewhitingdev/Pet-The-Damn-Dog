using SFB;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioManager : EditorWindow
{

    AudioController audioController;
    List<AudioController.Audio> audios;
    List<AudioController.Audio> stagedRemoveAudios;
    Vector2 scrollPos;

    [MenuItem("PetTheDamnDog/Audio/Manage Audio")]
    static public void spawnWindow()
    {
        GetWindow<AudioManager>("Audio Manager").minSize = new Vector2(500, 200);
    }

    public void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
        audios = audioController.loadAudios("Audio/test123");
        stagedRemoveAudios = new List<AudioController.Audio>();
    }

    private void OnGUI()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        foreach (var audio in audios)
        {
            if(!stagedRemoveAudios.Exists(x => x.id == audio.id))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ID:");
                EditorGUILayout.LabelField(audio.id);
                if (GUILayout.Button("Delete"))
                {
                    removeAudio(audio);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();

        if(stagedRemoveAudios.Count > 0)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(string.Format("Undo [{0}] deletes", stagedRemoveAudios.Count)))
            {
                stagedRemoveAudios.Clear();
            }
            if (GUILayout.Button("Save"))
            {
                audioController.saveAudios("Audio/test123.json");
                Close();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void removeAudio(AudioController.Audio audio)
    {
        Debug.Log(string.Format("Removing {0}", audio.id));
        stagedRemoveAudios.Add(audio);  // So we dont show the removed audio in the UI again.
        
        // Have to loop through the list since it doesnt seem to remove the audio parameter if we just use audioController.audios.remove(audio).
        foreach (var audioItem in audioController.audios)
        {
            if(audioItem.id == audio.id)
            {
                audioController.audios.Remove(audioItem);
                break;
            }
        }
    }
}
