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

        data.SkillsData.Power = _progressionController.Player.Skills.Power.CurrentLevel;
        data.SkillsData.Agility = _progressionController.Player.Skills.Agility.CurrentLevel;
        data.SkillsData.Luck = _progressionController.Player.Skills.Luck.CurrentLevel;
        data.SkillsData.Endurance = _progressionController.Player.Skills.Endurance.CurrentLevel;

        if (_progressionController.Player.WeaponSlot.IsSomeWeapon())
        {
            data.WeaponData = new WeaponData();
            data.HasWeapon = true;

            data.WeaponData.ID = _progressionController.Player.WeaponSlot.Weapon.ItemName;
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
    public SkillsData SkillsData;
    public WeaponData WeaponData;
    public bool HasWeapon = false;
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
    public string ID;
}