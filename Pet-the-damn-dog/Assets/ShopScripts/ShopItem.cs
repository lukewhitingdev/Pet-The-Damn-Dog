using UnityEngine;
using UnityEngine.UI;

// Virtual class for allowing each shopItem to be triggered whilst having different function bodies depending on what they wanna do.
public abstract class ShopItem : MonoBehaviour
{
    public abstract void onPurchase();          // Virtual function that is overwritten by the specific shop item scripts.

    private Button purchaseButton;
    private PointsController pointsController;
    public float price = 1.0f;

    private void Start()
    {
        purchaseButton = this.gameObject.transform.parent.gameObject.GetComponentInChildren<Button>();
        purchaseButton.interactable = false;
        pointsController = FindObjectOfType<PointsController>();
    }

    private void Update()
    {
        Debug.Log((pointsController.getTotalPoints() > price));
        if (pointsController.getTotalPoints() > price)
        {
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        };
    }
}
