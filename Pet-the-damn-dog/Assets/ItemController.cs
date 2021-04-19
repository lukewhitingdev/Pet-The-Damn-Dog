using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private TextMeshProUGUI itemName, itemPrice;
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
        itemName.text = shopItem.upgradeName;
        itemPrice.text = shopItem.price.ToString() + " Love Points";

        purchaseButton.interactable = !(shopItem.price > pointsController.getTotalPoints());
    }
}
