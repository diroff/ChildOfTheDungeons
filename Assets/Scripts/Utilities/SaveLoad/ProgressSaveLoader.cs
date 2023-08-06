using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ProgressSaveLoader : MonoBehaviour
{
    public const string Key = "PlayerData";

    [SerializeField] private ProgressionController _progressionController;

    private IStorageService _storageService;
    private ProgressData _currentData;

    private void Awake()
    {
        _storageService = new JsonToFileStorageService();

        _storageService.Load<User>(Key, data => 
        {
            if (data == default)
                Save();
        });
    }

    [ContextMenu("Save this")]
    public void Save()
    {
        ProgressData data = new ProgressData();
        data.SkillsData = new SkillsData();

        data.Level = _progressionController.Player.GetLevel();
        data.Coins = _progressionController.Player.Coins;

        data.SkillsData.Power = _progressionController.Player.Skills.Power.CurrentLevel;
        data.SkillsData.Agility = _progressionController.Player.Skills.Agility.CurrentLevel;
        data.SkillsData.Luck = _progressionController.Player.Skills.Luck.CurrentLevel;
        data.SkillsData.Endurance = _progressionController.Player.Skills.Endurance.CurrentLevel;

        data.InventoryData = new InventoryData();

        data.InventoryData.GoldKeys = _progressionController.Player.Inventory.GoldKeyCount;
        data.InventoryData.SilverKeys = _progressionController.Player.Inventory.SilverKeyCount;
        data.InventoryData.Potions = _progressionController.Player.Inventory.PotionCount;

        if (_progressionController.Player.WeaponSlot.IsSomeWeapon())
        {
            data.WeaponData = new WeaponData();
            data.HasWeapon = true;

            data.WeaponData.WeaponID = _progressionController.Player.WeaponSlot.Weapon.ItemName;
        }
        
        data.ArmorData = new ArmorData();

        if(_progressionController.Player.ArmorSlots.Costume != null)
        {
            data.ArmorData.CostumeID = _progressionController.Player.ArmorSlots.Costume.ItemName;
            data.HasCostume = true;
        }

        if (_progressionController.Player.ArmorSlots.Helm != null)
        {
            data.ArmorData.HelmID = _progressionController.Player.ArmorSlots.Helm.ItemName;
            data.HasHelm = true;
        }

        if (_progressionController.Player.ArmorSlots.Shoes != null)
        {
            data.ArmorData.ShoesID = _progressionController.Player.ArmorSlots.Shoes.ItemName;
            data.HasShoes = true;
        }

        _storageService.Save(Key, data);
    }

    public void Load()
    {
        _storageService.Load<ProgressData>(Key, data => 
        { 
            _currentData = data;
        });
    }

    public ProgressData GetData()
    {
        Load();
        return _currentData;
    }
}

public class ProgressData
{
    public int Level;
    public int Coins;

    public SkillsData SkillsData;
    public WeaponData WeaponData;
    public ArmorData ArmorData;
    public InventoryData InventoryData;

    public bool HasWeapon = false;
    public bool HasCostume = false;
    public bool HasHelm = false;
    public bool HasShoes = false;
}

public class SkillsData
{
    public int Power;
    public int Agility;
    public int Luck;
    public int Endurance;
}

public class WeaponData
{
    public string WeaponID;
}

public class ArmorData
{
    public string CostumeID;
    public string HelmID;
    public string ShoesID;
}

public class InventoryData
{
    public int GoldKeys;
    public int SilverKeys;
    public int Potions;
}