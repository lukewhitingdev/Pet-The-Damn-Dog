using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    GameObject contentObject;

    public void Awake()
    {
        contentObject = GameObject.FindGameObjectWithTag("[Options]Content");
    }
}
