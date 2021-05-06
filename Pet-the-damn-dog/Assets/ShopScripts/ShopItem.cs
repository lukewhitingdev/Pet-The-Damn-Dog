﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopItem : MonoBehaviour
{

    public struct upgrade
    {
        public string name;
        public string value;
    }

    private PointsController pointsController;

    public bool verbose = false;

    [HideInInspector]
    public string upgradeName = "N/A";

    [Header("Upgrade Costs")]
    public float price = 1.0f;

    [HideInInspector]
    public int level = 1;

    [Header("Upgrade values")]
    public bool largeUpgrade = default;
    public bool oneTime = default;
    public float totalClickPower = default;
    public float totalPPS = default;
    public float multiplier = default;

    [HideInInspector]
    public bool oneTimeBought = false;

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

        if(verbose)
            Debug.LogFormat("Loading ShopItem {0}", upgradeName);

        price = (float)SaveManager.getOrAddData<float>(upgradeName + "-price", price);
        level = (int)SaveManager.getOrAddData<int>(upgradeName + "-level", level);
        oneTimeBought = (bool)SaveManager.getOrAddData<bool>(upgradeName + "-oneTimePurchased", oneTimeBought);
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
        SaveManager.updateOrAddData<bool>(upgradeName + "-oneTimePurchased", oneTimeBought);
    }

    public void onPurchase(string objectName)
    {
        pointsController = FindObjectOfType<PointsController>();
        ShopItem item = GameObject.Find(objectName + "(Clone)").GetComponent<ShopItem>();

        if(item.totalPPS > 0)
            pointsController.addPointsPerSecond(item.totalPPS);
        
        if(item.multiplier > 0)
            pointsController.addMultiplier(multiplier);

        if(item.totalClickPower > 0)
            GameMaster.instance.addClickPower(item.totalClickPower);

        pointsController.minusPointsFromTotal(item.price);

        item.level++;
        item.price *= item.level;

        if (item.oneTime && !item.oneTimeBought)
        {
            item.oneTimeBought = true;
        }
    }

}
