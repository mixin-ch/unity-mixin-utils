using UnityEngine;
using System;

[Serializable]
public class GameData : DataFile
{
    //google play games services -> user id for firebase connection
    //currently not using i guess
    public string UserId;

    //player settings
    //[ReadOnly]
    [SerializeField]
    public string CurrentSelectedCharacter;
    public string CurrentSelectedCharacterSkin;
    public bool ShowTutorial = true;

    //purchases
    public SaveInventory SaveInventory = new SaveInventory();

    //Stat Save Package of the Game.
    [Space]
    public StatSavePackage GameStatSavePackage = new StatSavePackage();

    //Stat Save Package of active Level.
    [Space]
    public StatSavePackage CurrentLevelStatSavePackage = new StatSavePackage();

    /*public void SetSaveInformation()
    {
        GameVersion = Application.version.ToString();
        TestBuild = GameManager.TestMode;
        SaveCounter++;
        LastSave = DateTime.Now;
    }*/

    public void LevelEndSetup()
    {
        //Merge Data of last Level Round to Main Save.
        MergeCurrentLevelSaveData();

        //Sets all unlocked and 0 cost Articles to "owned".
        SaveInventory.RefreshOwnership();
    }

    public void Clear()
    {
        CurrentSelectedCharacter = "";
        CurrentSelectedCharacterSkin = "";
        ShowTutorial = true;
        SaveInventory.Clear();
        GameStatSavePackage.Clear();
        CurrentLevelStatSavePackage.Clear();
        RefreshData();
    }

    public void RefreshData()
    {
        FillDefaultFields();
        SaveInventory.RefreshOwnership();
    }

    public void FillDefaultFields()
    {
        if (SaveInventory == null)
            SaveInventory = new SaveInventory();
        if (GameStatSavePackage == null)
            GameStatSavePackage = new StatSavePackage();
        if (CurrentLevelStatSavePackage == null)
            CurrentLevelStatSavePackage = new StatSavePackage();

        SaveInventory.FillDefaultFields();
        GameStatSavePackage.FillDefaultFields();
        CurrentLevelStatSavePackage.FillDefaultFields();
    }

    public void MergeCurrentLevelSaveData()
    {
        GameStatSavePackage = GameStatSavePackage.Merge(CurrentLevelStatSavePackage);
        CurrentLevelStatSavePackage.Clear();
    }

    public StatSavePackage GetTotalSaveDataPackage()
    {
        return GameStatSavePackage.Merge(CurrentLevelStatSavePackage);
    }

    public StatSavePackage GetGameStatSavePackage()
        => GameStatSavePackage;

    #region Selected Character
    public bool HasValidSelectedCharacter()
    {
        return Enum.TryParse(CurrentSelectedCharacter, true, out CharacterType _);
    }

    public CharacterType GetCurrentSelectedCharacter()
    {
        CharacterType characterType;
        bool isEnumParsed = Enum.TryParse(CurrentSelectedCharacter, true, out characterType);
        return isEnumParsed ? characterType : CharacterType.Moth;
    }

    public void SetCurrentSelectedCharacter(CharacterType characterId)
    {
        CurrentSelectedCharacter = characterId.ToString();
    }

    public bool IsCharacterSelected(CharacterType characterId)
    {
        CharacterType characterType;
        bool isEnumParsed = Enum.TryParse(CurrentSelectedCharacter, true, out characterType);
        return isEnumParsed && characterType == characterId;
    }
    #endregion

    #region Selected CharacterSkin
    public CharacterSkinData GetCurrentSelectedCharacterSkin()
    {
        if (!InventoryManager.Instance.CharacterDict.TryGetValue(GetCurrentSelectedCharacter(), out CharacterData characterData))
            return null;

        CharacterSkinData characterSkin = InventoryManager.Instance.GetCharacterSkin(characterData.CharacterType, CurrentSelectedCharacterSkin);
        return characterSkin == null ? characterData.SkinList[0] : characterSkin;
    }

    public void SetCurrentSelectedCharacterSkin(string skinId)
    {
        CharacterType characterType = GetCurrentSelectedCharacter();
        CharacterSkinData characterSkin = InventoryManager.Instance.GetCharacterSkin(characterType, skinId);

        if (characterSkin == null)
        {
            Debug.LogError("Character Skin with ID: '" + characterSkin.SkinId
                + "' not part of Character with ID: '" + characterType.ToString() + "'.");
            return;
        }

        CurrentSelectedCharacterSkin = skinId;
    }

    public bool IsCharacterSkinSelected(CharacterSkinData characterSkin)
    {
        if (!InventoryManager.Instance.CharacterDict.TryGetValue(GetCurrentSelectedCharacter(), out CharacterData characterData))
            return false;

        if (!characterData.SkinList.Contains(characterSkin))
            return false;

        return characterSkin.SkinId == CurrentSelectedCharacterSkin;
    }
    #endregion
}