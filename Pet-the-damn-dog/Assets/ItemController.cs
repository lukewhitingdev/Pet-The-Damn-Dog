using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private TextMeshProUGUI itemName, itemPrice, purchaseButtonTxt;
    private Button purchaseButton;

    private PointsController pointsController;
    private ShopItem shopItem;

    void Awake()
    {

        // Grab references to all the different texts
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in texts)
        {
            switch (text.gameObject.name)
            {
                case "Name":
                    itemName = text;
                    break;

                case "Price":
                    itemPrice = text;
                    break;

                case "Purchase":
                    purchaseButtonTxt = text;
                    break;

                default:
                    break;
            }
        }

        purchaseButton = GetComponentInChildren<Button>();
        pointsController = FindObjectOfType<PointsController>();
    }

    private void Start()
    {
        shopItem = GetComponentInChildren<ShopItem>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (itemName.text != shopItem.upgradeName)
        {
            itemName.text = shopItem.upgradeName;
        }

        // Update item price dynamically.
        itemPrice.text = shopItem.price.ToString() + " Love Points";

        // Update purchase button availability 
        if (!shopItem.oneTimeBought && !(shopItem.price > pointsController.getTotalPoints()))
        {
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        }

        if (shopItem.oneTime)
        {
            purchaseButtonTxt.text = (!shopItem.oneTimeBought) ? "Purchase" : "Purchased";

            if (shopItem.oneTimeBought)
                itemPrice.text = "Purchased";
        }
    }
}
