using System.Collections.Generic;
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

    public void Save()
    {
        ProgressData data = new ProgressData();

        data.Level = _progressionController.Player.GetLevel();
        data.Power = _progressionController.Player.Skills.Power.CurrentLevel;
        data.Agility = _progressionController.Player.Skills.Agility.CurrentLevel;
        data.Luck = _progressionController.Player.Skills.Luck.CurrentLevel;
        data.Endurance = _progressionController.Player.Skills.Endurance.CurrentLevel;

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
    public int Power;
    public int Agility;
    public int Luck;
    public int Endurance;
}