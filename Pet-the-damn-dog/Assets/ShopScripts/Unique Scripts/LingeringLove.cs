using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LingeringLove : ShopItem
{

    private float priceIncrement = 10.0f;

    private void Awake()
    {
        price = 10.0f;
    }

    public override void onPurchase()
    {
        // For some reason have to grab these since i think because this is called from the onClick of a button its within its own scope.
        PettingTechUpgrade pettingTech = FindObjectOfType<PettingTechUpgrade>();
        PointsController pointsController = FindObjectOfType<PointsController>();

        Debug.Log("Lingering Love Upgrade Purchased! Price: " + pettingTech.price.ToString());

        GameMaster.instance.addPetsPerSecond(1.0f);

        pointsController.minusPointsFromTotal(pettingTech.price);

        pettingTech.price += priceIncrement;
    }
}
