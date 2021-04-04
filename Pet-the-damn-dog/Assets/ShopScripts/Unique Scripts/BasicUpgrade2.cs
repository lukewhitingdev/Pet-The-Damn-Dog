using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpgrade2 : ShopItem
{
    public override float getPrice() { return 1.0f; }

    public override void onPurchase()
    {
        Debug.Log("Basic Upgrade 2 Purchased! Price: " + getPrice().ToString());
    }
}
