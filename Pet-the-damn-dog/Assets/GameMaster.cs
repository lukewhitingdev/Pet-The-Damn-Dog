using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    // Singleton.
    static GameMaster instance;
    static public GameMaster getInstance()
    {
        if (instance == null)
            instance = new GameMaster();

        return instance;
    }

    float clickPower;

    public void addClickPower(float value) { clickPower += value; }
    public float getClickPower() { return clickPower; }

}
