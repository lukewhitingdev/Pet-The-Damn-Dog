﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class PettingTechUpgrade : ShopItem
{

    private float priceIncrement = 10.0f;


    private void Awake()
    {
        upgradeName = "Petting Tech Upgrade";
        price = 1.0f;
        level = 1;

        Load();
    }

    public override void onPurchase()
    {
        // For some reason have to grab these since i think because this is called from the onClick of a button its within its own scope.
        PettingTechUpgrade pettingTech = FindObjectOfType<PettingTechUpgrade>();

        PointsController pointsController = FindObjectOfType<PointsController>();

        Debug.Log("Petting Technique Upgrade Purchased! Price: " + pettingTech.price.ToString());

        GameMaster.instance.addClickPower(pettingTech.price / 10);

        pointsController.minusPointsFromTotal(pettingTech.price);

        base.BaseOnPurchase();
        base.UpdateData();
    }
}
