using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgrade : ShopItem
{

    public override float getPrice(){ return 100.0f; }

    public override void onPurchase()
    {
        Debug.Log("Basic Upgrade Purchased! Price: " + getPrice().ToString());
    }
}
