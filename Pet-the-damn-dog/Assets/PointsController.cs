using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PointsController : MonoBehaviour
{
    TextMeshProUGUI TotalLovePointsText;
    TextMeshProUGUI PPSText;

    float totalPoints;
    float pps;
    float multiplier = 1f;

    private void Awake()
    {
        SaveManager.onLoad.AddListener(LoadData);
    }

    // Start is called before the first frame update
    void Start()
    {
        TotalLovePointsText = GameObject.FindGameObjectWithTag("[UI]TotalLovePoints").GetComponent<TextMeshProUGUI>();
        PPSText = GameObject.FindGameObjectWithTag("[UI]PointsPerSecond").GetComponent<TextMeshProUGUI>();
        StartCoroutine(CalculatePointsPerSecond());
    }

    private void Update()
    {
        updateUI();
    }

    private void updateUI()
    {
        string drawTotalPoints = totalPoints.ToString();
        // UI Update.
        if (totalPoints > 1000)
        {
            drawTotalPoints = (totalPoints / 1000).ToString() + "k";
        }

        TotalLovePointsText.text = drawTotalPoints + " Love Points";
        PPSText.text = pps.ToString("0.0") + " PP/S (Pets per second)";
    }

    IEnumerator CalculatePointsPerSecond()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f);   // Allow us to wait for seconds and also not use new alot inside a loop.
        while (true)
        {
            addPointsToTotal(pps * multiplier);                     // Add ours points per second to the total points every second.
            yield return waitForSeconds;                            // Wait the 1 second.
        }
    }

    public void addPointsToTotal(float value) { 
        totalPoints += value;
        SaveManager.updateOrAddData<float>("playerTotalPoints", totalPoints);
    }
    public void minusPointsFromTotal(float value) { 
        totalPoints -= value;
        SaveManager.updateOrAddData<float>("playerTotalPoints", totalPoints);
    }
    public float getTotalPoints() { return totalPoints; }

    public void addPointsPerSecond(float value) { 
        pps += value;
        SaveManager.updateOrAddData<float>("playerTotalPPS", pps);
    }
    public float getPointsPerSecond() { return pps; }

    public void addMultiplier(float value)
    {
        multiplier += value;
        SaveManager.updateOrAddData<float>("playerMultiplier", multiplier);
    }
    public float getMultiplier() { return multiplier; }

    public void LoadData()
    {
        totalPoints = (float)SaveManager.getOrAddData<float>("playerTotalPoints", totalPoints);
        pps = (float)SaveManager.getOrAddData<float>("playerTotalPPS", pps);
        multiplier = (float)SaveManager.getOrAddData<float>("playerMultiplier", multiplier);
    }
}
