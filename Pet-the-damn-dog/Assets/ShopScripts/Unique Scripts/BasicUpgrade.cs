using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class BasicUpgrade : ShopItem
{

    public override void onPurchase()
    {
        Debug.Log("Basic Upgrade Purchased!");
    }
}
