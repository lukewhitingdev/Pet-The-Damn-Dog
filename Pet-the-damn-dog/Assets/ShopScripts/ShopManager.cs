using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // Load all items from a prefab folder in resources.
    // Use their prefab names as their item names
    // Have a item script inside their prefab that we can use to get their value and also their onPurchase method.

    // Where to place the UI elements
    GameObject contentObject;

    // UI
    GameObject itemUIPrefab;
    GameObject titleUIPrefab;
    GameObject dividerUIPrefab;

    // To make the content height big enough to cover all items allowing scrolling.
    float itemUIHeight;
    float titleUIHeight;
    float dividerUIHeight;
    float contentHeight;      

    // Shop Upgrades
    GameObject[] shopItems;

    private void Awake()
    {

        contentObject = GameObject.FindGameObjectWithTag("[ShopUI]Content");

        contentHeight = 0;

        LoadUI();

        if (itemUIPrefab != null)
            LoadItems();
        else
            Debug.LogError("[ShopManager][Awake] Tried to load items before the UI is loaded!");

        // Set the content rect height to the one we calculated when loading.
        contentObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight + 150);
    }

    // Loads all the UI elements in the Resources/Shop/UI folder.
    private void LoadUI()
    {
        Debug.Log("[ShopManager] Loading UI Elements...");

        GameObject[] uiItems = Resources.LoadAll<GameObject>("Shop/UI");

        foreach (var uiItem in uiItems)
        {
            switch (uiItem.name)
            {
                case "Item":
                    if(itemUIPrefab == null)
                    {
                        itemUIPrefab = uiItem;
                        itemUIHeight = itemUIPrefab.GetComponent<RectTransform>().rect.height;
                    }
                    break;

                case "Title":
                    if (titleUIPrefab == null)
                    {
                        titleUIPrefab = uiItem;
                        titleUIHeight = titleUIPrefab.GetComponent<RectTransform>().rect.height;
                    }
                    break;

                case "Divider":
                    if (dividerUIPrefab == null)
                    {
                        dividerUIPrefab = uiItem;
                        dividerUIHeight = dividerUIPrefab.GetComponent<RectTransform>().rect.height;
                    }
                    break;

                default:
                    break;
            }
        }
        Debug.Log("[ShopManager] Completed Loading UI Elements!");
    }

    // Loads all the upgrades in the Resources/Shop/Upgrades folder.
    private void LoadItems()
    {
        Debug.Log("[ShopManager] Loading Shop Items...");

        shopItems = Resources.LoadAll<GameObject>("Shop/Upgrades");

        foreach (var upgrade in shopItems)
        {
            Debug.Log(upgrade.name);

            ShopItem shopItem = upgrade.GetComponent<ShopItem>();

            GameObject UIObject;

            switch (upgrade.tag)
            {
                case "[ShopUI]Item":
                    // Create the item.
                    UIObject = Instantiate(itemUIPrefab, contentObject.transform);
                    UIObject.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.name;
                    UIObject.GetComponentInChildren<Button>().onClick.AddListener(shopItem.onPurchase);

                    contentHeight += itemUIHeight;

                    break;

                default:
                    break;
            }
        }

        Debug.Log("[ShopManager] Finished Loading Shop Items!");
    }
}
