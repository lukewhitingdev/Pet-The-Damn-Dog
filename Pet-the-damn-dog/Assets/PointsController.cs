using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsController : MonoBehaviour
{
    TextMeshProUGUI TotalLovePointsText;
    TextMeshProUGUI PPSText;

    float totalPoints;
    float pps;

    private void Awake()
    {
        // Setup stuff we want to be saved.
        SaveManager.addData<float>("playerTotalPoints", totalPoints);
        SaveManager.addData<float>("playerTotalPPS", pps);
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
        // UI Update.
        TotalLovePointsText.text = totalPoints.ToString("0.00") + " Love Points";
        PPSText.text = pps.ToString("0.0") + " PP/S (Pets per second)";
    }

    IEnumerator CalculatePointsPerSecond()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1.0f);   // Allow us to wait for seconds and also not use new alot inside a loop.
        while (true)
        {
            addPointsToTotal(pps);                                  // Add ours points per second to the total points every second.
            yield return waitForSeconds;                            // Wait the 1 second.
        }
    }

    public void addPointsToTotal(float value) { totalPoints += value; SaveManager.updateData<float>("playerTotalPoints", totalPoints); }
    public void minusPointsFromTotal(float value) { totalPoints -= value; SaveManager.updateData<float>("playerTotalPoints", totalPoints); }
    public float getTotalPoints() { return totalPoints; }

    public void addPointsPerSecond(float value) { pps += value; SaveManager.updateData<float>("playerTotalPPS", pps); }
    public float getPointsPerSecond() { return pps; }

    public void LoadData()
    {
        if(SaveManager.getData<float>("playerTotalPoints") != null)
            totalPoints = (float)SaveManager.getData<float>("playerTotalPoints");

        if (SaveManager.getData<float>("playerTotalPPS") != null)
            pps = (float)SaveManager.getData<float>("playerTotalPPS");
    }
}
