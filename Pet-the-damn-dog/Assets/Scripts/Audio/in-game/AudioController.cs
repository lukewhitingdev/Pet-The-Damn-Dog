using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    [Serializable]
    public class Audio
    {
        public string id;
        public string path;
        public string mixerGroupID;
    }

    public class Sound
    {
        public Audio audio;
        public AudioSource source;
        public AudioMixerGroup mixerGroup;
    }

    [Serializable]
    public class ListWrapper<T>
    {
        public List<T> wrappedList;
    }

    /*
        Problems:
        Need a way to save audio that we are using to disk. so we can edit them later.
        Need a way to easily add audio clips probs similar to the UpgradeCreator.cs
        Need a way to reference and activate specific audio clips that are pre-loaded on scene load.
     
        Ideal Solution:
        Loads all sounds from file -> When we want them we call the AudioController.search("[AudioID]").play to play.
     */

    public List<Audio> audios = new List<Audio>(); // Audio clips we want to use in the game. (Used for saving)
    private List<Sound> sounds = new List<Sound>(); // Audio's that are linked to instanciated audioSources in-game (Used for reference at run-time).

    // Config
    //private string savedAudioName = "audiosJSON";
    public string savedAudioName = "test123";


    /*  
     *  Used to load audios from a json file.
     *  Steps:
     *  1. Loads the textAsset in the resources folder. (JSON file in audio folder)
     *  2. Converts that from JSON back into the wrapper class it was serialized into.
     *  3. Get the wrapper classes' wrappedList which contains the list we originally wanted to use.
     *  4. Returns the wrappedList back to user. 
     */

    public List<Audio> loadAudios(string path){ return JsonUtility.FromJson<ListWrapper<Audio>>(Resources.Load<TextAsset>(path).text).wrappedList; }

    /*  
     *  Used to save audios to a json file.
     *  Steps:
     *  1. Creates a new ListWrapper object whose wrappedList variable is the list we want to save.
     *  2. Convert that ListWrapper to JSON using JSONUtility.
     *  3. Write to a JSON file in the resources folder for storage. (Will overwrite everytime since we have no need for old data)
     */

    public void saveAudios(string path) { File.WriteAllText(Application.dataPath + "/Resources/" + path, JsonUtility.ToJson(new ListWrapper<Audio>() { wrappedList = audios }, true)); }

    private void createSounds()
    {
        foreach (var audio in audios)
        {
            GameObject soundObject = new GameObject(audio.id);
            soundObject.transform.parent = this.transform;
            Sound sound = new Sound();
            sound.audio = audio;
            sound.source = soundObject.AddComponent<AudioSource>();
            sound.source.playOnAwake = false;
            try
            {
                sound.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups(audio.mixerGroupID)[0];
            }
            catch (Exception)
            {
                Debug.LogWarning("[AudioCreation] Creating audio without corresponding mixer! Make sure the mixer exists before creating the sounds!");
            }
            sound.source.clip = Resources.Load<AudioClip>(audio.path);
        }
    }

    // Used to get a sound that is instanciated in the scene.
    public AudioSource getSound(string id){ return sounds.Find(x => x.audio.id == id).source; }

    private void Awake()
    {
        audios = loadAudios("Audio/" + savedAudioName);

        createSounds();

        // TODO: Make this into a editor window so we can select our sources and give them names.
        //audios.Add(new Audio() { id = "test", path = "Audio/Sources/" + "test" });

        //audios.Clear();
    }

    private void OnApplicationQuit()
    {
        saveAudios("Audio/" + savedAudioName + ".json");
    }
}
