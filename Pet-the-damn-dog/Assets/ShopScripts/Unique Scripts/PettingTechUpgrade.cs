﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PettingTechUpgrade : ShopItem
{

    private float priceIncrement = 10.0f;


    private void Awake()
    {
        price = 1.0f;
    }

    public override void onPurchase()
    {
        // For some reason have to grab these since i think because this is called from the onClick of a button its within its own scope.
        PettingTechUpgrade pettingTech = FindObjectOfType<PettingTechUpgrade>();
        PointsController pointsController = FindObjectOfType<PointsController>();

        Debug.Log("Petting Technique Upgrade Purchased! Price: " + pettingTech.price.ToString());

        GameMaster.instance.addClickPower(pettingTech.price / 10);

        pointsController.minusPointsFromTotal(pettingTech.price);

        pettingTech.price += priceIncrement;
    }
}
