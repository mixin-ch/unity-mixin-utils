using System;
using UnityEngine;
using UnityEngine.Events;

public class PurchaseManager : MonoBehaviour
{
    public static PurchaseManager Instance;

    public static event Action OnPurchaseSucceeded;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PurchaseSecrets(int amount)
    {
        string title = $"+{amount} Secrets!";
        string message = $"Congratulation, you successfully bought some Secrets :-)";
        Sprite sprite = CollectionOwner.Instance.GetSprite(SpriteType.Secrets);
        SaveManager.GameData.GameStatSavePackage.Secrets += amount;
        SaveManager.GameDataFileManager.Save();
        StoreManager.Instance.Refresh();
        CreateNewPurchasePopup(title, message, sprite);
        OnPurchaseSuccess();
    }

    public void PurchaseNobility(int amount)
    {
        string title = $"+{amount} Nobility!";
        string message = $"Congratulation, you successfully bought some Nobility :-)";
        Sprite sprite = CollectionOwner.Instance.GetSprite(SpriteType.Nobility);
        SaveManager.GameData.GameStatSavePackage.Nobility += amount;
        SaveManager.GameDataFileManager.Save();
        StoreManager.Instance.Refresh();
        CreateNewPurchasePopup(title, message, sprite);
        OnPurchaseSuccess();
    }

    public void PurchaseDLC()
    {
        string title = $"DLC";
        string message = "Congratulation, you successfully bought the DLC package :-)";
        Sprite sprite = CollectionOwner.Instance.GetSprite(SpriteType.Checkmark);
        SaveManager.GameData.SaveInventory.SetDlcOwned(DlcType.UnnamedExtensions, true);
        SaveManager.GameDataFileManager.Save();
        StoreManager.Instance.Refresh();
        CreateNewPurchasePopup(title, message, sprite);
        OnPurchaseSuccess();
    }

    // purchasing experience is only possible in sandbox mode
    public void PurchaseExperience(int amount)
    {
        string title = $"+{amount} Experience!";
        string message = $"Congratulation, you successfully bought some Experience :-)";
        Sprite sprite = CollectionOwner.Instance.GetSprite(SpriteType.Checkmark);
        SaveManager.GameData.GameStatSavePackage.Experience += amount;
        SaveManager.GameDataFileManager.Save();
        StoreManager.Instance.Refresh();
        CreateNewPurchasePopup(title, message, sprite);
        OnPurchaseSuccess();
    }

    public static void BuyStoreProduct(int amount, CurrencyType currencyType, Action onSuccessCall)
    {
        // check if sum is greater equal 0
        if (CanPurchaseWithCurrency(amount, currencyType)) // purchase successful
        {
            RemoveCurrency(amount, currencyType);

            onSuccessCall?.Invoke();

            // Fire Event
            OnPurchaseSucceeded?.Invoke();
        }
        else // purchase failed
        {
            CreateAndOpenPurchaseFailedPopup(amount, currencyType);
        }
    }

    private static void RemoveCurrency(int amount, CurrencyType currencyType)
    {
        switch (currencyType)
        {
            case CurrencyType.Secrets:
                SaveManager.GameData.GetGameStatSavePackage().RemoveSecrets(amount);
                break;
            case CurrencyType.Nobility:
                SaveManager.GameData.GetGameStatSavePackage().RemoveNobility(amount);
                break;
            default:
                ThrowUnknownCurrencyTypeException(currencyType);
                return;
        }
        SaveManager.GameDataFileManager.Save();
    }

    private static bool CanPurchaseWithCurrency(int amount, CurrencyType currencyType)
    {
        int currentAmount;
        switch (currencyType)
        {
            case CurrencyType.Secrets:
                currentAmount = SaveManager.GameData.GetGameStatSavePackage().GetSecrets();
                break;
            case CurrencyType.Nobility:
                currentAmount = SaveManager.GameData.GetGameStatSavePackage().GetNobility();
                break;
            default:
                ThrowUnknownCurrencyTypeException(currencyType);
                return false;
        }
        // Check if currentAmount - amount is greater or equal 0
        return (currentAmount - amount) >= 0;
    }


    public void RemoveNobility(int amount, Action onSuccessCall)
    {
        // check if value is greater than 0
        if (SaveManager.GameData.GameStatSavePackage.Nobility - amount < 0)
        {
            // purchase failed
            int difference = Mathf.Abs(SaveManager.GameData.GetGameStatSavePackage().GetNobility() - amount);
            string title = "Not enough Nobility";
            string message = $"You need {difference} more Nobility.";
            new PopupObject(PopupType.Error, title, message)
                .AutoOpen();
        }
        else
        {
            // purchase successful
            if (onSuccessCall != null)
                onSuccessCall();
            //SaveManager.GameData().GetGameStatSavePackage().GetNobility() -= amount;
            //SaveManager.GameDataFile().Save();
        }
    }

    private void CreateNewPurchasePopup(string title, string message, Sprite sprite) =>
        new PopupObject(PopupType.Reward, title, message)
        .AddSprite(sprite)
        .AddToList();

    private void OnPurchaseSuccess()
    {
        Debug.Log("purchase done");
        PopupManager.Instance.TryOpenNext();
    }

    public static void CreateAndOpenPurchaseSucceededPopup(string title, string message, Sprite sprite)
    {
        new PopupObject(PopupType.Reward, title, message)
            .AddSubmitButtonText(ButtonTextType.Cool)
            .AddSprite(sprite)
            .AutoOpen();
    }

    private static void CreateAndOpenPurchaseFailedPopup(int amount, CurrencyType currencyType)
    {
        int difference;
        switch (currencyType)
        {
            case CurrencyType.Secrets:
                difference = Mathf.Abs(SaveManager.GameData.GetGameStatSavePackage().GetSecrets() - amount);
                break;
            case CurrencyType.Nobility:
                difference = Mathf.Abs(SaveManager.GameData.GetGameStatSavePackage().GetNobility() - amount);
                break;
            default:
                ThrowUnknownCurrencyTypeException(currencyType);
                return;
        }
        string title = $"Not enough {currencyType}";
        string message = $"You need {difference} more {currencyType}.";
        new PopupObject(PopupType.Error, title, message)
            .AutoOpen();
    }

    private static void ThrowUnknownCurrencyTypeException(CurrencyType currencyType)
    {
        throw new Exception($"Unknown CurrencyType {currencyType}");
    }
}

