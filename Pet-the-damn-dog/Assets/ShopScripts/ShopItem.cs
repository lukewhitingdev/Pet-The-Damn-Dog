using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Virtual class for allowing each shopItem to be triggered whilst having different function bodies depending on what they wanna do.
public abstract class ShopItem : MonoBehaviour
{
    public abstract void onPurchase();                                 // Virtual function that is overwritten by the specific shop item scripts.

    private Button purchaseButton;
    private PointsController pointsController;
    public string upgradeName = "[YOU HAVE NOT SET THE UPGRADE NAME. SET IT]";
    public float price = 1.0f;
    public int level = 1;
    protected GameObject parentObject;                                // Used so that the script knows which name to use even when not parented.

    private TextMeshProUGUI[] texts;

    protected void Load()
    {
        upgradeName = this.name;
        if (upgradeName.Contains("(Clone)"))
        {
            upgradeName = upgradeName.Substring(0, upgradeName.IndexOf("(Clone)"));
        }
        Debug.LogFormat("Loading ShopItem {0}", upgradeName);
        price = (float)SaveManager.getOrAddData<float>(upgradeName + "-price", price);
        level = (int)SaveManager.getOrAddData<int>(upgradeName + "-level", level);
    }

    protected void UpdateData()
    {
        upgradeName = this.name;
        SaveManager.updateData<float>(upgradeName + "-price", price);
        SaveManager.updateData<int>(upgradeName + "-level", level);
    }

    private void Start()
    {
        purchaseButton = this.gameObject.transform.parent.gameObject.GetComponentInChildren<Button>();
        purchaseButton.interactable = false;
        pointsController = FindObjectOfType<PointsController>();
        texts = this.transform.parent.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        Debug.Log("-- " + price + " || " + level);
        price *= level;
    }

    private void Update()
    {
        //Debug.Log((pointsController.getTotalPoints() > price));
        if (pointsController.getTotalPoints() > price)
        {
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        };

        Debug.Log("Update: " + this.name + " Price: " + price);

        foreach (var text in texts)
        {
            switch (text.gameObject.name)
            {
                case "Price":
                        text.text = price + " Love Points";
                    break;

                default:
                    break;
            }
        }
    }
}
