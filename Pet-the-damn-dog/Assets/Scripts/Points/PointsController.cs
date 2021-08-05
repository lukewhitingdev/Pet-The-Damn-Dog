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
    float prestigePoints = 0;
    float prestigePointThreshold = 150 * 100;    // Initially 15k.
    float prestigePointThresholdMultiplier = 2;  // Used to increment the prestige token generation.
    
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

    private void updateUI()
    {
        TotalLovePointsText.text = NumberFormatter.formatNumber(totalPoints) + " Love Points";
        PPSText.text = NumberFormatter.formatNumber(pps * multiplier * permMultiplier) + " PP/S (Pets per second)";
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

        if (totalPoints >= prestigePointThreshold)
        {
            addPrestigePoints(1.0f);
            prestigePointThreshold *= prestigePointThresholdMultiplier;
        }
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

    public void addPrestigePoints(float value)
    {
        prestigePoints += value;
        SaveManager.updateOrAddData<float>("prestigePoints", prestigePoints);
    }
    public void minusPrestigePoints(float value)
    {
        prestigePoints -= value;
        SaveManager.updateOrAddData<float>("prestigePoints", prestigePoints);
    }
    public float getPrestigePoints() { return prestigePoints; }

    public void prestige()
    {
        if(prestigePoints > 0)
        {
            permMultiplier += 1;
            minusPrestigePoints(1);
        }
    }

    public void LoadData()
    {
        totalPoints = (float)SaveManager.getOrAddData<float>("playerTotalPoints", totalPoints);
        pps = (float)SaveManager.getOrAddData<float>("playerTotalPPS", (pps * multiplier * permMultiplier));
        multiplier = (float)SaveManager.getOrAddData<float>("playerMultiplier", multiplier);
        permMultiplier = (float)SaveManager.getOrAddData<float>("playerPermMultiplier", permMultiplier);
        prestigePoints = (float)SaveManager.getOrAddData<float>("prestigePoints", prestigePoints);
    }
}
