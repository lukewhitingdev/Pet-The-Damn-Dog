using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    // Singleton.
    static private PointsController pointsController;
    static private PetPointController petPointController;
    static public GameMaster instance;

    private double dateTimeDiff = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (pointsController == null)
            pointsController = FindObjectOfType<PointsController>();

        if (petPointController == null)
            petPointController = FindObjectOfType<PetPointController>();

        if (SaveManager.Load()) {
            Load();
        };

        pointsController.permMultiplier = 1;
    }

    private void Load()
    {
        // Setup stuff we want to be saved.
        clickPower = (float)SaveManager.getOrAddData<float>("playerClickPower", clickPower);

        if (SaveManager.checkIfDataExists<System.DateTime>("dateTime"))
            SaveManager.addData<System.DateTime>("dateTime", System.DateTime.Now);

        // Setup stuff we want to load.
        if (SaveManager.checkIfDataExists<float>("playerClickPower"))
        {
            clickPower = (float)SaveManager.getData<float>("playerClickPower");
        }

        System.DateTime loadedDateTime = System.DateTime.Now;

        if (SaveManager.checkIfDataExists<System.DateTime>("dateTime"))
        {
            Debug.Log("Loaded from idle state");
            loadedDateTime = (System.DateTime)SaveManager.getData<System.DateTime>("dateTime");
        }

        dateTimeDiff = (System.DateTime.Now - loadedDateTime).TotalSeconds;

        if (dateTimeDiff > 0)
        {
            if (SaveManager.checkIfDataExists<float>("playerTotalPPS"))
                pointsController.addPointsToTotal((float)SaveManager.getData<float>("playerTotalPPS") * (float)dateTimeDiff);
        }
    }

    public void prestige() { pointsController.prestige(); }

    // Clicking.
    private float clickPower = 1.0f;
    public void click()
    {
        pointsController.addPointsToTotal(clickPower);
        petPointController.spawnPetPoint(clickPower);
    }
    public void addClickPower(float value) {
        clickPower += value;
        SaveManager.updateOrAddData<float>("playerClickPower", clickPower);
    }
    public float getClickPower() { return clickPower; }

    // Pets Per Second.
    public void addPetsPerSecond(float value) { pointsController.addPointsPerSecond(value); }


    private void OnApplicationQuit()
    {
        // Save our stuff.
        SaveManager.Save();
    }

}
