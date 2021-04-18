using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class LingeringLove : ShopItem
{

    private float priceIncrement = 10.0f;

    private void Awake()
    {
        upgradeName = "Lingering Love";
        price = 1;
        level = 1;

        Debug.Log("-- " + this.gameObject.name);

        Load();
    }

    public override void onPurchase()
    {
        // For some reason have to grab these since i think because this is called from the onClick of a button its within its own scope.
        LingeringLove LingeringLove = FindObjectOfType<LingeringLove>();
        PointsController pointsController = FindObjectOfType<PointsController>();

        Debug.Log("Lingering Love Upgrade Purchased! Price: " + LingeringLove.price.ToString());

        GameMaster.instance.addPetsPerSecond(1.0f);

        pointsController.minusPointsFromTotal(LingeringLove.price);

        //LingeringLove.level++;
        //LingeringLove.price *= LingeringLove.level;

        base.UpdateData();
    }
}
