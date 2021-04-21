using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopItem : MonoBehaviour
{
    private PointsController pointsController;

    [HideInInspector]
    public string upgradeName = "N/A";

    [Header("Upgrade Costs")]
    public float price = 1.0f;

    [HideInInspector]
    public int level = 1;

    [Header("Upgrade values")]
    public float totalPoints;
    public float totalPPS;

    private void Start()
    {
        upgradeName = this.gameObject.name;
        upgradeName = upgradeName.Substring(0, upgradeName.IndexOf("(Clone)"));
        Load();
    }

    public void Load()
    {
        upgradeName = this.name;
        if (upgradeName.Contains("(Clone)"))
        {
            upgradeName = upgradeName.Substring(0, upgradeName.IndexOf("(Clone)"));
        }
        Debug.LogFormat("Loading ShopItem {0}", upgradeName);
        price = (float)SaveManager.getOrAddData<float>(upgradeName + "-price", price);
        level = (int)SaveManager.getOrAddData<int>(upgradeName + "-level", level);
    }

    private void Update()
    {
        upgradeName = this.name;
        if (upgradeName.Contains("(Clone)"))
        {
            upgradeName = upgradeName.Substring(0, upgradeName.IndexOf("(Clone)"));
        }
        SaveManager.updateOrAddData<float>(upgradeName + "-price", price);
        SaveManager.updateOrAddData<int>(upgradeName + "-level", level);
    }

    public void onPurchase(string objectName)
    {
        pointsController = FindObjectOfType<PointsController>();
        ShopItem item = GameObject.Find(objectName + "(Clone)").GetComponent<ShopItem>();

        pointsController.addPointsPerSecond(item.totalPPS);
        pointsController.addPointsToTotal(item.totalPoints);

        pointsController.minusPointsFromTotal(item.price);

        item.level++;
        item.price *= item.level;
    }

}
