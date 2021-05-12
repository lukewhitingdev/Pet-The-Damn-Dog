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
    public float permMultiplier = 1;

    private void Awake()
    {
        permMultiplier = 1;
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
        SaveManager.updateOrAddData<float>("playerTotalPPS", (pps * multiplier * permMultiplier));
    }

    private string formatPoints(float points)
    {
        string drawTotalPoints = totalPoints.ToString();

        // UI Update.
        if (points > 1000)                                      // 1k
            drawTotalPoints = (points / 1000).ToString("0.0") + "k";
        if (points > 10000)                                     // 100k = 1K * 10
            drawTotalPoints = (points / 10000).ToString("0.0") + "K";
        if (points > 1000000)                                   // 1000K = 1m * 10
            drawTotalPoints = (points / 1000000).ToString("0.0") + "m";
        if (points > 10000000)                                  // 100m = 1M
            drawTotalPoints = (points / 100000000).ToString("0.0") + "M";
        if (points > 10000000000)                               // 1000M = 1b
            drawTotalPoints = (points / 100000000).ToString("0.0") + "b";
        if (points > 100000000000)                               // 1000M = 1b
            drawTotalPoints = (points / 100000000).ToString("0.0") + "B";

        return drawTotalPoints;
    }

    private void updateUI()
    {
        TotalLovePointsText.text = formatPoints(totalPoints) + " Love Points";
        PPSText.text = formatPoints(pps * multiplier * permMultiplier) + " PP/S (Pets per second)";
    }

    IEnumerator CalculatePointsPerSecond()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f);   // Allow us to wait for seconds and also not use new alot inside a loop.
        while (true)
        {
            addPointsToTotal(pps * multiplier * permMultiplier);    // Add ours points per second to the total points every second.
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
        pps = (float)SaveManager.getOrAddData<float>("playerTotalPPS", (pps * multiplier * permMultiplier));
        multiplier = (float)SaveManager.getOrAddData<float>("playerMultiplier", multiplier);
        permMultiplier = (float)SaveManager.getOrAddData<float>("playerPermMultiplier", permMultiplier);
    }
}
