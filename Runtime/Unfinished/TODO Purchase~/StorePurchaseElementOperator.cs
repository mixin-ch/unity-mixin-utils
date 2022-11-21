using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using TMPro;
using UnityEngine.UI;

public class StorePurchaseElementOperator : MonoBehaviour
{
    [Header("Purchase")]
    [Space]
    public IAPButton IAPButton;
    public Button Button;
    public TMP_Text TitleText;
    public TMP_Text DescriptionText;
    public TMP_Text PriceText;

    /// <summary>
    /// This Word will get removed from the Title
    /// It's because the Stores automatically prefix it.
    /// </summary>
    const string _REMOVE_FROM_TITLE = "(MixIsland Jumpers)";

    void Start()
    {
        // if no product id is set, then return
        if (IAPButton.productId == null) return;
        // get product id
        string productId = IAPButton.productId;
        // get product
        var product = CodelessIAPStoreListener.Instance.GetProduct(productId);
        // if product does not exist, then return
        if (product == null) return;

        int quantity = (int)product.definition.payout.quantity;
        string subtype = product.definition.payout.subtype;

        //listen to button click based on type
        switch (subtype)
        {
            case "Secrets":
                IAPButton.onPurchaseComplete.AddListener(delegate
                {
                    PurchaseManager.Instance.PurchaseSecrets(quantity);
                });
                break;
            case "Nobility":
                IAPButton.onPurchaseComplete.AddListener(delegate
                {
                    PurchaseManager.Instance.PurchaseNobility(quantity);
                });
                break;
            case "DLC":
                IAPButton.onPurchaseComplete.AddListener(delegate
                {
                    PurchaseManager.Instance.PurchaseDLC();
                });
                break;
            default:
                Debug.LogError($"unknown subtype {subtype}: purchase failed");
                break;
        }

        // set listener for purchase failed 
        IAPButton.onPurchaseFailed.AddListener(delegate
        {
            string title = "Purchase failed";
            string message = "Purchase has failed, try again.";
            new PopupObject(PopupType.Error, title, message)
                .AutoOpen();
        });

        // Disable Product if not available
        if (!product.availableToPurchase)
        {
            gameObject.SetActive(false);
            return;
        }

        //set texts
        if (TitleText != null)
        {
            string title = product.metadata.localizedTitle
                .Replace(_REMOVE_FROM_TITLE, "");
            TitleText.text = title;
        }

        if (DescriptionText != null)
        {
            DescriptionText.text = product.metadata.localizedDescription;
        }

        if (PriceText != null)
        {
            //PriceText.text = product.metadata.isoCurrencyCode + " ";
            PriceText.text = product.metadata.localizedPriceString;
        }
    }


}
