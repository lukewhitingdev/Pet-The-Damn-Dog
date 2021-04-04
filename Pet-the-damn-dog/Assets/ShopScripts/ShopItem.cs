using UnityEngine;

// Virtual class for allowing each shopItem to be triggered whilst having different function bodies depending on what they wanna do.

public abstract class ShopItem : MonoBehaviour
{
    public abstract void onPurchase();          // Virutal function that is overwritten by the specific shop item scripts.
    public abstract float getPrice();           // The price for the item.
}
