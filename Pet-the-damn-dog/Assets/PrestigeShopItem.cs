using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeShopItem : MonoBehaviour
{

    private Button purchaseButton;
    private PointsController pointsController;
    private TextMeshProUGUI prestigePointsText;

    // Start is called before the first frame update
    void Awake()
    {
        pointsController = FindObjectOfType<PointsController>();

        purchaseButton = GetComponentInChildren<Button>();

        // Grab references to all the different texts
        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (var text in texts)
        {
            switch (text.gameObject.name)
            {
                case "PrestigePoints":
                    prestigePointsText = text;
                    break;

                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        purchaseButton.interactable = (pointsController.getPrestigePoints() < 1) ? false : true;

        prestigePointsText.text = pointsController.getPrestigePoints().ToString() + "x";
    }
}
