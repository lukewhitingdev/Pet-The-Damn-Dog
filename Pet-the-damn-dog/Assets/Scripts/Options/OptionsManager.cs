using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    private AudioController audioController;

    public void Awake()
    {
        audioController = FindObjectOfType<AudioController>();
    }

    public void toggleOptionsMenu()
    {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
        audioController.getSound("click").Play();
    }
}
