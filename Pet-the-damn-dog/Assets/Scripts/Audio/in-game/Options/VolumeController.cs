using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public string VolumeID;
    private AudioMixer audioMixer;
    private Slider slider;

    public void Awake()
    {
        audioMixer = FindObjectOfType<AudioController>().audioMixer;
        slider = GetComponentInChildren<Slider>();
    }

    public void changeVolume()
    {
        audioMixer.SetFloat(VolumeID, Mathf.Log10(slider.value) * 20);
    }
}
