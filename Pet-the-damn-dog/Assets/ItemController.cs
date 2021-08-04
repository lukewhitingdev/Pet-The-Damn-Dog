using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    private TextMeshProUGUI itemName, itemPrice, itemStats, purchaseButtonTxt;
    private Button purchaseButton;

    private PointsController pointsController;
    private ShopItem shopItem;

    private Image[] upgradeIcons;

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

                case "Stat":
                    itemStats = text;
                    break;

                default:
                    break;
            }
        }
        upgradeIcons = transform.Find("Icon").GetComponentsInChildren<Image>();
        purchaseButton = GetComponentInChildren<Button>();
        pointsController = FindObjectOfType<PointsController>();
    }

    private void Start()
    {
        shopItem = GetComponentInChildren<ShopItem>();

        // Setup icon stuff
        if (shopItem.largeUpgrade)
        {
            upgradeIcons[0].enabled = false;
            upgradeIcons[1].enabled = true;
        }
        else
        {
            upgradeIcons[0].enabled = true;
            upgradeIcons[1].enabled = false;
        }

        // Setup stats
        itemStats.text = "";

        itemStats.text += (shopItem.totalClickPower > 0) ? " CP + " + shopItem.totalClickPower.ToString() : "";
        itemStats.text += (shopItem.totalPPS > 0) ? " PP + " + shopItem.totalPPS.ToString() : "";
        itemStats.text += (shopItem.multiplier > 0) ? " Multi + " + shopItem.multiplier.ToString() : "";
    }

    // Update is called once per frame
    void Update()
    {

        if (itemName.text != shopItem.upgradeName)
        {
            itemName.text = shopItem.upgradeName;
        }

        // Update item price dynamically.
        itemPrice.text = NumberFormatter.formatNumber(shopItem.price) + " Love Points";

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
            {
                itemPrice.text = "Purchased";
                itemPrice.color = Color.gray;
                purchaseButtonTxt.color = Color.green;
                purchaseButton.targetGraphic.enabled = false;
            }
        }
    }
}
