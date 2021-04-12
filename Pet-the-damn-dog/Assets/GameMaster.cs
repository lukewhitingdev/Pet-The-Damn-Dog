using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{

    // Singleton.
    static private PointsController pointsController;
    static private PetPointController petPointController;
    static public GameMaster instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (pointsController == null)
            pointsController = FindObjectOfType<PointsController>();

        if (petPointController == null)
            petPointController = FindObjectOfType<PetPointController>();

        if (SaveManager.Load())
        {

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

            dateTimeDiff = (System.DateTime.Now.AddDays(5) - loadedDateTime).TotalSeconds;

            pointsController.addPointsToTotal(pointsController.getPointsPerSecond() * (float)dateTimeDiff);

            Debug.Log("Time Diff: " + dateTimeDiff);

            pointsController.LoadData();
        };
    }

    private double dateTimeDiff = 0;

    private void Start()
    {
        // Setup stuff we want to be saved.
        SaveManager.addData<float>("playerClickPower", clickPower);
        SaveManager.addData<System.DateTime>("dateTime", System.DateTime.Now);
    }

    // Clicking.
    private float clickPower = 1.0f;
    public void click()
    {
        pointsController.addPointsToTotal(clickPower);
        petPointController.spawnPetPoint(clickPower);
    }
    public void addClickPower(float value) { clickPower += value; SaveManager.updateData<float>("playerClickPower", clickPower); }
    public float getClickPower() { return clickPower; }

    // Pets Per Second.
    public void addPetsPerSecond(float value) { pointsController.addPointsPerSecond(value); }


    private void OnApplicationQuit()
    {
        // Save our stuff.
        SaveManager.Save();
    }

}
