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

    private PointsController pointsController;
    private PetPointController petPointController;

    private void Awake()
    {
        pointsController = FindObjectOfType<PointsController>();
        petPointController = FindObjectOfType<PetPointController>();
    }

    // Clicking.
    float clickPower = 1.0f;
    public void click()
    {
        pointsController.addPointsToTotal(clickPower);
        petPointController.spawnPetPoint(clickPower);
    }
    public void addClickPower(float value) { clickPower += value; }
    public float getClickPower() { return clickPower; }

}
