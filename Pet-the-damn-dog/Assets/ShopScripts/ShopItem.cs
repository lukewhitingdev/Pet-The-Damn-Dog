using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Virtual class for allowing each shopItem to be triggered whilst having different function bodies depending on what they wanna do.
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
    }

    protected void Load()
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

    protected void UpdateData()
    {

        // TODO: Refactor upgradeData to be obselete.
        upgradeName = this.name;
        SaveManager.updateData<float>(upgradeName + "-price", price);
        SaveManager.updateData<int>(upgradeName + "-level", level);
    }

    public void onPurchase(string objectName)
    {
        // 
        pointsController = FindObjectOfType<PointsController>();
        ShopItem item = GameObject.Find(objectName + "(Clone)").GetComponent<ShopItem>();

        pointsController.addPointsPerSecond(item.totalPPS);
        pointsController.addPointsToTotal(item.totalPoints);

        pointsController.minusPointsFromTotal(item.price);

        item.level++;
        item.price *= item.level;
    }

}
