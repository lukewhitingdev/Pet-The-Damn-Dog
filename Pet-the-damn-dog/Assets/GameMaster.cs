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
    }

    // Clicking.
    private float clickPower = 1.0f;
    public void click()
    {
        pointsController.addPointsToTotal(clickPower);
        petPointController.spawnPetPoint(clickPower);
    }
    public void addClickPower(float value) { clickPower += value; }
    public float getClickPower() { return clickPower; }

}
